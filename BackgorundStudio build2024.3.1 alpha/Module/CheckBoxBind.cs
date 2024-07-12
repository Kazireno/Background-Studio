using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgorundStudio_build2024._3._1_alpha.Module
{
    internal class CheckBoxBind
    {
        public string Name { get; set; }
        public bool Enable { get; set; }

        public CheckBoxBind(string name, bool enable)
        {
            Name = name;
            Enable = enable;
        }

        public override string ToString() 
        {
            return Name;
        }
    }
}
