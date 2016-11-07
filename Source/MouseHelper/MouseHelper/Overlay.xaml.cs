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
using System.Windows.Shapes;

namespace MouseHelper
{
    /// <summary>
    /// Interaktionslogik für Overlay.xaml
    /// </summary>
    public partial class Overlay : Window
    {
        App myapp;

        public Overlay(App app)
        {
            myapp = app;
            InitializeComponent();
        }

        public new void Show()
        {
            AccessHelper helper = myapp.Helper;
            Width = helper.Target.Current.BoundingRectangle.Width;
            Height = helper.Target.Current.BoundingRectangle.Height;
            Top = helper.Target.Current.BoundingRectangle.Top;
            Left = helper.Target.Current.BoundingRectangle.Left;

            foreach (KeyValuePair<int, AccessElement> entry in helper.AccessElements)
            {
                AccessElement element = entry.Value;
                if ((element.Width > 0) && (element.Height > 0))
                {
                    System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();
                    rect.Width = element.Width;
                    rect.Height = element.Height;
                    rect.Fill = Brushes.Black;
                    rect.Opacity = .5;
                    rect.Name = "rect_"+element.Id.ToString();

                    TextBlock number = new TextBlock();
                    number.Background = Brushes.White;
                    number.Foreground = Brushes.Black;
                    number.VerticalAlignment = VerticalAlignment.Center;
                    number.HorizontalAlignment = HorizontalAlignment.Center;
                    number.TextAlignment = TextAlignment.Center;
                    number.Width = 20;
                    number.Height = 16;
                    number.Text = element.Id.ToString();
                    number.Name = "number_"+element.Id.ToString();

                    Canvas.SetLeft(rect, element.Left - Left);
                    Canvas.SetTop(rect, element.Top - Top);
                    canvas.Children.Add(rect);
                    Canvas.SetLeft(number, Canvas.GetLeft(rect));
                    Canvas.SetTop(number, Canvas.GetTop(rect));
                    canvas.Children.Add(number);

                }
            }
            base.Show();
        }

        internal void ClearCanvas()
        {
            canvas.Children.Clear();
        }

        internal void ColorRect(int id)
        {
            foreach (UIElement item in canvas.Children)
            {
                if (item.GetType().Equals(typeof(System.Windows.Shapes.Rectangle)))
                {
                    System.Windows.Shapes.Rectangle rect = item as System.Windows.Shapes.Rectangle;
                    if (rect.Name == "rect_"+id.ToString())
                    {
                        rect.Fill = Brushes.Blue;
                    } else
                    {
                        rect.Fill = Brushes.Black;
                    }
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            myapp.Cleanup();
        }
    }
}
