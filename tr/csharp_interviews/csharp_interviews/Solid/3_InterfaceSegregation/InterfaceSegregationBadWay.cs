using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.Solid._3_InterfaceSegregation
{
    internal class InterfaceSegregationBadWay
    {
        public class Document { }

        public interface IMachine
        {
            public void Print();
            public void Scan();
            public void Fax();
        }

        public class MultiFunctionPrinter : IMachine
        {
            public void Fax()
            {
                // i can fax
            }

            public void Print()
            {
                // i can print
            }

            public void Scan()
            {
                // i can Scan
            }
        }

        public class OldFashionedPrinter : IMachine
        {
            public void Fax()
            {
                throw new NotImplementedException();
            }

            public void Print()
            {
                // i can only print :(
            }

            public void Scan()
            {
                throw new NotImplementedException();
            }
        }
    }
}
