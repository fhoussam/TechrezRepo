using domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace NUnitTestProject
{
    public static class NorthwindContextFactory
    {
        private static string _testingFilePath = "../../../TestingData/{0}.json";

        public static NorthwindContext Create()
        {
            //WriteTestingData();

            var options = new DbContextOptionsBuilder<NorthwindContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new NorthwindContext(options);

            context.Database.EnsureCreated();

            ReadTestingData(context);

            return context;
        }

        private static void WriteTestingData()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new CustomResolver(),
                //// OR:
                //ContractResolver = new CustomResolver(new[]
                //{
                //    nameof(PC.Libs), // keep Libs property among virtual properties
                //    nameof(PC.Files) // keep Files property among virtual properties
                //}),
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var realContext = new NorthwindContext(
                new DbContextOptionsBuilder<NorthwindContext>()
                .UseSqlServer("Server=DESKTOP-AA9JR1F\\SQLEXPRESS;Database=Northwind;Trusted_Connection=true;")
                .Options);

            //loading data
            List<Customers> customers = realContext.Customers.ToList();
            List<Employees> employees = realContext.Employees.ToList();
            List<Orders> orders = realContext.Orders.ToList();
            List<OrderDetails> orderDetails = realContext.OrderDetails.ToList();
            List<Products> products = realContext.Products.ToList();

            //writng data
            WriteDataTo("customers", JsonConvert.SerializeObject(customers, settings));
            WriteDataTo("employees", JsonConvert.SerializeObject(employees, settings));
            WriteDataTo("orders", JsonConvert.SerializeObject(orders, settings));
            WriteDataTo("orderDetails", JsonConvert.SerializeObject(orderDetails, settings));
            WriteDataTo("products", JsonConvert.SerializeObject(products, settings));

        }

        private static void ReadTestingData(NorthwindContext context)
        {
            context.Customers.AddRange(LoadJsonObject<Customers>("customers"));
            context.Employees.AddRange(LoadJsonObject<Employees>("employees"));
            context.Orders.AddRange(LoadJsonObject<Orders>("orders"));
            context.OrderDetails.AddRange(LoadJsonObject<OrderDetails>("orderDetails"));
            context.Products.AddRange(LoadJsonObject<Products>("products"));
            context.SaveChanges();
        }

        private static void WriteDataTo(string filename, string json) 
        {
            string path = string.Format(_testingFilePath, filename);
            File.WriteAllText(path, json);
        }

        private static List<T> LoadJsonObject<T>(string fileName)
        {
            string path = string.Format(_testingFilePath, fileName);
            using (StreamReader fs = new StreamReader(path))
            {
                string json = fs.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
        }

        public static void Destroy(NorthwindContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }

        class CustomResolver : DefaultContractResolver
        {
            private readonly List<string> _namesOfVirtualPropsToKeep = new List<string>(new String[] { });

            public CustomResolver() { }

            public CustomResolver(IEnumerable<string> namesOfVirtualPropsToKeep)
            {
                this._namesOfVirtualPropsToKeep = namesOfVirtualPropsToKeep.Select(x => x.ToLower()).ToList();
            }

            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                JsonProperty prop = base.CreateProperty(member, memberSerialization);
                var propInfo = member as PropertyInfo;
                if (propInfo != null)
                {
                    if (propInfo.GetMethod.IsVirtual && !propInfo.GetMethod.IsFinal
                        && !_namesOfVirtualPropsToKeep.Contains(propInfo.Name.ToLower()))
                    {
                        prop.ShouldSerialize = obj => false;
                    }
                }
                return prop;
            }
        }
    }
}
