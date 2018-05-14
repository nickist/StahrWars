using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPDServer
{
    class Weapons
    {
        private Char weaponType;
        private Char angle;
        private int col;
        private int row;
        private String sector;
        private long time;
        private int offset;

        public Weapons(Char type, int col, int row, Char angle, String sector)
        {
            this.weaponType = type;
            this.col = col;
            this.row = row;
            this.angle = angle;
            this.sector = sector;
            this.time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            this.offset = 0;
        }

        public long Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }
        public Char Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
            }
        }
        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
            }
        }
        public int Col
        {
            get
            {
                return col;
            }
            set
            {
                col = value;
            }
        }
        public Char WeaponType
        {
            get
            {
                return weaponType;
            }
            set
            {
                weaponType = value;
            }
        }
        public int Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
            }
        }
    }
}
