using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread_vs_Single
{
    class Data1
    {
        public int nr { get; set; }
        public int lvl { get; set; }
        public int hp { get; set; }
        public int str { get; set; }
        public int agl { get; set; }
        public int vit { get; set; }
        public int dmg { get; set; }
        public int stam { get; set; }

        public Data1(int nr)
        {
            this.nr = nr;
            lvl = nr * 12;
            hp = nr * 55;
            str = nr * 44;
            agl = nr * 6;
            vit = nr * 87;
            dmg = nr * 4;
            stam = nr * 91;
        }
    }
}
