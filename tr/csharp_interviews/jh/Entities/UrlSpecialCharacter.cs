using System;
using System.Collections.Generic;
using System.Text;

namespace jh.Entities
{
    public class UrlSpecialCharacter
    {
        public int UrlSpecialCharacterId { get; set; }
        public char Value { get; set; }
        public string Replacer { get; set; }
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }
    }
}
