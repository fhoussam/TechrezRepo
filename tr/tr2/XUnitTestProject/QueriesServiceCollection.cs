using app.Mappings;
using AutoMapper;
using domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NUnitTestProject
{
    public class QueriesServiceCollection : IDisposable
    {
        public NorthwindContext context { get; set; }
        public IMapper mapper { get; set; }

        public QueriesServiceCollection()
        {
            context = NorthwindContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = configurationProvider.CreateMapper();
        }
        public void Dispose()
        {
            NorthwindContextFactory.Destroy(context);
        }
    }

    [CollectionDefinition("Queries")]
    public class QueriesServiceCollectionDefinition : ICollectionFixture<QueriesServiceCollection> { }
}
