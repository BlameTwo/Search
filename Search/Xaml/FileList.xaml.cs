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
        public FileList(string path2)
        {
            InitializeComponent();
            path = path2;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                FilePath.Text = "文件路径" + path;
                FileInfo fileinfo = new FileInfo(path);
                FileTime.Text = "修改时间:" + fileinfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                ShowIcon();
                FileName.Text ="文件名：" + System.IO.Path.GetFileName(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            this.BeginAnimation(Window.OpacityProperty, dou);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation dou = new DoubleAnimation();
            Duration duration = new Duration(TimeSpan.FromSeconds(0.2));
            dou.From = this.Opacity; dou.To = 0.5; dou.Duration = duration;
            this.BeginAnimation(Window.OpacityProperty, dou);
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
