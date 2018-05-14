using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace UPDServer
{
    class Galaxy
    {
        private String starLocations = "";
        private String blackholeLocations = "";
        private String treasureLocations = "";
        private String planetLocations = "";
        private Dictionary<int, Char> cells = new Dictionary<int, Char>();
        private Dictionary<String, int> players = new Dictionary<String, int>();
        private List<IPEndPoint> playerIPs = new List<IPEndPoint>();
        //private List<Weapons> bullets = new List<Weapons>();

        public Galaxy(Random rnd)
        {
            //Star = 1, blackhole = 2, treasure = 3, planet = 4, player = 5
            
            for (int i = 0; i < 100; i++)
            {
                int x = rnd.Next(0, 75);
                switch (x)
                {
                    case 1:
                        cells.Add(i, 's');
                        if (starLocations.Length == 0)
                        {
                            starLocations = i.ToString();
                        }
                        else
                        {
                            starLocations = starLocations + "," + i.ToString();
                        }
                        break;
                    case 2:
                        cells.Add(i, 'b');
                        if (blackholeLocations.Length == 0)
                        {
                            blackholeLocations = i.ToString();
                        }
                        else
                        {
                            blackholeLocations = blackholeLocations + "," + i.ToString();
                        }
                        break;
                    case 3:
                        cells.Add(i, 't');
                        if (treasureLocations.Length == 0)
                        {
                            treasureLocations = i.ToString();
                        }
                        else
                        {
                            treasureLocations = treasureLocations + "," + i;
                        }
                        break;
                    case 4:
                        cells.Add(i, 'p');
                        if (planetLocations.Length == 0)
                        {
                            planetLocations = i.ToString();
                        }
                        else
                        {
                            planetLocations = planetLocations + "," + i;
                        }
                        break;
                    default:
                        cells.Add(i, 'e'); //e represents an empty cell
                        break;
                }
            }
        }

        public Dictionary<int, Char> Getgalaxy()
        {
            return cells;
        }

        public char GetCell(int sec)
        {
            return cells[sec];
        }

        public String StarLocations
        {
            get
            {
                return starLocations;
            }

            set
            {
                starLocations = value;
            }
        }

        public String BlackholeLocations
        {
            get
            {
                return blackholeLocations;
            }

            set
            {
                blackholeLocations = value;
            }
        }

        public String PlanetLocations
        {
            get
            {
                return planetLocations;
            }

            set
            {
                planetLocations = value;
            }
        }

        public String TreasureLocations
        {
            get
            {
                return treasureLocations;
            }

            set
            {
                treasureLocations = value;
            }
        }

        public void updatePlayer(String id, int cell)
        {
            if (players.ContainsKey(id))
            {
                players[id] = (cell);
            }
            else
            {
                players.Add(id, cell);
            }
        }

        public int getPlayerCount()
        {
            return players.Count;
        }

        public void removePlayer(String id) {
            players.Remove(id);
        }

        public String getPlayersLocs()
        {
            String playersList = "";
            foreach (String s in players.Keys)
            {
                playersList = "," + players[s].ToString();
            }
            playersList = playersList.Substring(1);
            return playersList;
        }

        public void removePlanet(int cellNum)
        {

            List<String> cells = planetLocations.Split(',').ToList();
            cells.Remove(cellNum.ToString());
            planetLocations = string.Join(",", cells.ToArray());
        }

       /* public int getNumBullets()
        {
            return bullets.Count;
        }
        
        public Weapons getWeapon(int i)
        {
            return bullets.ElementAt(i);
        }

        public void removeWeapon(Weapons w)
        {
            bullets.Remove(w);
        }

        public string getWeaponLocations()
        {
            List<Weapons> outOfRange = new List<Weapons>();
            String locs = "";
            foreach (Weapons w in bullets)
            {
                int x = 1;
                int y = 1;
                switch(w.Angle)
                {
                    case 'n':
                        x = w.Col;
                        y = w.Row;
                        y -= w.Offset;
                        break;
                    case 'e':
                        x = w.Col;
                        y = w.Row;
                        x += w.Offset;
                        break;
                    case 's':
                        x = w.Col;
                        y = w.Row;
                        y += w.Offset;
                        break;
                    case 'w':
                        x = w.Col;
                        y = w.Row;
                        x -= w.Offset;
                        break;
                }
                if (x > 9 || x < 0 || y > 9 || y < 0)
                {
                    outOfRange.Add(w);
                }
                else
                {
                locs = locs + "," + (y*10 + x % 10).ToString();
                }
            }
            foreach(Weapons w in outOfRange)
            {
                bullets.Remove(w);
            }
            if (locs.Length == 0)
            {
                return locs;
            }
            else
            {
                return locs.Substring(1);
            }
        }

        public void addWeapon(Char type, int col, int row, Char angle, String sector)
        {
            bullets.Add(new Weapons(type, col, row, angle, sector));
        }*/

        public Dictionary<String, int> getPlayers()
        {
            return players;
        }
    }
}
