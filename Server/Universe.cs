using System;
using System.Collections.Generic;
using System.Timers;

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

        public void updateWeps()
        {
            foreach(String id in galaxies.Keys)
            {
               for (int i = 0; i < galaxies[id].getNumBullets(); i++)
                {
                    Weapons wep = galaxies[id].getWeapon(i);
                    //Torp speed = 1 cell every 400 ms
                    //Phasor speed = 1 cell every 200 ms
                    if (wep.WeaponType == 't')
                    {
                        int offset = (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - wep.Time) / 400;
                        wep.Offset = offset;
                    }
                    else
                    {
                        int offset = (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - wep.Time) / 200;
                        wep.Offset = offset;
                    }
                    int x = 0;
                    int y = 0;
                    switch (wep.Angle)
                    {
                        case 'n':
                            x = wep.Col;
                            y = wep.Row;
                            y -= wep.Offset;
                            break;
                        case 'e':
                            x = wep.Col;
                            y = wep.Row;
                            x += wep.Offset;
                            break;
                        case 's':
                            x = wep.Col;
                            y = wep.Row;
                            y += wep.Offset;
                            break;
                        case 'w':
                            x = wep.Col;
                            y = wep.Row;
                            x -= wep.Offset;
                            break;
                    }
                    List<Player> players = galaxies[id].getSectorPlayers();
                    for (int j = 0; i < players.Count; i++) 
                    {
                        if (players[j].Row == wep.Row && players[j].Column == wep.Col)
                        {
                            if (players[j].ShieldOn)
                            {
                                players[j].ShieldOn = false;
                                players[j].shields--;
                            }
                            else {
                                if (wep.WeaponType == 't')
                                {
                                    players[j].Health -= 15;
                                } else
                                {
                                    players[j].Health -= 5;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
