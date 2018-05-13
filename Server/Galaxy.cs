using System;
using System.Collections.Generic;
using System.Net;

namespace UPDServer
{
    class Galaxy
    {
        private String starLocations = "";
        private String blackholeLocations = "";
        private String treasureLocations = "";
        private String planetLocations = "";
        private int playerCount;
        private Dictionary<int, Char> cells = new Dictionary<int, Char>();
        private Dictionary<String, int> players = new Dictionary<String, int>();

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
        
        public void removePlayer(String id)
        {
            players.Remove(id);
        }

        public String getPlayers()
        {
            String playersList = "";
            foreach (String s in players.Keys)
            {
                playersList = "," + players[s].ToString();
            }
            playersList = playersList.Substring(1);
            return playersList;
        }

        public void editCell(int cellNum, Char value)
        {
            cells[cellNum] = value;
        }
        public int PlayerCount { get => playerCount; set => playerCount = value; }
    }
}
