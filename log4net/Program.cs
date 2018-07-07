using System;

namespace log4net
{
    public static class Log4NetExtension
    {
        public static ILog GetDebugLogger(this ILog log)
        {
            if (log.IsDebugEnabled)
            {
                return log;
            }
            return null;
        }

        public static void IfDebug(this ILog log, Func<string> action)
        {
            if (log.IsDebugEnabled)
            {
                string temp = action();
                log.Debug(temp);
            }
        }
    }

    class Program
    {
        private string _message;
        public string message
        {
            get
            {
                return _message;
            }
            set { _message = value; }
        }

        static void Main(string[] args)
        {
            int? temp = 0;

            log4net.ILog log = log4net.LogManager.GetLogger(typeof(System.Action));

            log.Info("Application is working");
            for (int i = 0; i < 100; i++)
                log.Error("test test email");

            log.GetDebugLogger()?.Debug("Application is working");

            log.IfDebug(() => "Application is working");

            TestPerformance();
        }

        public static void Test1(ILog log, Program program)
        {
            log.IfDebug(() => $"message :{program.message}");
        }
        public static void Test2(ILog log, Program program)
        {
            log.Debug("message :" + program.message);
        }

        private static void TestPerformance()
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(System.Action));
            var program = new Program();
            program.message = "msg1";

            const long itCount = 10000000;
            for (int i = 0; i < itCount; i++)
            {
                Test1(log, program);
            }
            for (int i = 0; i < itCount; i++)
            {
                Test2(log, program);
            }
        }

        public static void Test3(ILog log, Program program)
        {
            log.GetDebugLogger()?.Debug($"message :{program.message}");
        }

    }
}
