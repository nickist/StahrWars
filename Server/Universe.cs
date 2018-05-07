using System;
using System.Collections.Generic;

namespace UPDServer {
    class Universe {
        private Dictionary<String, Galaxy> galaxies = new Dictionary<String, Galaxy>();

        public Universe() {

            char sec = 'a';
            while (sec != 'q') {
                for (int i = 0; i < 16; i++) {
                    galaxies.Add(sec + "" + i, new Galaxy());
                }
                sec++;
            }
        }
        /* dont remember why I made this method
        public Dictionary<String, Char> getSector(String sec) {
            return galaxies[sec].Getgalaxy();
        }
        */
        


    }
}
