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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using BaiDuFanYi;
using System.Threading;

namespace Search
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Forms.ToolStripMenuItem exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        System.Windows.Forms.ToolStripMenuItem exitMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
        private NotifyIcon notifyIcon = null;
        public MainWindow()         /*构造函数*/
        {
            InitializeComponent();
            SetNotifyIcon();
            contextMenu();
        }
        BaiDuFanYi.FanYi fanyi = new FanYi();

        /// <summary>
        /// 创建系统托盘图标
        /// </summary>
        public void SetNotifyIcon()
        {
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.Text = "快搜";//最小化到托盘时，鼠标在图标上显示的文本

            System.Drawing.Icon icon1 = new System.Drawing.Icon(System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/Picture/123.ico")).Stream);//程序图标，这里需要改成你程序中图标的路径，注意图片的格式。
            this.notifyIcon.Icon = icon1;//指定图标
            this.notifyIcon.Visible = true;//初始时不可见
            this.notifyIcon.DoubleClick += NotifyIcon_DoubleClick; ; ;
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
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
        
        #region 托盘右键菜单
        /// <summary>
        /// 托盘图标菜单及事件
        /// </summary>
        private void contextMenu()
        {
            
            ContextMenuStrip cms = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip = cms;
            exitMenuItem.Text = "移除启动项";
            exitMenuItem2.Text = "退出";
            exitMenuItem2.Click += ExitMenuItem_Click;
            exitMenuItem.Click += ExitMenuItem_Click1; 
            cms.Items.Add(exitMenuItem);
            cms.Items.Add(exitMenuItem2);
            
            
        }
        #endregion

        private void ExitMenuItem_Click1(object sender, EventArgs e)
        {
            string StartPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
            if (!System.IO.File.Exists(StartPath + "\\快搜.lnk") && exitMenuItem.Text.ToString() == "添加启动项")
            {
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShellClass();

                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(StartPath + "\\快搜.lnk");
                shortcut.TargetPath = AppDomain.CurrentDomain.BaseDirectory + "Search.exe";

                shortcut.Description = "快捷搜索的快捷方式";
                shortcut.IconLocation = AppDomain.CurrentDomain.BaseDirectory + "Search.exe";
                shortcut.Save();
                exitMenuItem.Text = "移除启动项";
            }
            else if (System.IO.File.Exists(StartPath + "\\快搜.lnk") && exitMenuItem.Text.ToString() == "移除启动项")
            {
                System.IO.File.Delete(StartPath + "\\快搜.lnk");
                exitMenuItem.Text = "添加启动项";
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Icon.Dispose();
            Environment.Exit(0);
        }

        Search.File file = new File();
        SearchMath searchmath = new SearchMath();
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)         //主要逻辑，以后重构代码的时候的重点
        {
            if (e.Key == Key.Enter)
            {
                if (SearchText1.Text == "")
                {
                    SearchText1.Text = "请输入关键字或者命令";
                }
                else
                {
                    if (SearchText1.Text.Substring(0, 3) == "bd ")
                    {
                        string def = file.GetText(SearchText1.Text.Substring(3));
                        Process.Start("https://www.baidu.com/s?wd=" + def + "&tn=baidulocal");
                    }
                    else if (SearchText1.Text.Substring(0, 3) == "bg ")
                    {
                        string def = file.GetText(SearchText1.Text.Substring(3));
                        Process.Start("https://cn.bing.com/search?q=" + def);
                    }
                    else if (SearchText1.Text.Substring(0, 3) == "gg ")
                    {
                        string def = file.GetText(SearchText1.Text.Substring(3));
                        Process.Start("http://www.google.com//search?q=" + def);
                    }
                    else if (SearchText1.Text.Substring(0, 3) == "ff ")
                    {
                        List<string> list = new List<string>();
                        list = file.GetValue(SearchText1.Text.Substring(3), " -dm ", " -sort name-ascending ", "&exit");
                        FileShow fileshow = new FileShow(list, SearchText1.Text.Substring(3));
                        fileshow.Com1.SelectedIndex = 0;
                        fileshow.Show();
                    }
                    else if (SearchText1.Text.Substring(0, 3) == "fy ")
                    {
                        SearchText1.Text = file.GetBaiduText(SearchText1.Text.Substring(3));
                    }
                    else if (SearchText1.Text.Substring(0, 4) == "end ")
                    {
                        this.Close();
                    }
                    else if (SearchText1.Text.Substring(0, 4) == "sqr ")
                    {
                        try
                        {
                            SearchText1.Text = Math.Sqrt(int.Parse(SearchText1.Text.Substring(4))).ToString();
                        }
                        catch (Exception)
                        {
                            SearchText1.Text = "";
                        }
                    }
                    else if (SearchText1.Text.Substring(0, 4) == "sum ")
                    {
                        try
                        {
                            SearchText1.Text = searchmath.GetSum(SearchText1.Text.Substring(4)).ToString();
                        }
                        catch (Exception)
                        {
                            SearchText1.Text = "";
                        }

                    }
                    else if (SearchText1.Text.Substring(0, 4) == "help")
                    {
                        Process.Start(AppDomain.CurrentDomain.BaseDirectory + "help\\Help.html");
                    }
                    else if (SearchText1.Text.Substring(0, 5) == "aver ")
                    {
                        SearchText1.Text = searchmath.GetAverage(SearchText1.Text.Substring(5)).ToString();
                    }
                    else if (SearchText1.Text.Substring(0, 5) == "sqrs ")
                    {
                        SearchText1.Text = searchmath.GetSqrs(SearchText1.Text.Substring(5)).ToString();
                    }
                    
                    else if (SearchText1.Text.Substring(0, 5) == "reff ")
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "Date\\Everything.exe";
                        p.StartInfo.Arguments = "-close -admin";
                        p.Start();
                    }
                    
                    else if (SearchText1.Text.Substring(0, 11) == "Style-Back ")
                    {
                        Search.Cs.KeyDown keydown = new Cs.KeyDown();
                        Grid1.Background = keydown.GetSolid(SearchText1.Text.Substring(11));
                    }
                    
                    else
                    {
                        Process.Start("https://www.baidu.com/s?wd=" + SearchText1.Text + "&tn=baidulocal");
                    }
                }
            }
        }




        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        int windowswidth, windowsheight;
        WindowBlur blur = new WindowBlur();

        void WindowStyle()
        {
            Version currentVersion = Environment.OSVersion.Version;
            if (currentVersion.Major == 10)
            {
                blur.SetIsEnabled(this, true);
            }
            else
            {
                Grid1.Background = new SolidColorBrush(Colors.White);
                Grid1.Opacity = 1;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowStyle();
            string StartPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
            if (System.IO.File.Exists(StartPath+@"\快搜.lnk"))
            {
                exitMenuItem.Text = "移除启动项";
            }
            else
            {
                exitMenuItem.Text = "添加启动项";
            }
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
            SearchText1.Focus();
        }

        private void SearchText1_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SearchText1.Cursor = System.Windows.Input.Cursors.IBeam;
        }


        private void SearchText1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchText1.Text.Length == 3)               //两个个字符的命令
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
                    SVG.Source = new Uri(@"pack://application:,,,/Picture/谷歌.svg");
                }
                else if (abc == "ff ")
                {
                    SVG.Source = new Uri(@"pack://application:,,,/Picture/file.svg");
                }else if(abc =="fy ")
                {
                    SVG.Source = new Uri(@"pack://application:,,,/Svg/fanyi.svg");
                }
                else
                {
                    SVG.Source = new Uri(@"pack://application:,,,/Picture/放大镜.svg");
                }
            }
            if (SearchText1.Text.Length == 4)               //三个字符的命令
            {
                string abc = SearchText1.Text.Substring(0, 4);
                if (abc == "sqr " || abc == "sum ")
                {
                    SVG.Source = new Uri(@"pack://application:,,,/Svg/MathSvg.svg");
                }else if (abc == "end ")                //退出命令
                {
                    SVG.Source = new Uri(@"pack://application:,,,/Svg/end.svg");
                }
                else if (abc == "help")
                {
                    SVG.Source = new Uri(@"pack://application:,,,/Svg/help.svg");
                }
            }
            if (SearchText1.Text.Length == 5)
            {
                string abc = SearchText1.Text.Substring(0, 5);
                if (abc == "aver ")
                {
                    SVG.Source = new Uri(@"pack://application:,,,/Svg/MathSvg.svg");
                }else if (abc =="sqrs ")
                {
                    SVG.Source = new Uri(@"pack://application:,,,/Svg/MathSvg.svg");
                }
            }
            if (SearchText1.Text.Length == 11)
            {
                string abc = SearchText1.Text.Substring(0, 11);
                if (abc == "Style-Back ")
                {
                    SVG.Source = new Uri(@"pack://application:,,,/Svg/Setting.svg");
                }
            }
        }
    }
}
