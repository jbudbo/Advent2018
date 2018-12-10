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
                var captures = Regex.Match(line, "#(\\d+).@.(\\d{1,4}),(\\d{1,4}):.(\\d{1,4})x(\\d{1,4})").Groups;

                var newRect = new Rect(double.Parse(captures[2].Value), double.Parse(captures[3].Value), double.Parse(captures[4].Value), double.Parse(captures[5].Value));

                Rectangle rect = new Rectangle
                {
                    StrokeThickness = 1,
                    Stroke = new SolidColorBrush(Colors.Black),
                    //Fill = new SolidColorBrush(Colors.White),
                    //Opacity = .1,
                    Width = double.Parse(captures[4].Value),
                    Height = double.Parse(captures[5].Value)
                };

                Canvas.SetTop(rect, double.Parse(captures[2].Value));
                Canvas.SetLeft(rect, double.Parse(captures[3].Value));

                c.Children.Add(rect);

            }
        }
    }
}