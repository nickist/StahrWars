using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace UPDServer {


    public struct Received
    {
        public IPEndPoint Sender;
        public string Message;
    }

    class Program
    {
        static Player p1 = new Player(""); //Add parameters later
        static Universe universe = new Universe();

        static void Main(string[] args)
        {
            //create a new server
            var server = new UdpListener();
            //create universe
            Random rnd = new Random();
            Dictionary<string, IPEndPoint> connections = new Dictionary<string, IPEndPoint>();
            Dictionary<string, Player> players = new Dictionary<string, Player>();
            bool sectorChanged = false;

            Console.WriteLine("============================================= Server");

            string[] parts;

            //start listening for messages and copy the messages back to the client
            Task.Factory.StartNew(async () => {
                while (true)
                {
                    var received = await server.Receive();
                    string msg = received.Message.ToString();
                    parts = msg.Split(':');
                    fixParts(parts);
                    Console.WriteLine(msg + " -- " + received.Sender.Address.MapToIPv4().ToString());

                    // Only add new connections to the list of clients
                    if (!connections.ContainsKey(received.Sender.Address.MapToIPv4().ToString()))
                    {
                        connections.Add(received.Sender.Address.MapToIPv4().ToString(), received.Sender);
                        p1 = new Player(""); //add parameters later
                        p1.Sector = rnd.Next(0, 255);
                        p1.IsAlive = true;
                        p1.Column = rnd.Next(0, 9);
                        p1.Row = rnd.Next(0, 9);
                        players.Add(received.Sender.Address.MapToIPv4().ToString(), p1);
                        p1.SectorStr = numToSectorID(p1.Sector);
                        server.Reply(String.Format("connected:true:{0}:{1}:{2}", p1.SectorStr, p1.Column, p1.Row), received.Sender);
                        Galaxy sector = universe.getGalaxy(p1.SectorStr);
                        server.Reply(String.Format("si:{0}:{1}:{2}", sector.StarLocations, sector.PlanetLocations, sector.BlackholeLocations), received.Sender);
                        sector.updatePlayer(Environment.UserName, (p1.Row*10+p1.Column%10));
                    }


                    string ret = "[Connected Users]";
                    if (received.Message.Equals("list"))
                    {
                        foreach (string s in connections.Keys)
                            ret += "\n>> " + s;
                        server.Reply(ret + "\n*****************", received.Sender);
                    }
                    else
                    {
                        // Okay, send message to everyone
                        //foreach (IPEndPoint ep in connections.Values)
                        //    server.Reply("[" + received.Sender.Address.ToString() + "] says: " + received.Message, ep);
                    }

                    if (received.Message == "quit")
                    {
                        connections.Remove(received.Sender.Address.ToString());     // Remove the IP Address from the list of connections
                    }
                    else
                    {
                        Player p;
                        players.TryGetValue(received.Sender.Address.MapToIPv4().ToString(), out p);
<<<<<<< HEAD
                        if (parts[0].Equals("mov"))
                        {
                            if (p.FuelPods != 0)
                            {
                                p.FuelPods--;
                                if (parts[1].Equals("n")) p.Row--;
                                else if (parts[1].Equals("s")) p.Row++;
                                else if (parts[1].Equals("e")) p.Column++;
                                else if (parts[1].Equals("w")) p.Column--;
                                Galaxy sector = universe.getGalaxy(p.SectorStr);
                                //Checks for moving to different sector
                                if (p.Row == -1)
                                {
                                    p.Sector -= 16;
                                    p.SectorStr = numToSectorID(p.Sector);
                                    p.Row = 9;
                                    sectorChanged = true;
                                    Galaxy newSector = universe.getGalaxy(p.SectorStr);
                                   // sector.removePlayer(p);
                                    //newSector.updatePlayer(p);
                                }
                                else if (p.Row == 10)
                                {
                                    p.Sector += 16;
                                    p.SectorStr = numToSectorID(p.Sector);
                                    p.Row = 0;
                                    sectorChanged = true;
                                    Galaxy newSector = universe.getGalaxy(p.SectorStr);
                                    //sector.removePlayer(p);
                                    //newSector.updatePlayer(p);
                                }
                                else if (p.Column == -1)
                                {
                                    p.Sector--;
                                    p.SectorStr = numToSectorID(p.Sector);
                                    p.Column = 9;
                                    sectorChanged = true;
                                    Galaxy newSector = universe.getGalaxy(p.SectorStr);
                                    //sector.removePlayer(p);
                                    //newSector.updatePlayer(p);
                                }
                                else if (p.Column == 10)
                                {
                                    p.Sector++;
                                    p.SectorStr = numToSectorID(p.Sector);
                                    p.Column = 0;
                                    sectorChanged = true;
                                    Galaxy newSector = universe.getGalaxy(p.SectorStr);
                                    //sector.removePlayer(p);
                                    //newSector.updatePlayer(p);
                                }
                                if (!sectorChanged)
                                {
                                    //sector.updatePlayer(p);
                                }
                                server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.SectorStr, p.Column, p.Row, parts[1], p.FuelPods), received.Sender);
                                
                                Char cellAction = onSpecialCell(p);
                                switch(cellAction)
                                {
                                    case 's': //Player is on a star
                                        p.Health = 0;
                                        //server.Reply("star:", received.Sender);
                                        server.Reply("loc:star", received.Sender);
                                        //server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.Health,p.Sheilds,p.Phasors, p.Torpedoes, p.FuelPods), received.Sender);
                                        break;
                                    case 'p'://Player is on a planet
                                        p.Health = 50;
                                        p.Sheilds = 15;
                                        p.Phasors = 50;
                                        p.Torpedoes = 10;
                                        p.FuelPods = 50;
                                        //server.Reply("planet:", received.Sender);
                                        server.Reply("loc:planet", received.Sender);
                                        //server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.Health, p.Sheilds, p.Phasors, p.Torpedoes, p.FuelPods), received.Sender);
                                        break;
                                    case 't':
                                        //Player found treasure
                                        break;
                                    case 'b':
                                        p.Sector = rnd.Next(0, 255);
=======
                        if (p1.IsAlive == false) {
                            server.Reply("You Are Dead!", received.Sender);
                        } else { 
                            if (parts[0].Equals("mov")) {
                                if (p.FuelPods != 0) {
                                    p.FuelPods--;
                                    if (parts[1].Equals("n")) p.Row--;
                                    else if (parts[1].Equals("s")) p.Row++;
                                    else if (parts[1].Equals("e")) p.Column++;
                                    else if (parts[1].Equals("w")) p.Column--;
                                    Galaxy sector = universe.getGalaxy(p.SectorStr);
                                    //Checks for moving to different sector
                                    if (p.Row == -1) {
                                        p.Sector -= 16;
>>>>>>> clientserver
                                        p.SectorStr = numToSectorID(p.Sector);
                                        p.Row = 9;
                                        sectorChanged = true;
                                        Galaxy newSector = universe.getGalaxy(p.SectorStr);
                                        // sector.removePlayer(p);
                                        //newSector.updatePlayer(p);
                                    } else if (p.Row == 10) {
                                        p.Sector += 16;
                                        p.SectorStr = numToSectorID(p.Sector);
                                        p.Row = 0;
                                        sectorChanged = true;
                                        Galaxy newSector = universe.getGalaxy(p.SectorStr);
                                        //sector.removePlayer(p);
                                        //newSector.updatePlayer(p);
                                    } else if (p.Column == -1) {
                                        p.Sector--;
                                        p.SectorStr = numToSectorID(p.Sector);
                                        p.Column = 9;
                                        sectorChanged = true;
                                        Galaxy newSector = universe.getGalaxy(p.SectorStr);
                                        //sector.removePlayer(p);
                                        //newSector.updatePlayer(p);
                                    } else if (p.Column == 10) {
                                        p.Sector++;
                                        p.SectorStr = numToSectorID(p.Sector);
                                        p.Column = 0;
                                        sectorChanged = true;
                                        Galaxy newSector = universe.getGalaxy(p.SectorStr);
                                        //sector.removePlayer(p);
                                        //newSector.updatePlayer(p);
                                    }
                                    if (!sectorChanged) {
                                        //sector.updatePlayer(p);
                                    }
                                    server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.SectorStr, p.Column, p.Row, parts[1], p.FuelPods), received.Sender);

                                    Char cellAction = onSpecialCell(p);
                                    switch (cellAction) {
                                        case 's': //Player is on a star
                                            p.Health = 0;
                                            p.FuelPods = 0;
                                            p.Phasors = 0;
                                            p.Torpedoes = 0;
                                            p.Sheilds = 0;
                                            server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.Health, p.Sheilds, p.Phasors, p.Torpedoes, p.FuelPods), received.Sender);
                                            break;
                                        case 'p'://Player is on a planet
                                            p.Health = 100;
                                            p.Sheilds = 15;
                                            p.Phasors = 50;
                                            p.Torpedoes = 10;
                                            p.FuelPods = 50;
                                            server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.Health, p.Sheilds, p.Phasors, p.Torpedoes, p.FuelPods), received.Sender);
                                            break;
                                        case 't':
                                            //Player found treasure
                                            break;
                                        case 'b': //player is on a blackhole
                                            p.Sector = rnd.Next(0, 255);
                                            p.SectorStr = numToSectorID(p.Sector);
                                            p.Column = rnd.Next(0, 9);
                                            p.Row = rnd.Next(0, 9);
                                            server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.SectorStr, p.Column, p.Row, p.Oriantation, p.FuelPods), received.Sender);
                                            sectorChanged = true;
                                            break;
                                    }

                                } else {

                                    server.Reply("Out of Fuelpods!", received.Sender);
                                    server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.Sector, p.Column, p.Row, parts[1], p.FuelPods), received.Sender);

                                    server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.SectorStr, p.Column, p.Row, parts[1], p.FuelPods), received.Sender);
                                }
                            } else if (parts[0].Equals("r")) {
                                if (parts[1].Equals("n")) {
                                    p.Oriantation = "n";
                                    server.Reply(String.Format("or:{0}", parts[1]), received.Sender);
                                } else if (parts[1].Equals("s")) {
                                    p.Oriantation = "s";
                                    server.Reply(String.Format("or:{0}", parts[1]), received.Sender);
                                } else if (parts[1].Equals("e")) {
                                    p.Oriantation = "e";
                                    server.Reply(String.Format("or:{0}", parts[1]), received.Sender);
                                } else if (parts[1].Equals("w")) {
                                    p.Oriantation = "w";
                                    server.Reply(String.Format("or:{0}", parts[1]), received.Sender);
                                }
                            } else if (parts[0].Equals("s")) {
                                if (parts[1].Equals("1")) {
                                    if (p.Sheilds != 0) {
                                        p.Sheilds--;
                                        p.ShieldOn = true;
                                        server.Reply(String.Format("sh:{0}", parts[1]), received.Sender);
                                    } else {
                                        parts[1] = "2";
                                        server.Reply(String.Format("sh:{0}", parts[1]), received.Sender);
                                    }
                                } else if (parts[1].Equals("0")) {
                                    p.ShieldOn = false;
                                    server.Reply(String.Format("sh:{0}", parts[1]), received.Sender);
                                }
                            } else if (parts[0].Equals("v")) {
                                string rply = "unv:";
                                    char sec = 'a';
                                    while (sec != 'q') {
                                        for (int i = 0; i < 16; i++) {
                                        rply += universe.getGalaxy(sec + "" + i).PlayerCount + ":";
                                        }
                                        sec++;
                                    }
                                server.Reply(rply, received.Sender);
                                
                           

                            } else if (parts[0].Equals("f")) {
                                if (parts[1].Equals("p")) {
                                    //Add code for handelig shooting phasors
                                    if (p.Phasors != 0) {
                                        server.Reply(String.Format("sh:{0}", parts[1]), received.Sender);
                                        p.Phasors--;
                                    } else {
                                        server.Reply("Out of Phasors!", received.Sender);
                                        server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.Sector, p.Column, p.Row, parts[1], p.Phasors), received.Sender);
                                    }
                                    server.Reply(String.Format("sh:{0}", parts[1]), received.Sender);
                                } else if (parts[1].Equals("t")) {
                                    //Add code for handeling shooting torpedo
                                    if (p.Torpedoes != 0) {
                                        p.Torpedoes--;


                                    } else {
                                        server.Reply("Out of Torpedos!", received.Sender);
                                        server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.Sector, p.Column, p.Row, parts[1], p.Torpedoes), received.Sender);
                                    }
                                    server.Reply(String.Format("sh:{0}", parts[1]), received.Sender);
                                }
                            } else if (parts[0].Equals("h")) {
                                if (p.FuelPods >= 5) {
                                    p.Sector = rnd.Next(0, 255);
                                    p.SectorStr = numToSectorID(p.Sector);
                                    p.Column = rnd.Next(0, 9);
                                    p.Row = rnd.Next(0, 9);
                                    p.FuelPods -= 5;
                                    server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.SectorStr, p.Column, p.Row, p.Oriantation, p.FuelPods), received.Sender);
                                    sectorChanged = true;
                                } else if (p.FuelPods > 0 && p.FuelPods < 5) { //Change this to a differnt reply in the future so user can be promted that they dont have enough fuel to hyperspace
                                    server.Reply("Not enough fuel", received.Sender);
                                    server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.Sector, p.Column, p.Row, p.Oriantation, p.FuelPods), received.Sender);
                                } else {
                                    server.Reply("Out of Fuel", received.Sender);
                                    server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.Sector, p.Column, p.Row, p.Oriantation, p.FuelPods), received.Sender);
                                    server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.SectorStr, p.Column, p.Row, p.Oriantation, p.FuelPods), received.Sender);
                                }
                           } else if (parts[0].Equals("ls")) {
                                server.Reply(String.Format("Health:{0}", p.Health), received.Sender);
                            }
                            if (sectorChanged) {
                                Galaxy sector = universe.getGalaxy(p.SectorStr);
                                server.Reply(String.Format("si:{0}:{1}:{2}", sector.StarLocations, sector.PlanetLocations, sector.BlackholeLocations), received.Sender);
                                sectorChanged = false;
                            }
                        }
                    }
                }
            });


            // Endless loop for user's to send messages to Client
            string read;
            do
            {
                read = Console.ReadLine();
                foreach (IPEndPoint ep in connections.Values)
                    server.Reply(read, ep);
            } while (read != "quit");
        }

        private static void fixParts(string[] parts)
        {
            for (int i = 0; i < parts.Length; i++)
                parts[i] = parts[i].Trim().ToLower();
        }

        private static string numToSectorID(int x)
        {
            Char col = (Char)((x % 16) + 97);
            return col.ToString() + (x / 16).ToString();
        }

        private static Char onSpecialCell(Player p)
        {
            Galaxy sector = universe.getGalaxy(p.SectorStr);
            int playerCell = (p.Row * 10) + (p.Column % 10);
            return sector.GetCell(playerCell);
            
        }
    }
}
