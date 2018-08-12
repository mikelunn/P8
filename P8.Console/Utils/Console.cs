using System;
using System.Collections.Generic;
using System.Text;

namespace P8.Console.Utils
{
    public static class Console
    {
		public static void Write(string message, bool newLine = true)
		{
			System.Console.WriteLine(message, newLine ? Environment.NewLine : null);
		}
		public static string Read()
		{
			return System.Console.ReadLine();
		}
	

    }
}
