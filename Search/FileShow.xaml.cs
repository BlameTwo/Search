using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Search.Xaml;
namespace Search
{
    /// <summary>
    /// FileShow.xaml 的交互逻辑
    /// </summary>
    public partial class FileShow : Window
    {
        List<string> list = new List<string>();
        string path = "";
        public FileShow(List<string> list2,string path2)
        {
            InitializeComponent();
            list = list2;
            path = path2;
        }

        int ScrollCount1 = 0;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           this.Title = "在全盘搜索：" + path + ",得到共计"+list.Count.ToString()+"个文件";
           this.Dispatcher.BeginInvoke(new Action(() => ActionShow()));
        }
        
        void ActionShow()
        {
            try
            {
                for (int i = ScrollCount1; i < ScrollCount1 + 10; i++)
                {
                    if (System.IO.File.Exists(list[i]) || System.IO.Directory.Exists(list[i]))
                    {
                        FileList filelist = new FileList(list[i]);
                        ListPanel.Children.Add(filelist);
                    }
                }
                ScrollCount1 = ScrollCount1 + 10;
            }
            catch (Exception ex)
            {
                Scroll1.ScrollChanged -= Scroll1_ScrollChanged;
                Border1.Height = 50; Border1.Width = 210;
                Border1.Visibility = Visibility.Visible;
                TT1.Text = "已经显示到底部了哦";

            }
        }


        public bool IsVerticalScrollBarAtButtom(ScrollViewer s)
        {
            bool isAtButtom = false;
            double dVer = s.VerticalOffset;
            double dViewport = s.ViewportHeight;
            double dExtent = s.ExtentHeight;
            if (dVer != 0)
            {
                if (dVer + dViewport == dExtent)
                {
                    isAtButtom = true;
                }
                else
                {
                    isAtButtom = false;
                }
            }
            else
            {
                isAtButtom = false;
            }
            if (s.VerticalScrollBarVisibility == ScrollBarVisibility.Disabled
              || s.VerticalScrollBarVisibility == ScrollBarVisibility.Hidden)
            {
                isAtButtom = true;
            }
            return isAtButtom;
        }

        private void Scroll1_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (IsVerticalScrollBarAtButtom(Scroll1) == true)
            {
                ActionShow();
            }
            else
            {
                Border1.Visibility = Visibility.Collapsed;
            }
        }
    }
}
