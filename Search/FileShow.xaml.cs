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
        public string dm { get; set; }
        public string sort { get; set; }
        public string exit { get; set; }
        List<string> list = new List<string>();
        string path = "";
        File file = new File();
        List<string> TimeList = new List<string>();
        List<string> PathList = new List<string>();
        string WinMAX = "M959.72 0H294.216a63.96 63.96 0 0 0-63.96 63.96v127.92H64.28A63.96 63.96 0 0 0 0.32 255.84V959.4a63.96 63.96 0 0 0 63.96 63.96h703.56a63.96 63.96 0 0 0 63.96-63.96V792.465h127.92a63.96 63.96 0 0 0 63.96-63.96V63.96A63.96 63.96 0 0 0 959.72 0zM767.84 728.505V959.4H64.28V255.84h703.56z m189.322 0H831.8V255.84a63.96 63.96 0 0 0-63.96-63.96H294.216V63.96H959.72z";
        string WinMin = "M960.7 234.6v554.7H64.3V234.6h896.4m64.3-64.3H0v683.3h1025V170.3z";
        public FileShow(List<string> list2,string path2)
        {
            InitializeComponent();
            list = list2;
            exit = "&exit";
            path = path2;
            StateChanged += MainWindow_StateChanged;
            
        }
       
        private void MainWindow_StateChanged(object sender, EventArgs e)            //窗口最大化和最小化控制图标事件
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.BorderThickness = new Thickness(7);
                this.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                MAXPath2.Data = Geometry.Parse(WinMAX);
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.BorderThickness = new Thickness(0.5);
                this.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 63, 227, 134));
                MAXPath2.Data = Geometry.Parse(WinMin);
            }
        }

        void load()
        {
            TimeList.Clear(); PathList.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                TimeList.Add(list[i].Substring(0,17));
                PathList.Add(list[i].Substring(17));
            }
        }

        int ScrollCount1 = 0;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReText.Text = "在全盘搜索：" + path + ",得到共计"+(list.Count-1).ToString()+"个文件";
        }
        
        void ActionShow()
        {
            try
            {
                for (int i = ScrollCount1; i < ScrollCount1 + 10; i++)
                {
                    if (System.IO.File.Exists(PathList[i]) || System.IO.Directory.Exists(PathList[i]))
                    {
                        FileList filelist = new FileList(PathList[i],i,TimeList[i]);
                        ListPanel.Children.Add(filelist);
                    }
                }
                ScrollCount1 = ScrollCount1 + 10;
            }
            catch (Exception)               //因为没有了文件，所有会报错，这里直接显示文件到了底部
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
            return isAtButtom;
        }

        private void Scroll1_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (IsVerticalScrollBarAtButtom(Scroll1) == true)
            {
                ActionShow();
            }
            else if(IsVerticalScrollBarAtButtom(Scroll1) == false)
            {
                Border1.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void End_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MaxClick(object sender, RoutedEventArgs e)          //最大化和还原按钮
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Com1.SelectedIndex)
            {
                case 0: { RemoveList(path," -dm ", " -sort name-ascending ","&exit"); break; }
                case 1: { RemoveList(path, " -dm ", " -sort name-descending ", "&exit"); break; }
                case 2: { RemoveList(path, " -dm ", " -sort size-ascending ", "&exit"); break; }
                case 3: { RemoveList(path, " -dm ", " -sort size-descending ", "&exit"); break; }
                case 4: { RemoveList(path, " -dm ", " -sort date-created-ascending ", "&exit"); break; }
                case 5: { RemoveList(path," -dm ", " -sort date-created-descending ", "&exit"); break; }
                case 6: { RemoveList(path," -dm ", " -sort path-ascending ", "&exit"); break; }
                case 7: { RemoveList(path," -dm ", " -sort path-descending ", "&exit"); break; }
                case 8: { RemoveList(path," -dm ", " -sort date-modified-ascending ", "&exit"); break; }
                case 9: { RemoveList(path, " -dm ", " -sort date-modified-descending ", "&exit"); break; }
                case 10: { RemoveList(path, " -dm ", " -date-recently-changed ", "&exit"); break; }
            }
        }

        void RemoveList(string path,string dm,string sort,string exit)
        {
            Scroll1.ScrollToTop();
            ListPanel.Children.Clear();
            Border1.Visibility = Visibility.Collapsed;
            TT1.Text = "";
            ScrollCount1 = 0;
            list = file.GetValue(path, dm, sort, exit);
            load();
            ActionShow();
        }
    }
}