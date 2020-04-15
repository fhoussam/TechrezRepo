using domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using app.Common.Enums;
using System.Globalization;

namespace app.Operations.Config.Commands
{
    public class GetDropDownListsQuery : IRequest<Dictionary<DropDownListIdentifier, IEnumerable<KeyValue>>>
    {
        private readonly IEnumerable<DropDownListIdentifier> _requestedDropDownListData;
        public GetDropDownListsQuery(IEnumerable<DropDownListIdentifier> requestedDropDownListData)
        {
            _requestedDropDownListData = requestedDropDownListData;
        }

        public class GetDropDownListDataQueryhandler : IRequestHandler<GetDropDownListsQuery, Dictionary<DropDownListIdentifier, IEnumerable<KeyValue>>>
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
                    { DropDownListIdentifier.Countries, context.Customers.Select(x=> new KeyValue(x.Country, x.Country )) },
                };
            }

            public GetDropDownListDataQueryhandler(INorthwindContext context)
            {
                _context = context;
            }

            public async Task<Dictionary<DropDownListIdentifier, IEnumerable<KeyValue>>> Handle(GetDropDownListsQuery request, CancellationToken cancellationToken)
            {
                var result = new Dictionary<DropDownListIdentifier, IEnumerable<KeyValue>>();
                foreach (var item in _configData(_context).Where(x => request._requestedDropDownListData.Contains(x.Key)))
                {
                    //because angular does not support iteration on dictionaries by default, so we switched back to key value class instead for more flexibility
                    //var data = await item.Value.ToDictionaryAsync(x=>x.Key, x=>x.Value);
                    var data = await item.Value.Select(x => new KeyValue(x.Key, x.Value)).ToListAsync();
                    result.Add(item.Key, data);
                }

                return result;
            }
        }
    }

    public class KeyValue
    {
        public readonly object Key;
        public readonly string Value;
        public KeyValue(object key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}