namespace MagicVilla_WebAPI.Logging;

public class Logging : ILogging
{
    public void Log(string message, string logtype)
    {
        if (logtype == "error")
        {
            Console.WriteLine("ERROR- " + message);
        }
        else
        {
            Console.WriteLine(message);
        }
    }
}