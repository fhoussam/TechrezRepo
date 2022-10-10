using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.Solid._3_InterfaceSegregation
{
    public class Document { }

    public interface IPrinter 
    { 
        void Print(Document document);
    }

    public interface IScanner 
    {
        void Scan(Document document);
    }

    public interface IMultifunctionDevice  : IPrinter, IScanner
    {

    }

    public class MultifunctionDevice_1 : IMultifunctionDevice
    {
        public void Print(Document document)
        {
            //print
        }

        public void Scan(Document document)
        {
            //scan
        }
    }

    //OR
    public class MultifunctionDevice_2
    {
        private readonly IPrinter printer;
        private readonly IScanner scanner;

        public MultifunctionDevice_2(IPrinter printer, IScanner scanner)
        {
            this.printer = printer;
            this.scanner = scanner;
        }

        public void Print(Document document)
        {
            printer.Print(document);
        }

        public void Scan(Document document)
        {
            scanner.Scan(document);
        }
    }
}
