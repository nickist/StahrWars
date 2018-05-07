using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPDServer {
    class Universe {

        Universe() {
            Dictionary<String, Galaxy> galaxies = new Dictionary<String, Galaxy>();

            char sec = 'A';
            while (sec != 'Q') {
                for (int i = 0; i < 16; i++) {
                    galaxies.Add(sec + "" + i, new Galaxy());

                }
                sec++;
            }
        }

        


    }
}
