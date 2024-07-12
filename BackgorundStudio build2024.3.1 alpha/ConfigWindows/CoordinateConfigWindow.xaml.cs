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
    /// CoordinateConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CoordinateConfigWindow : Window
    {
        private int CoordinateX = 0;
        private int CoordinateY = 0;

        public delegate void GetTextHandler(string value);
        public GetTextHandler getTextHandler;

        public CoordinateConfigWindow(string config)
        {
            InitializeComponent();
            CoordinateX_Slider.Maximum = SystemParameters.WorkArea.Width;
            CoordinateY_Slider.Maximum = SystemParameters.WorkArea.Height;
            CoordinateX = PresetConfigString.GetCoordinateX(config);
            CoordinateY = PresetConfigString.GetCoordinateY(config);
            CoordinateX_TextBox.Text = CoordinateX.ToString();
            CoordinateY_TextBox.Text = CoordinateY.ToString();

            this.Top = System.Windows.Forms.Control.MousePosition.Y;
            this.Left = System.Windows.Forms.Control.MousePosition.X;
        }

        private void CoordinateX_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CoordinateX = Convert.ToInt32(CoordinateX_TextBox.Text);
            if (CoordinateX_Slider != null) CoordinateX_Slider.Value = CoordinateX;
        }

        private void CoordinateY_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CoordinateY = Convert.ToInt32(CoordinateY_TextBox.Text);
            if (CoordinateY_Slider != null) CoordinateY_Slider.Value = CoordinateY;
        }

        private void CoordinateX_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CoordinateX = (int)CoordinateX_Slider.Value;
            if (CoordinateX_TextBox != null) CoordinateX_TextBox.Text = CoordinateX.ToString();
        }

        private void CoordinateY_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CoordinateY = (int)CoordinateY_Slider.Value;
            if (CoordinateY_TextBox != null) CoordinateY_TextBox.Text = CoordinateY.ToString();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            getTextHandler(PresetConfigString.IntToCoordinate(CoordinateX, CoordinateY));
            GC.Collect();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            getTextHandler("");
            GC.Collect();
            this.Close();
        }

        private void tackWindowsTransparent()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Menu.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Workspace.Background = new SolidColorBrush(Color.FromArgb(70, 128, 128, 128));
            Apply.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Cancel.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            CoordinateX_TextBox.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            CoordinateY_TextBox.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            GC.Collect();
        }

        private void tackBackWindow()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Menu.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Workspace.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            Apply.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Cancel.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            CoordinateX_TextBox.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            CoordinateY_TextBox.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            tackBackWindow();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            tackWindowsTransparent();
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

        private void Menu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tackBackWindow();
        }
    }
}
