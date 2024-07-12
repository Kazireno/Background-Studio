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
    /// NumberConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PercentageConfigWindow : Window
    {
        int value = 0;
        public delegate void GetTextHandler(int value);
        public GetTextHandler getTextHandler;

        public PercentageConfigWindow(int Value)
        {
            InitializeComponent();
            value = Value;

            this.Top = System.Windows.Forms.Control.MousePosition.Y;
            this.Left = System.Windows.Forms.Control.MousePosition.X;
        }

        private void Menu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                tackWindowsTransparent();
                GC.Collect();
                this.DragMove();

            }
            if (e.LeftButton == MouseButtonState.Released)
            {
                tackBackWindow();
            }
        }

        private void Menu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tackBackWindow();
        }

        private void Value_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.value = (int)Value_Slider.Value;
            Value_TextBox.Text = value.ToString();
        }

        private void Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            getTextHandler(value);
            GC.Collect();
            this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            tackBackWindow();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            tackWindowsTransparent();
        }

        private void tackWindowsTransparent()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Menu.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Workspace.Background = new SolidColorBrush(Color.FromArgb(70, 128, 128, 128));
            Value_TextBox.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Apply_Button.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            GC.Collect();
        }

        private void tackBackWindow()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Menu.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Workspace.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            Value_TextBox.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Apply_Button.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Value_Slider.Value = value;
            Value_TextBox.Text = value.ToString();
        }

        private void Value_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value = Convert.ToInt32(Value_TextBox.Text);
            if (value <= 100 && Value_Slider != null) Value_Slider.Value = value;
        }
    }
}
