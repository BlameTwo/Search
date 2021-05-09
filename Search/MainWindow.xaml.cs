using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Search
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Search.File file = new File();

        
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (SearchText1.Text == "")
                {
                    SearchText1.Tag = "请输入关键字或者命令";
                }
                else
                {
                    try
                    {
                        if (SearchText1.Text.Substring(0, 3) == "bd ")
                        {
                            string abc = SearchText1.Text.Substring(3);
                            Process.Start("https://www.baidu.com/s?wd=" + abc + "&tn=baidulocal");
                        }
                        else if (SearchText1.Text.Substring(0, 3) == "bg ")
                        {
                            string abc = SearchText1.Text.Substring(3);
                            Process.Start("https://cn.bing.com/search?q=" + abc);
                        }
                        else if (SearchText1.Text.Substring(0, 3) == "gg ")
                        {
                            string abc = SearchText1.Text.Substring(3);
                            Process.Start("http://www.google.com//search?q=" + abc);
                        }
                        else if (SearchText1.Text.Substring(0, 3) == "ff ")
                        {
                            List<string> list = new List<string>();
                            list = file.GetValue(SearchText1.Text.Substring(3));
                            FileShow fileshow = new FileShow(list, SearchText1.Text.Substring(3));
                            fileshow.Show();
                        }
                        else if (SearchText1.Text.Substring(0, 5) == "exit ")
                        {
                            this.Close();
                        }else if (SearchText1.Text.Substring(0, 4) == "sqr ")
                        {
                            SearchText1.Text = Math.Sqrt(int.Parse(SearchText1.Text.Substring(4))).ToString();
                        }
                        else
                        {
                            Process.Start("https://www.baidu.com/s?wd=" + SearchText1.Text + "&tn=baidulocal");
                        }
                    }
                    catch (Exception)
                    {
                        Process.Start("https://www.baidu.com/s?wd=" + SearchText1.Text + "&tn=baidulocal");
                    }
                }
            }
        }

        private void Window_LostFocus(object sender, RoutedEventArgs e)
        {
            NotifyIcon_Click(null, null);
        }

        private void NotifyIcon_Click(object sender, RoutedEventArgs e)
        {
            if (this.Visibility == Visibility.Hidden)
            {
                this.Visibility = Visibility.Visible;
                this.Activate();
            }
            else if (this.Visibility == Visibility.Visible)
            {
                this.Visibility = Visibility.Hidden;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        int windowswidth, windowsheight;



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            windowswidth = Screen.PrimaryScreen.Bounds.Width;
            windowsheight= Screen.PrimaryScreen.Bounds.Height;
            this.Top = windowsheight / 4 - this.ActualHeight*2;
            using (Process process = new Process())
            {
                process.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "Date\\Everything.exe";
                process.StartInfo.Arguments = "-close -admin";
                process.Start();
                process.Close();
            }
            
            SVG.Source = new Uri(@"pack://application:,,,/Picture/放大镜.svg");
        }

        private void SearchText1_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SearchText1.Cursor = System.Windows.Input.Cursors.IBeam;
        }


        private void SearchText1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchText1.Text.Length >= 3)
            {
                string abc = SearchText1.Text.Substring(0, 3);
                if (abc == "bd ")
                {
                    SVG.Source = new Uri(@"pack://application:,,,/Picture/百度.svg");
                }
                else if (abc == "bg ")
                {
                    SVG.Source = new Uri(@"pack://application:,,,/Picture/bing.svg");
                }
                else if (abc == "gg ")
                {
                    SVG.Source = new Uri( @"pack://application:,,,/Picture/谷歌.svg");
                }
                else if(abc == "ff ")
                {
                    SVG.Source = new Uri(@"pack://application:,,,/Picture/file.svg");
                }
                else
                {
                    SVG.Source = new Uri(@"pack://application:,,,/Picture/放大镜.svg");
                }
            }
            
        }

        private void Menutiem1_Click(object sender, RoutedEventArgs e)          /*添加启动项，和删除启动项*/
        {
            string StartPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
            if (!System.IO.File.Exists(StartPath+ "\\快搜.lnk") && Menutiem1.Header.ToString() =="添加启动项")
            {
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShellClass();

                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(StartPath + "\\快搜.lnk");
                shortcut.TargetPath = AppDomain.CurrentDomain.BaseDirectory + "Search.exe";

                shortcut.Description = "快捷搜索的快捷方式";
                shortcut.IconLocation = AppDomain.CurrentDomain.BaseDirectory + "Search.exe";
                shortcut.Save();
                Menutiem1.Header = "移除启动项";
            }
            else if(System.IO.File.Exists(StartPath + "\\快搜.lnk") && Menutiem1.Header.ToString() == "移除启动项")
            {
                System.IO.File.Delete(StartPath + "\\快搜.lnk");
                Menutiem1.Header = "添加启动项";
            }
        }
    }
}
