using System;

namespace app.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
        public NotFoundException(object key)
            : base($"Entity with key ({key}) was not found.")
        {
        }
    }
}