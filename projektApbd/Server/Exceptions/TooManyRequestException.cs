namespace projektApbd.Server.Exceptions
{
    public class TooManyRequestException : Exception
    {
        public TooManyRequestException(string msg) : base(msg) { }
    }
}
