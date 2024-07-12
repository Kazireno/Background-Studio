using BackgorundStudio_build2024._3._1_alpha.Tools.Strings;
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
    /// ResolutionConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ResolutionConfigWindow : Window
    {
        private int ResolutionX = 0;
        private int ResolutionY = 0;

        public delegate void GetTextHandler(string value);
        public GetTextHandler getTextHandler;

        public ResolutionConfigWindow(string config)
        {
            InitializeComponent();
            ResolutionX_Slider.Maximum = SystemParameters.PrimaryScreenWidth;
            ResolutionY_Slider.Maximum = SystemParameters.PrimaryScreenHeight;
            ResolutionX = PresetConfigString.GetResolutionX(config);
            ResolutionY = PresetConfigString.GetResolutionY(config);
            ResolutionX_TextBox.Text = ResolutionX.ToString();
            ResolutionY_TextBox.Text = ResolutionY.ToString();

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

        private void Menu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tackBackWindow();
        }

        private void Menu_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            getTextHandler(PresetConfigString.IntToResolution(ResolutionX, ResolutionY));
            GC.Collect();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            getTextHandler("");
            GC.Collect();
            this.Close();
        }

        private void ResolutionX_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ResolutionX = (int)ResolutionX_Slider.Value;
            if (ResolutionX_TextBox != null) ResolutionX_TextBox.Text = ResolutionX.ToString();
        }

        private void ResolutionY_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ResolutionY = (int)ResolutionY_Slider.Value;
            if (ResolutionY_TextBox != null) ResolutionY_TextBox.Text = ResolutionY.ToString();
        }

        private void ResolutionX_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ResolutionX = Convert.ToInt32(ResolutionX_TextBox.Text);
            if (ResolutionX_Slider != null) ResolutionX_Slider.Value = ResolutionX;
        }

        private void ResolutionY_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ResolutionY = Convert.ToInt32(ResolutionY_TextBox.Text);
            if (ResolutionY_Slider != null) ResolutionY_Slider.Value = ResolutionY;
        }

        private void tackWindowsTransparent()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Menu.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Workspace.Background = new SolidColorBrush(Color.FromArgb(70, 128, 128, 128));
            Apply.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Cancel.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            ResolutionX_TextBox.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            ResolutionY_TextBox.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            GC.Collect();
        }

        private void tackBackWindow()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Menu.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Workspace.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            Apply.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Cancel.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            ResolutionX_TextBox.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            ResolutionY_TextBox.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
        }
    }
}
