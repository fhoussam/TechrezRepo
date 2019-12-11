using angularclient.DbAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace angularclient.Models
{
    public class Feed : IEntity
    {
        [Key]
        public string Code { get; set; }
        public string UserName { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public string OperationType { get; set; }
    }
}
