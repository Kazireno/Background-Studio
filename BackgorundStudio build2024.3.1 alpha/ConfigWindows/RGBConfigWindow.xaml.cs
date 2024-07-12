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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BackgorundStudio_build2024._3._1_alpha.ConfigWindows
{
    /// <summary>
    /// RGBConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RGBConfigWindow : Window
    {
        int R = 0;
        int G = 0;
        int B = 0;
        int A = 0;

        public delegate void GetTextHandler(string value);
        public GetTextHandler getTextHandler;

        public RGBConfigWindow(string value)
        {
            InitializeComponent();

            A = PresetConfigString.StringToARGB(value)[0];
            R = PresetConfigString.StringToARGB(value)[1];
            G = PresetConfigString.StringToARGB(value)[2];
            B = PresetConfigString.StringToARGB(value)[3];

            A_TextBox.Text = A.ToString();
            R_TextBox.Text = R.ToString();
            G_TextBox.Text = G.ToString();
            B_TextBox.Text = B.ToString();
            A_Slider.Value = A;
            R_Slider.Value = R;
            G_Slider.Value = G;
            B_Slider.Value = B;

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

        private void R_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (R_Slider == null) return;
                R_Slider.Value = Convert.ToInt32(R_TextBox.Text);
                R = (int)R_Slider.Value;
                ColorViewerChange();
            }
            catch { }
        }

        private void G_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (G_Slider == null) return;
                G_Slider.Value = Convert.ToInt32(G_TextBox.Text);
                G = (int)G_Slider.Value;
                ColorViewerChange();
            }
            catch { }
        }

        private void B_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (B_Slider == null) return;
                B_Slider.Value = Convert.ToInt32(B_TextBox.Text);
                B = (int)B_Slider.Value;
                ColorViewerChange();
            }
            catch { }
        }

        private void A_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (A_Slider == null) return;
            A_Slider.Value = Convert.ToInt32(A_TextBox.Text);
            A = (int)A_Slider.Value;
            ColorViewerChange();
        }

        private void R_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (R_TextBox == null) return;
            R_TextBox.Text = R_Slider.Value.ToString();
            R = (int)R_Slider.Value;
            ColorViewerChange();
        }

        private void G_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (G_TextBox == null) return;
            G_TextBox.Text = G_Slider.Value.ToString();
            G = (int)G_Slider.Value;
            ColorViewerChange();
        }

        private void B_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (B_TextBox == null) return;
            B_TextBox.Text = B_Slider.Value.ToString();
            B = (int)B_Slider.Value;
            ColorViewerChange();
        }

        private void A_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (A_TextBox == null) return;
            A_TextBox.Text = A_Slider.Value.ToString();
            A = (int)A_Slider.Value;
            ColorViewerChange();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            getTextHandler("");
            GC.Collect();
            this.Close();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            getTextHandler(PresetConfigString.ARGBToString(A, R, G, B));
            GC.Collect();
            this.Close();
        }

        private void ColorViewerChange()
        {
            ColorViewer.Fill = new SolidColorBrush(Color.FromArgb((byte)A, (byte)R, (byte)G, (byte)B));
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

        private void tackWindowsTransparent()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Menu.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Workspace.Background = new SolidColorBrush(Color.FromArgb(70, 128, 128, 128));
            R_TextBox.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            G_TextBox.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            B_TextBox.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            A_TextBox.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Apply.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            Cancel.Background = new SolidColorBrush(Color.FromArgb(70, 72, 71, 71));
            GC.Collect();
        }

        private void tackBackWindow()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Menu.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Workspace.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            R_TextBox.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            G_TextBox.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            B_TextBox.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            A_TextBox.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Apply.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
            Cancel.Background = new SolidColorBrush(Color.FromArgb(255, 72, 71, 71));
        }
    }
}
