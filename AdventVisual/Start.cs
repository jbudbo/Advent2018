using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows.Shapes;
using a
namespace AdventVisual
{
    class Start : Application
    {
        static Window w;
        static Canvas c;

        [STAThread]
        static void Main(string[] args)
        {
            

            c = new Canvas { Width = 1000, Height = 1000, RenderSize = new Size(1000, 1000) };

            RenderOptions.SetBitmapScalingMode(c, BitmapScalingMode.Fant);
            RenderOptions.SetEdgeMode(c, EdgeMode.Unspecified);

            w = new Window
            {
                Content = c,
                SizeToContent = SizeToContent.WidthAndHeight
            };// { Width = 1000, Height = 1000 };
            w.Show();

            c.HorizontalAlignment = HorizontalAlignment.Left;
            c.VerticalAlignment = VerticalAlignment.Top;

            Init(File.ReadAllLines("D:\\AoC\\Advent2018\\day3.txt"));

            Application app = new Application();
            app.Run();
        }

        private static void Init(string[] lines)
        {
            Dictionary<int, Rect> plots = new Dictionary<int, Rect>();

            foreach (var line in lines)
            {
                var captures = Regex.Match(line, "#(\\d+).@.(\\d{1,3}),(\\d{1,3}):.(\\d{1,2})x(\\d{1,2})").Groups;

                var x = double.Parse(captures[2].Value);
                var y = double.Parse(captures[3].Value);
                var w = double.Parse(captures[4].Value);
                var h = double.Parse(captures[5].Value);


                Rectangle rect = new Rectangle
                {
                    StrokeThickness = 1,
                    Stroke = new SolidColorBrush(Colors.Black),
                    //Fill = new SolidColorBrush(Colors.White),
                    //Opacity = .1,
                    Width = w,
                    Height = h
                };

                Canvas.SetTop(rect, y);
                Canvas.SetLeft(rect, x);

                rect.MouseDown += (s,e) =>
                {
                    MessageBox.Show(captures[1].Value);
                };

                c.Children.Add(rect);
            }
        }
        
    }
}