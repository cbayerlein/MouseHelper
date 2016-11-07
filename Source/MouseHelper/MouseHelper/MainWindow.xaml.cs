using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace MouseHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static App myapp;

        public MainWindow(App app)
        {
            myapp = app;
            InitializeComponent();
        }

        public new void Show()
        {
            Top = System.Windows.SystemParameters.WorkArea.Top;
            Height = System.Windows.SystemParameters.WorkArea.Height;
            Left =  System.Windows.SystemParameters.WorkArea.Width - 300;
            Width = 300;
            NamesList.ItemsSource = myapp.Helper.AccessElements.Values;
            base.Show();
            txtIn.Focus();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;

            if (lb == null) return;

            AccessElement element = lb.SelectedItem as AccessElement;
            int id = element.Id;
            txtIn.Text = id.ToString();
            myapp.Overlay.ColorRect(id);
            
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            myapp.Overlay.Topmost = true;
            this.Topmost = true;
            this.Activate();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            myapp.Cleanup();
        }

        private void ValidateTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+"); //regex that matches disallowed text
            bool isAllowed = regex.IsMatch(e.Text);
            myapp.Overlay.Visibility = Visibility.Hidden;
            e.Handled = isAllowed; 
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                int number = 0;
                if (int.TryParse(txtIn.Text, out number))
                {
                   if (myapp.Helper.InvokeElement(number))
                    {
                        myapp.Cleanup();
                    }
                }
                txtIn.Focus();
                txtIn.Text = "";
            }
        }
    }
}
