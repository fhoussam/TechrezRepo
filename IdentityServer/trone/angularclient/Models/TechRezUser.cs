using angularclient.DbAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace angularclient.Models
{
    public class TechRezUser : IEntity
    {
        [Key]
        public string Code { get; set; }
        public string UserName { get; set; }
        public List<Order> Orders { get; set; }
    }
}
