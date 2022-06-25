using System.Diagnostics;

namespace projektApbd.Server.Middlewares
{
    public class Logger
    {
        public static void Log(Exception ex)
        {
            Trace.TraceError(ex.ToString());
        }
    }
}
