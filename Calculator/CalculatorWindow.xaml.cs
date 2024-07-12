using PluginAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

namespace Calculator
{
    /// <summary>
    /// CalculatorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CalculatorWindow : Window
    {
        string BackgroundColor = "0,0,0,0";
        string WorkAreaColor = "0,0,0,0";
        int Size = 50;
        string Bind = "Alt";
        int BindValue = 0;

        CalculatorCore core = new CalculatorCore();

        bool isShow = false;

        Color backgroundColor;
        Color workAreaColor;
        Color backgroundColor_Transparent;
        Color workAreaColor_Transparent;

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out System.Drawing.Point lpPoint);
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr pInstance, int threadID);
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(IntPtr pHookHandle);
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        internal struct Keyboard_LL_Hook_Data
        {
            public UInt32 vkCode;
            public UInt32 scanCode;
            public UInt32 flags;
            public UInt32 time;
            public IntPtr extraInfo;
        }

        private static IntPtr pKeyboardHook = IntPtr.Zero;

        public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
        private static HookProc keyboardHookProc;

        private int keyboardHookCallback(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
            {
                return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            }

            Keyboard_LL_Hook_Data khd = (Keyboard_LL_Hook_Data)Marshal.PtrToStructure(lParam, typeof(Keyboard_LL_Hook_Data));
            //Trace.Write($"key event:{wParam}, key code:{khd.vkCode}, event time:{khd.time}\n");              //在控制台输出当前键盘事件

            if (khd.vkCode == BindValue && wParam == new IntPtr(257))
            {
                ChangeShowState();
                System.Drawing.Point MousePose = new System.Drawing.Point();
                GetCursorPos(out MousePose);
                Top = MousePose.Y;
                Left = MousePose.X;
                Results_TextBox.Text = "0";
                Formula_TextBox.Text = null;
                One_Button.IsEnabled = true;
                Two_Button.IsEnabled = true;
                Three_Button.IsEnabled = true;
                Four_Button.IsEnabled = true;
                Five_Button.IsEnabled = true;
                Six_Button.IsEnabled = true;
                Seven_Button.IsEnabled = true;
                Eight_Button.IsEnabled = true;
                Nine_Button.IsEnabled = true;
                Zero_Button.IsEnabled = true;
                Amount_Button.IsEnabled = true;
                Addition_Button.IsEnabled = true;
                Subtraction_Button.IsEnabled = true;
                Multiplication_Button.IsEnabled = true;
                Division_Button.IsEnabled = true;
                Point_Button.IsEnabled = true;
            }
            return 0;
        }

        private bool InsertKeyboardHook()
        {
            if (pKeyboardHook == IntPtr.Zero)
            {
                keyboardHookProc = keyboardHookCallback;
                pKeyboardHook = SetWindowsHookEx(13,        //13表示全局键盘事件。
                    keyboardHookProc,
                    (IntPtr)0,
                    0);

                if (pKeyboardHook == IntPtr.Zero)
                {
                    removeKeyboardHook();
                    return false;
                }
            }

            return true;
        }

        private bool removeKeyboardHook()
        {
            if (pKeyboardHook != IntPtr.Zero)
            {
                if (UnhookWindowsHookEx(pKeyboardHook))
                {
                    pKeyboardHook = IntPtr.Zero;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        internal bool InsertHook()
        {
            bool iRet;
            iRet = InsertKeyboardHook();
            if (!iRet)
            {
                return false;
            }
            return true;
        }

        public void ChangeShowState()
        {
            if (isShow) { this.Hide(); isShow = false; }
            else { this.Show(); isShow = true; }
        }

        public CalculatorWindow()
        {
            InitializeComponent();
            InsertHook();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Drawing.Point MousePose = new System.Drawing.Point();
            GetCursorPos(out MousePose);
            Top = MousePose.Y; 
            Left = MousePose.X;
        }

        public void GetConfig(string BackgroundColor, string WorkAreaColor, int Size ,string Bind)
        {
            this.BackgroundColor = BackgroundColor;
            this.WorkAreaColor = WorkAreaColor;
            this.Size = Size;
            this.Bind = Bind;

            backgroundColor = Color.FromArgb(
                255,
                (byte)Tools.StringToARGB(BackgroundColor)[1],
                (byte)Tools.StringToARGB(BackgroundColor)[2],
                (byte)Tools.StringToARGB(BackgroundColor)[3]);

            workAreaColor = Color.FromArgb(
                255,
                (byte)Tools.StringToARGB(WorkAreaColor)[1],
                (byte)Tools.StringToARGB(WorkAreaColor)[2],
                (byte)Tools.StringToARGB(WorkAreaColor)[3]);

            backgroundColor_Transparent = Color.FromArgb(
                70,
                (byte)Tools.StringToARGB(BackgroundColor)[1],
                (byte)Tools.StringToARGB(BackgroundColor)[2],
                (byte)Tools.StringToARGB(BackgroundColor)[3]);

            workAreaColor_Transparent = Color.FromArgb(
                70,
                (byte)Tools.StringToARGB(WorkAreaColor)[1],
                (byte)Tools.StringToARGB(WorkAreaColor)[2],
                (byte)Tools.StringToARGB(WorkAreaColor)[3]);

            this.Height = 278 * (Size * 0.02);
            this.Width = 202 * (Size * 0.02);

            BindValue = KeyCode.StringToKeyCode(Bind);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            switch (btn.Tag.ToString()) 
            {
                case "Zero":
                case "One":
                case "Two":
                case "Three":
                case "Four":
                case "Five":
                case "Six":
                case "Seven":
                case "Eight":
                case "Nine":
                case "Point":
                    NumberButton(btn.Tag.ToString());
                    break;
                case "Addition":
                case "Subtraction":
                case "Multiplication":
                case "Division":
                    OperatorButton(btn.Tag.ToString());
                    break;
                case "Amount":
                    GetAmount();
                    break;
            }
        }

        private void NumberButton(string value) 
        {
            try
            {
                switch (value)
                {
                    case "Zero":
                        this.Results_TextBox.Text += "0";
                        break;
                    case "One":
                        this.Results_TextBox.Text += "1";
                        Results_TextBox.Text = Convert.ToDouble(Results_TextBox.Text).ToString();
                        break;
                    case "Two":
                        this.Results_TextBox.Text += "2";
                        Results_TextBox.Text = Convert.ToDouble(Results_TextBox.Text).ToString();
                        break;
                    case "Three":
                        this.Results_TextBox.Text += "3";
                        Results_TextBox.Text = Convert.ToDouble(Results_TextBox.Text).ToString();
                        break;
                    case "Four":
                        this.Results_TextBox.Text += "4";
                        Results_TextBox.Text = Convert.ToDouble(Results_TextBox.Text).ToString();
                        break;
                    case "Five":
                        this.Results_TextBox.Text += "5";
                        Results_TextBox.Text = Convert.ToDouble(Results_TextBox.Text).ToString();
                        break;
                    case "Six":
                        this.Results_TextBox.Text += "6";
                        Results_TextBox.Text = Convert.ToDouble(Results_TextBox.Text).ToString();
                        break;
                    case "Seven":
                        this.Results_TextBox.Text += "7";
                        Results_TextBox.Text = Convert.ToDouble(Results_TextBox.Text).ToString();
                        break;
                    case "Eight":
                        this.Results_TextBox.Text += "8";
                        Results_TextBox.Text = Convert.ToDouble(Results_TextBox.Text).ToString();
                        break;
                    case "Nine":
                        this.Results_TextBox.Text += "9";
                        Results_TextBox.Text = Convert.ToDouble(Results_TextBox.Text).ToString();
                        break;
                    case "Point":
                        this.Results_TextBox.Text += ".";
                        break;
                }
            }
            catch { }
        }

        private void OperatorButton(string value) 
        {
            core.setA(Convert.ToDouble(Results_TextBox.Text));
            Formula_TextBox.Text += Results_TextBox.Text;
            switch (value) 
            {
                case "Addition":
                    core.@operator = CalculatorCore.Operator.Addition;
                    Formula_TextBox.Text += " + ";
                    break;
                case "Subtraction":
                    core.@operator = CalculatorCore.Operator.Subtraction;
                    Formula_TextBox.Text += " - ";
                    break;
                case "Multiplication":
                    core.@operator = CalculatorCore.Operator.Multiplication;
                    Formula_TextBox.Text += " × ";
                    break;
                case "Division":
                    core.@operator = CalculatorCore.Operator.Division;
                    Formula_TextBox.Text += " ÷ ";
                    break;
            }
            Addition_Button.IsEnabled = false;
            Subtraction_Button.IsEnabled = false;
            Multiplication_Button.IsEnabled = false;
            Division_Button.IsEnabled = false;
            Results_TextBox.Text = "0";
        }

        private void GetAmount()
        {
            core.setB(Convert.ToDouble(Results_TextBox.Text));
            Formula_TextBox.Text += Results_TextBox.Text + " = ";
            Results_TextBox.Text = core.Calculating().ToString();
            One_Button.IsEnabled = false;
            Two_Button.IsEnabled = false;
            Three_Button.IsEnabled = false;
            Four_Button.IsEnabled = false;
            Five_Button.IsEnabled = false;
            Six_Button.IsEnabled = false;
            Seven_Button.IsEnabled = false;
            Eight_Button.IsEnabled = false;
            Nine_Button.IsEnabled = false;
            Zero_Button.IsEnabled = false;
            Amount_Button.IsEnabled = false;
            Point_Button.IsEnabled = false;
        }

        private void tackWindowsTransparent()
        {
            this.Background = new SolidColorBrush(backgroundColor_Transparent);
            Workspace.Background = new SolidColorBrush(workAreaColor_Transparent);
            GC.Collect();
        }

        private void tackBackWindow()
        {
            this.Background = new SolidColorBrush(backgroundColor);
            Workspace.Background = new SolidColorBrush(workAreaColor);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            tackBackWindow();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            tackWindowsTransparent();
        }

        private void Workspace_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                tackWindowsTransparent();
                this.DragMove();
            }
            if (e.LeftButton == MouseButtonState.Released)
            {
                tackBackWindow();
            }
        }

        private void Workspace_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tackBackWindow();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = !removeKeyboardHook();
            base.OnClosed(e);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C) 
            {
                Results_TextBox.Text = "0";
                Formula_TextBox.Text = null;
                One_Button.IsEnabled = true;
                Two_Button.IsEnabled = true;
                Three_Button.IsEnabled = true;
                Four_Button.IsEnabled = true;
                Five_Button.IsEnabled = true;
                Six_Button.IsEnabled = true;
                Seven_Button.IsEnabled = true;
                Eight_Button.IsEnabled = true;
                Nine_Button.IsEnabled = true;
                Zero_Button.IsEnabled = true;
                Amount_Button.IsEnabled = true; 
                Addition_Button.IsEnabled = true;
                Subtraction_Button.IsEnabled = true;
                Multiplication_Button.IsEnabled = true;
                Division_Button.IsEnabled = true;
                Point_Button.IsEnabled = true;
            }
        }

        private void Results_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Results_TextBox.Text.Length <= 5) 
            {
                Results_TextBox.FontSize = 40;
            }
            else if(Results_TextBox.Text.Length <= 15) 
            {
                Results_TextBox.FontSize = 40 - Results_TextBox.Text.Length * 2;
            }
            else
            {
                Results_TextBox.FontSize = 10;
            }

        }
    }
}
