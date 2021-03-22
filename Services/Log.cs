using System;
using System.Collections.Generic;
using System.Text;

namespace Scraper.Services
{
    class Log
    {
        public static void debug(string message)
        {
            message = formatLog(message, LogType.DEBUG);

            Console.WriteLine(message);
            separateMessage();
        }

        public static void info(string message)
        {
            message = formatLog(message, LogType.INFO);

            Console.WriteLine(message);
            separateMessage();
        }

        public static void success(string message)
        {
            message = formatLog(message, LogType.SUCCESS);

            Console.WriteLine(message);
            separateMessage();
        }

        public static void waring(string message)
        {
            message = formatLog(message, LogType.WARNING);

            Console.WriteLine(message);
            separateMessage();
        }

        public static void error(string message)
        {
            message = formatLog(message, LogType.ERROR);

            Console.WriteLine(message);
            separateMessage();
        }

        public static void separateMessage()
        {
            Console.WriteLine("==========================================================");
        }

        private static string formatLog(string message, LogType type)
        {
            string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string logTypeName = ((LogType)type).ToString();

            return string.Format("[{0}][{1}] {2} => {3}", now, logTypeName, Environment.NewLine, message);
        }
    }

    enum LogType
    {
        DEBUG = 0,
        SUCCESS = 1,
        INFO = 2,
        WARNING = 3,
        ERROR = 4
    }
}
