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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;
using System.Windows.Media.Animation;
using System.Diagnostics;
namespace Search.Xaml
{
    /// <summary>
    /// FileList.xaml 的交互逻辑
    /// </summary>
    public partial class FileList : UserControl
    {
        string path;
        Search.File file = new File();
        int ListCount2 = 0;
        string time2;
        public FileList(string path2,int ListCount,string Time)
        {
            InitializeComponent();
            ListCount2 = ListCount;
            path = path2;
            time2 = Time;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                FilePath.Text = "文件路径" + path.TrimStart();
                FileInfo fileinfo = new FileInfo(path);
                FileTime.Text = "修改时间:" + time2.TrimStart();
                FileCount.Text  =   ListCount2.ToString();
                ShowIcon();
                FileName.Text ="文件名：" + System.IO.Path.GetFileName(path);
            }
            catch (Exception){}
        }

        void ShowIcon()
        {
            if (System.IO.Path.GetExtension(path) == ".docx" || System.IO.Path.GetExtension(path) == ".doc")
            {
                FileImage.Source = new Uri(@"pack://application:,,,/Svg/word.svg");
            }else if (System.IO.Path.GetExtension(path) == ".xlsx" || System.IO.Path.GetExtension(path) == ".xls")
            {
                FileImage.Source = new Uri(@"pack://application:,,,/Svg/xlsx.svg");
            }else if(System.IO.Path.GetExtension(path) == ".pptx" || System.IO.Path.GetExtension(path) == ".ppt")
            {
                FileImage.Source = new Uri(@"pack://application:,,,/Svg/pptx.svg");
            }else if (System.IO.Path.GetExtension(path) == ".exe" || System.IO.Path.GetExtension(path) == ".com")
            {
                FileImage.Source = new Uri(@"pack://application:,,,/Svg/exe.svg");
            }else if(System.IO.Path.GetExtension(path) == ".dll")
            {
                FileImage.Source = new Uri(@"pack://application:,,,/Svg/dll.svg");
            }else if(System.IO.Directory.Exists(path) == true)
            {
                FileImage.Source = new Uri(@"pack://application:,,,/Svg/dir.svg");
            }
            else
            {
                FileImage.Source = new Uri(@"pack://application:,,,/Svg/file.svg");
            }
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            DoubleAnimation dou = new DoubleAnimation();
            Duration duration = new Duration(TimeSpan.FromSeconds(0.2));
            dou.From = this.Opacity; dou.To = 1; dou.Duration = duration;
            Border1.BeginAnimation(Border.OpacityProperty, dou);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation dou = new DoubleAnimation();
            Duration duration = new Duration(TimeSpan.FromSeconds(0.2));
            dou.From = this.Opacity; dou.To = 0.5; dou.Duration = duration;
            Border1.BeginAnimation(Border.OpacityProperty, dou);
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Process.Start(path);
        }

        private void MiFullScreen_Click(object sender, RoutedEventArgs e)
        {
             Process.Start(path);
        }

        private void MiPlay_Click(object sender, RoutedEventArgs e)                 /*调用资源管理器，并选定文件*/
        {
            string args = string.Format("/e, /select, \"{0}\"", path);
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "explorer";
            info.Arguments = args;
            Process.Start(info);
        }

        private void MyCopyFile_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetDataObject(path);
        }
        
        private void MyOpen_Click(object sender, RoutedEventArgs e)                 //调用系统打开方式
        {
            file.OpenFileDIog(path);
        }
    }
}
