using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Search.Cs
{
    class KeyDown
    {
        public SolidColorBrush GetSolid(string text)
        {
            int a = text.IndexOf("("); int b = text.IndexOf(")");
            string str2 = text.Substring(a + 1, b - a - 1);           //先取出来括号里面的内容
            string[] list = str2.Split(',');           //分割字符串
            int Red = int.Parse(list[0]);
            int Green =int.Parse( list[1]);
            int Blue =int.Parse( list[2]);
            SolidColorBrush solid = new SolidColorBrush();
            solid.Color =  Color.FromRgb((byte)Red, (byte)Green,(byte)Blue);
            solid.Opacity = 0.3;
            return solid;
        }
    }
}