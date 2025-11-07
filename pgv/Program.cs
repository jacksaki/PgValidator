using ConsoleAppFramework;

namespace pgv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = ConsoleApp.Create();
            //.ConfigureLogging(x=>
            //{
            //    x.ClearProviders();
            //    x.SetMinimumLevel(LogLevel.Trace);
            //    x.AddZLoggerConsole();
            //});
            app.Add<ValidateCommand>();
            app.Run(args);
        }
    }
}
