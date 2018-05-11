using System;
using System.Collections.Generic;

namespace UPDServer {
    class Universe {
        private Dictionary<String, Galaxy> galaxies = new Dictionary<String, Galaxy>();

        public Universe() {

            char sec = 'A';
            while (sec != 'Q') {
                for (int i = 0; i < 16; i++) {
                    galaxies.Add(sec + "-" + i, new Galaxy());
                }
                sec++;
            }
        }
        public Galaxy getGalaxy(String sectorID)
        {
            return galaxies[sectorID];
        }
    }
}
