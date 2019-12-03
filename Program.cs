using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            //%USERPROFILE%\AppData\Local\Packages
            string str = "%USERPROFILE%\\AppData\\Local\\Packages";
            str = Environment.ExpandEnvironmentVariables(str);
            DirectoryInfo dir = new DirectoryInfo(str);
            foreach (var temp in dir.GetDirectories())
            {
                str = "CheckNetIsolation.exe LoopbackExempt -a -n=\"" + temp.Name + "\"";
                Console.WriteLine(str);
                Control(str);
            }
            Console.WriteLine("Finish");
            Console.Read();
        }
        private static string Control(string str)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            p.Start();
            p.StandardInput.WriteLine(str + "&exit");
            p.StandardInput.AutoFlush = true;
            string output = p.StandardOutput.ReadToEnd();
            output += p.StandardError.ReadToEnd();
            p.WaitForExit();
            p.Close();
            return output + "\r\n";
        }
    }
}
