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

namespace BackgorundStudio_build2024._3._1_alpha.ConfigWindows
{
    /// <summary>
    /// CreatePreset.xaml 的交互逻辑
    /// </summary>
    public partial class CreatePreset : Window
    {
        public delegate void GetTextHandler(string value);
        public GetTextHandler getTextHandler;

        public CreatePreset()
        {
            InitializeComponent();

            this.Top = System.Windows.Forms.Control.MousePosition.Y;
            this.Left = System.Windows.Forms.Control.MousePosition.X;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            tackBackWindow();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            tackWindowsTransparent();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            getTextHandler(TextInput.Text);
            GC.Collect();
            this.Close();
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tackBackWindow();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point a = Mouse.GetPosition(this);
                if (a.Y <= 30)
                {
                    tackWindowsTransparent();
                    GC.Collect();
                    this.DragMove();
                }
            }
            if (e.LeftButton == MouseButtonState.Released)
            {
                tackBackWindow();
            }
        }

        private void tackWindowsTransparent()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Menu.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Workspace.Background = new SolidColorBrush(Color.FromArgb(70, 128, 128, 128));
            TextInput.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Apply.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Cancel.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            GC.Collect();
        }

        private void tackBackWindow()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Menu.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Workspace.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            TextInput.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Apply.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Cancel.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            getTextHandler("");
            GC.Collect();
            this.Close();
        }
    }
}
