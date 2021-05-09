using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Search
{
    public class File
    {
        private static string CmdPath = @"C:\Windows\System32\cmd.exe";


        public List<string> GetValue(string name)
        {
            var list = new List<string>();
            string str = "";
            string str2 = name;
            string cmd = "es.exe " + str2 + " -sort-ascending"+ "&exit";

            using (Process p = new Process())
            {
                p.StartInfo.FileName = CmdPath;
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();                                  //启动程序
                p.StandardInput.AutoFlush = true;
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.AutoFlush = true;
                
                //output = p.StandardOutput.ReadToEnd();
                //p.WaitForExit();//等待程序执行完退出进程
                int i = 0;
                bool falge = false;
                string abc;
                abc = p.StandardOutput.ReadToEnd();
                while (true)
                {
                    i++;
                    if (i >= 4)
                    {
                        falge = true;
                        // string abc = p.StandardOutput.ReadToEnd();
                        string[] sArray = abc.Split(new char[2] { '\r', '\n' });
                        for (int s = 4; s < sArray.Length; s++)
                        {
                            string a = sArray[s];
                            if (String.IsNullOrWhiteSpace(a) == false)
                            {
                                list.Add(sArray[s]);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        break;
                    }
                }
                p.Close();
            }
            return list;
        }
    }
}
