using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPDServer {
    class Galaxy {

        private char star;
        private char blackhole;
        private char treasure;
        private char planet;
        private char[] cells = new char[100];

        public Galaxy() {
            Random rnd = new Random();

            for (int i = 0; i < 99; i++) {
                cells[i] = (char) rnd.Next(30, 127);
            }

        }

        public char GetCell(int location) {
            return cells[location];
        }  

        public char Star { get => star; set => star = value; }
        public char Blackhole { get => blackhole; set => blackhole = value; }
        public char Treasure { get => treasure; set => treasure = value; }
        public char Planet { get => planet; set => planet = value; }
    }
}
