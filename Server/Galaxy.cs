using System;
using System.Collections.Generic;

namespace UPDServer {
    class Galaxy {

        private char star;
        private char blackhole;
        private char treasure;
        private char planet;
        private Dictionary<String, Char> cells = new Dictionary<String, char>();

        public Galaxy() {
            Random rnd = new Random();
            char sec = 'a';
            while (sec != 'k') {
                for (int i = 0; i < 9; i++) {
                    cells.Add(sec + "" + i, (char)rnd.Next(33,126));

                }
                sec++;
            }
        }

        public Dictionary<String, Char> Getgalaxy() {
            return cells;
        }

        public char GetCell(string sec) {
            return cells[sec];
        }  

        public char Star { get => star; set => star = value; }
        public char Blackhole { get => blackhole; set => blackhole = value; }
        public char Treasure { get => treasure; set => treasure = value; }
        public char Planet { get => planet; set => planet = value; }
    }
}
