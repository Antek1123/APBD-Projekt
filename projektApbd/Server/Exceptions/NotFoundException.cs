using System;

namespace projektApbd.Server.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg) : base(msg) { }
    }
}
