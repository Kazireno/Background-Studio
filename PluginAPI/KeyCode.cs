using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginAPI
{
    public static class KeyCode
    {
        /// <summary>
        /// 部分按键对应虚拟键值
        /// </summary>
        public enum KeyCodes : byte
        {
            /// <summary>
            /// BACKSPACE 键
            /// </summary>
            BACKSPACE = 0x08,
            /// <summary>
            /// Tab 键
            /// </summary>
            TAB = 0x09,
            /// <summary>
            /// CLEAR 键
            /// </summary>
            CLEAR = 0x0C,
            /// <summary>
            /// Enter 键
            /// </summary>
            Enter = 0x0D,
            /// <summary>
            /// ESC 键
            /// </summary>
            ESC = 0x1B,
            /// <summary>
            /// 0
            /// </summary>
            ZERO = 0x30,
            /// <summary>
            /// 1
            /// </summary>
            ONE = 0x31,
            /// <summary>
            /// 2
            /// </summary>
            TWO = 0x32,
            /// <summary>
            /// 3
            /// </summary>
            THREE = 0x33,
            /// <summary>
            /// 4
            /// </summary>
            FOUR = 0x34,
            /// <summary>
            /// 5
            /// </summary>
            FIVE = 0x35,
            /// <summary>
            /// 6
            /// </summary>
            SIX = 0x36,
            /// <summary>
            /// 7
            /// </summary>
            SEVEN = 0x37,
            /// <summary>
            /// 8
            /// </summary>
            EIGHT = 0x38,
            /// <summary>
            /// 9
            /// </summary>
            NIGHT = 0x39,
            /// <summary>
            /// A 键
            /// </summary>
            A = 0x41,
            /// <summary>
            /// B 键
            /// </summary>
            B = 0x42,
            /// <summary>
            /// C 键
            /// </summary>
            C = 0x43,
            /// <summary>
            /// D 键
            /// </summary>
            D = 0x44,
            /// <summary>
            /// E 键
            /// </summary>
            E = 0x45,
            /// <summary>
            /// F 键
            /// </summary>
            F = 0x46,
            /// <summary>
            /// G 键
            /// </summary>
            G = 0x47,
            /// <summary>
            /// H 键
            /// </summary>
            H = 0x48,
            /// <summary>
            /// I 键
            /// </summary>
            I = 0x49,
            /// <summary>
            /// J 键
            /// </summary>
            J = 0x4A,
            /// <summary>
            /// K 键
            /// </summary>
            K = 0x4B,
            /// <summary>
            /// L 键
            /// </summary>
            L = 0x4C,
            /// <summary>
            /// M 键
            /// </summary>
            M = 0x4D,
            /// <summary>
            /// N 键
            /// </summary>
            N = 0x4E,
            /// <summary>
            /// O 键
            /// </summary>
            O = 0x4F,
            /// <summary>
            /// P 键
            /// </summary>
            P = 0x50,
            /// <summary>
            /// Q 键
            /// </summary>
            Q = 0x51,
            /// <summary>
            /// R 键
            /// </summary>
            R = 0x52,
            /// <summary>
            /// S 键
            /// </summary>
            S = 0x53,
            /// <summary>
            /// T 键
            /// </summary>
            T = 0x54,
            /// <summary>
            /// U 键
            /// </summary>
            U = 0x55,
            /// <summary>
            /// V 键
            /// </summary>
            V = 0x56,
            /// <summary>
            /// W 键
            /// </summary>
            W = 0x57,
            /// <summary>
            /// X 键
            /// </summary>
            X = 0x58,
            /// <summary>
            /// Y 键
            /// </summary>
            Y = 0x59,
            /// <summary>
            /// Z 键
            /// </summary>
            Z = 0x5A,
            /// <summary>
            /// 乘号键
            /// </summary>
            MULTIPLY = 0x6A,
            /// <summary>
            /// 加号键
            /// </summary>
            ADD = 0x6B,
            /// <summary>
            /// 分隔符键
            /// </summary>
            SEPARATOR = 0x6C,
            /// <summary>
            /// 减号键
            /// </summary>
            SUBTRACT = 0x6D,
            /// <summary>
            /// 句点键
            /// </summary>
            DECIMAL = 0x6E,
            /// <summary>
            /// 除号键
            /// </summary>
            DIVIDE = 0x6F,
            /// <summary>
            /// F1 键
            /// </summary>
            F1 = 0x70,
            /// <summary>
            /// F2 键
            /// </summary>
            F2 = 0x71,
            /// <summary>
            /// F3 键
            /// </summary>
            F3 = 0x72,
            /// <summary>
            /// F4 键
            /// </summary>
            F4 = 0x73,
            /// <summary>
            /// F5 键
            /// </summary>
            F5 = 0x74,
            /// <summary>
            /// F6 键
            /// </summary>
            F6 = 0x75,
            /// <summary>
            /// F7 键
            /// </summary>
            F7 = 0x76,
            /// <summary>
            /// F8 键
            /// </summary>
            F8 = 0x77,
            /// <summary>
            /// F9 键
            /// </summary>
            F9 = 0x78,
            /// <summary>
            /// F10 键
            /// </summary>
            F10 = 0x79,
            /// <summary>
            /// F11 键
            /// </summary>
            F11 = 0x7A,
            /// <summary>
            /// F12 键
            /// </summary>
            F12 = 0x7B,
            /// <summary>
            /// F13 键
            /// </summary>
            F13 = 0x7C,
            /// <summary>
            /// F14 键
            /// </summary>
            F14 = 0x7D,
            /// <summary>
            /// F15 键
            /// </summary>
            F15 = 0x7E,
            /// <summary>
            /// F16 键
            /// </summary>
            F16 = 0x7F,
            /// <summary>
            /// F17 键
            /// </summary>
            F17 = 0x80,
            /// <summary>
            /// F18 键
            /// </summary>
            F18 = 0x81,
            /// <summary>
            /// F19 键
            /// </summary>
            F19 = 0x82,
            /// <summary>
            /// F20 键
            /// </summary>
            F20 = 0x83,
            /// <summary>
            /// F21 键
            /// </summary>
            F21 = 0x84,
            /// <summary>
            /// F22 键
            /// </summary>
            F22 = 0x85,
            /// <summary>
            /// F23 键
            /// </summary>
            F23 = 0x86,
            /// <summary>
            /// F24 键
            /// </summary>
            F24 = 0x87,
            /// <summary>
            /// 左 SHIFT 键
            /// </summary>
            LSHIFT = 0xA0,
            /// <summary>
            /// 右 SHIFT 键
            /// </summary>
            RSHIFT = 0xA1,
            /// <summary>
            /// 左 Ctrl 键
            /// </summary>
            LCTRL = 0xA2,
            /// <summary>
            /// 右 Ctrl 键
            /// </summary>
            RCTRL = 0xA3,
            /// <summary>
            /// 左 ALT 键
            /// </summary>
            LALT = 0xA4,
            /// <summary>
            /// 右 ALT 键
            /// </summary>
            RALT = 0xA5,
        }

        public static int StringToKeyCode(string value) 
        {
            try
            {
                return (byte)Enum.Parse(typeof(KeyCodes), value.ToUpper());
            }
            catch { return 0; }
        }
    }
}
