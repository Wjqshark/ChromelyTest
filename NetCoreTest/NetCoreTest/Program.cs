using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;

namespace NetCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            StartProcessMethod();
            StartProcessMethod();
            Console.ReadLine();
        }


        private static void StartProcessMethod()
        {
            var process = new Process();

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ChromelyTest.exe");
            if (File.Exists(path))
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.Arguments = "aaaaaaaa cccccccccccccc";
                process.StartInfo.ErrorDialog = true;
                process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                process.StartInfo.Verb = "runas";
                process.StartInfo.FileName = path;
                process.Start();
            }
        }
    }
}
