using domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using app.Common.Enums;

namespace app.Operations.Config.Commands
{
    public class GetDropDownListsQuery : IRequest<Dictionary<DropDownListIdentifier, Dictionary<object, string>>>
    {
        public IEnumerable<DropDownListIdentifier> requestedDropDownListData { get; set; }
        public class GetDropDownListDataQueryhandler : IRequestHandler<GetDropDownListsQuery, Dictionary<DropDownListIdentifier, Dictionary<object, string>>>
        {
            private readonly INorthwindContext _context;

            private static Dictionary<DropDownListIdentifier, IQueryable<KeyValue>> _configData(INorthwindContext context)
            {
                return new Dictionary<DropDownListIdentifier, IQueryable<KeyValue>>()
                {
                    { DropDownListIdentifier.Categories, context.Categories.Select(x=> new KeyValue(x.CategoryId, x.CategoryName)) },
                    { DropDownListIdentifier.Suppliers, context.Suppliers.Select(x=> new KeyValue(x.SupplierId, x.CompanyName)) },
                    { DropDownListIdentifier.Employees, context.Employees.Select(x=> new KeyValue(x.EmployeeId, $"{x.FirstName} {x.LastName}")) },
                    { DropDownListIdentifier.Customers, context.Customers.Select(x=> new KeyValue(x.CustomerId, x.CompanyName )) },
                };
            }

            private class KeyValue
            {
                public object Key { get; private set; }
                public string Value { get; private set; }
                public KeyValue(object key, string value)
                {
                    Key = key;
                    Value = value;
                }
            }

            public GetDropDownListDataQueryhandler(INorthwindContext context)
            {
                _context = context;
            }

            public async Task<Dictionary<DropDownListIdentifier, Dictionary<object, string>>> Handle(GetDropDownListsQuery request, CancellationToken cancellationToken)
            {
                var result = new Dictionary<DropDownListIdentifier, Dictionary<object, string>>();
                foreach (var item in _configData(_context).Where(x => request.requestedDropDownListData.Contains(x.Key)))
                {
                    var data = await item.Value.ToDictionaryAsync(x=>x.Key, x=>x.Value);
                    result.Add(item.Key, data);
                }

                return result;
            }
        }
    }
}