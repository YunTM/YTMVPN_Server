using System;
using System.Collections.Generic;
using System.Text;

namespace YTMVPN_Server
{
    class LogHelper
    {
        public static void Logging(string str)
        {
            Console.WriteLine("{0:G} {1}", DateTime.Now, str);
        }

        public static void Logging(string str, ConsoleColor ForegroundColor = ConsoleColor.White, ConsoleColor BackgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
            Console.WriteLine("{0:G} {1}", DateTime.Now, str);
        }
    }
}
