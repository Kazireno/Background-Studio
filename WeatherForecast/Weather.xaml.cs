using PluginAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Threading;

namespace WeatherForecast
{
    /// <summary>
    /// Weather.xaml 的交互逻辑
    /// </summary>
    public partial class Weather : Window
    {
        string CorrCoordinate = "0-0";
        int Size = 50;
        string RGB = "0,0,0,0";
        string _City = "北京";

        public Weather()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getInfo();
            DispatcherTimer dayTimer = new DispatcherTimer();
            dayTimer.Interval = TimeSpan.FromMinutes(30); ;
            dayTimer.Tick += new EventHandler(getinfo);
            dayTimer.Start();
        }

        public void getConfig(string CorrCoordinate, int Size, string RGB, string City)
        {
            ResourceDictionary resourceDictionary = this.Resources;

            this.CorrCoordinate = CorrCoordinate;
            this.Size = Size;
            this.RGB = RGB;
            this._City = City;

            this.Top = Tools.GetCoordinateY(CorrCoordinate);
            this.Left = Tools.GetCoordinateX(CorrCoordinate);

            this.Height = 80 * (Size * 0.02);
            this.Width = 320 * (Size * 0.02);

            resourceDictionary["Brush"] = new SolidColorBrush(Color.FromArgb((byte)Tools.StringToARGB(RGB)[0], (byte)Tools.StringToARGB(RGB)[1], (byte)Tools.StringToARGB(RGB)[2], (byte)Tools.StringToARGB(RGB)[3]));
            this.City.Foreground = new SolidColorBrush(Color.FromArgb((byte)Tools.StringToARGB(RGB)[0], (byte)Tools.StringToARGB(RGB)[1], (byte)Tools.StringToARGB(RGB)[2], (byte)Tools.StringToARGB(RGB)[3]));
            this.Tip.Foreground = new SolidColorBrush(Color.FromArgb((byte)Tools.StringToARGB(RGB)[0], (byte)Tools.StringToARGB(RGB)[1], (byte)Tools.StringToARGB(RGB)[2], (byte)Tools.StringToARGB(RGB)[3]));
            this.Temp.Foreground = new SolidColorBrush(Color.FromArgb((byte)Tools.StringToARGB(RGB)[0], (byte)Tools.StringToARGB(RGB)[1], (byte)Tools.StringToARGB(RGB)[2], (byte)Tools.StringToARGB(RGB)[3]));
            this.Warn.Foreground = new SolidColorBrush(Color.FromArgb((byte)Tools.StringToARGB(RGB)[0], (byte)Tools.StringToARGB(RGB)[1], (byte)Tools.StringToARGB(RGB)[2], (byte)Tools.StringToARGB(RGB)[3]));
            getInfo();
        }


        private void getinfo(object sender, EventArgs e) 
        {
            getInfo();
        }
        private void getInfo()
        {
            HttpWebAPI httpWebAPI = new HttpWebAPI();
            string ICO = "";

            if (!httpWebAPI.SetCityId(_City)) return;
            if (!httpWebAPI.SetTemp()) return;

            this.City.Content = httpWebAPI.GetCity();
            this.Temp.Content = httpWebAPI.GetTemp() + "℃";
            this.Warn.Content = httpWebAPI.GetText();

            ICO = "_" + httpWebAPI.GetICO() + "DrawingImage";
            img.Source = (DrawingImage)this.FindResource(ICO);

            if (httpWebAPI.SetWarn()) this.Warn.Content += "\n" + httpWebAPI.GetWarns();
            else this.Warn.Content += "\n" + "无气象预警。";

            this.Tip.Content = "更新于：" + DateTime.Now.ToString("f");
        }
    }
}
