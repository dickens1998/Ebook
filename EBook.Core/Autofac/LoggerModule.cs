using Autofac;
using Serilog;
using Serilog.Events;

namespace EBook.Core.Autofac
{
    public class LoggerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.Register<ILogger>((c, p) =>
            {

                var log = new LoggerConfiguration()
                .WriteTo.RollingFile(
                    @"C:\Users\Administrator.20180806-144156\Desktop\日志文件\StopWatch\log-{Date}.txt",
                    LogEventLevel.Information)
                    .CreateLogger();

                return log;
            })
            .Named<ILogger>("mvc_stopwatch")
            .SingleInstance();

            builder.Register<ILogger>((c, p) =>
            {

                var log = new LoggerConfiguration()
                .WriteTo.RollingFile(
                    @"C:\Users\Administrator.20180806-144156\Desktop\日志文件\Exception\log-{Date}.txt",
                    LogEventLevel.Error)
                    .CreateLogger();

                return log;
            })
          .Named<ILogger>("mvc_exction")
          .SingleInstance();
        }
    }
}
