using System;
using System.Collections.Generic;

namespace UPDServer {
    class Universe {
        private Dictionary<String, Galaxy> galaxies = new Dictionary<String, Galaxy>();
        Random rnd = new Random();
        public Universe() {

            char sec = 'a';
            while (sec != 'q') {
                for (int i = 0; i < 16; i++) {
                    galaxies.Add(sec + "" + i, new Galaxy(rnd));
                }
                sec++;
            }
        }
        public Galaxy getGalaxy(String sectorID)
        {
            return galaxies[sectorID];
        }

        public int getGalaxySize() {
            return galaxies.Count;
        }

    }
}
