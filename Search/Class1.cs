using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using BaiDuFanYi;
namespace Search
{
    public class File
    {
        FanYi fanyi = new FanYi();
        private static string CmdPath = @"C:\Windows\System32\cmd.exe";
        string[] mf3 = { "!", "\"", "#", "$", "%", "&", "=", "(", ")" };              //输入字符对照表
        string[] mf4 = { "%21", "%22", "%23", "%24", "%25", "%26", "%27", "%28", "%29" };          //输出字符对照表

        public List<string> GetValue(string name, string dm, string sort, string exit)
        {
            var list = new List<string>();
            string str2 = name;
            string cmd = "es.exe "+dm + str2 + sort + exit;

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
                string abc;
                abc = p.StandardOutput.ReadToEnd();
                while (true)
                {
                    i++;
                    if (i >= 4)
                    {
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



        /// <summary>
        /// 替换有效字符
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public string GetText(string cmd)
        {
            string abc = "";
            int CmdCount = cmd.Length;
            string def = "";
            int s = 0;
            def = cmd.Substring(0, 1);
            //我真是太难了……，这个方法总共写了将近三个钟头
            try
            {
                while (s < cmd.Length)
                {
                    if (def == mf3[0] || def == mf3[1] || def == mf3[2] || def == mf3[3] || def == mf3[4] || def == mf3[5] || def == mf3[6] || def == mf3[7] || def == mf3[8])
                    {
                        for (int i = 0; i < mf3.Length - 1; i++)
                        {
                            if (def == mf3[i])
                            {
                                abc = abc + mf4[i];
                            }
                        }
                    }
                    else
                    {
                        abc = abc + def;
                    }
                    s++;
                    def = cmd.Substring(s, 1);
                }
            }
            catch (Exception) { } //因为到最后一次赋值def时，会报错，直接try掉，不影响
            return abc;
        }

        public string GetBaiduText(string text)
        {
            string Text2 = "";
            int a = text.IndexOf("("); int b = text.IndexOf(")");
            string str2 = text.Substring(a + 1, b - a - 1);           //先取出来括号里面的内容
            string[] strlist = str2.Split(',');
            return fanyi.LostTranslate(strlist[0], strlist[1], strlist[2]);
        }


        public void OpenFileDIog(string path)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();

            proc.EnableRaisingEvents = false;

            proc.StartInfo.FileName = "rundll32.exe";

            proc.StartInfo.Arguments = "shell32,OpenAs_RunDLL " + path;

            proc.Start();
        }
    }

    public class SearchMath
    {

        /// <summary>
        /// sum求和方法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public double GetSum(string str)
        {
            double Sum = 0;
            int a = str.IndexOf("("); int b = str.IndexOf(")");
            string str2 = str.Substring(a + 1, b - a - 1);           //先取出来括号里面的内容
            string[] list =  str2.Split(',');           //分割字符串
            for (int i = 0; i < list.Length; i++)           //按照数组的元素个数进行循环
            {
                Sum = Sum + double.Parse(list[i]);              //累加
            }
            return Sum;                 //返回结果
        }

        public double GetAverage(string str)
        {
            double average = 0;
            int a = str.IndexOf("(");       int b = str.IndexOf(")");       
            string str2 = str.Substring(a+1,b-a-1);           
            string[] strlist = str2.Split(',');
            double abc = 0;
            for (int i = 0; i < strlist.Length; i++)
            {
                abc = abc + double.Parse(strlist[i]);
            }
            average = abc / strlist.Length;              //把结果除以个数 
            return average;
        }

        public string GetSqrs(string str)
        {
            string sqrs = "";
            int a = str.IndexOf("("); int b = str.IndexOf(")");
            string str2 = str.Substring(a + 1, b - a - 1);           
            string[] strlist = str2.Split(',');
            for (int i = 0; i < strlist.Length; i++)
            {
                sqrs = sqrs +Math.Round(Math.Sqrt(double.Parse(strlist[i])),3).ToString()+"  和  ";              //直接输出字符串就ok了
            }
            return sqrs;
        }
    }
}
