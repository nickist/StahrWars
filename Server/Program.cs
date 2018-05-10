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

        static void Main(string[] args)
        {
            //create a new server
            var server = new UdpListener();
            //create universe
            Universe universe = new Universe();
            Random rnd = new Random();
            Dictionary<string, IPEndPoint> connections = new Dictionary<string, IPEndPoint>();
            Dictionary<string, Player> players = new Dictionary<string, Player>();


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
                        p1.Column = rnd.Next(0, 9);
                        p1.Row = rnd.Next(0, 9);
                        players.Add(received.Sender.Address.MapToIPv4().ToString(), p1);
                        p1.SectorStr = numToSectorID(p1.Sector);
                        server.Reply(String.Format("connected:true:{0}:{1}:{2}", p1.SectorStr, p1.Column, p1.Row), received.Sender);
                        //Galaxy sector = universe.getGalaxy(p1.SectorStr);
                        //server.Reply(String.Format("si:{0}:{1}:{2}", sector.StarLocations, sector.PlanetLocations, sector.BlackholeLocations), received.Sender);
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
                        if (parts[0].Equals("mov"))
                        {
                            if (p.FuelPods != 0)
                            {
                                p.FuelPods--;
                                if (parts[1].Equals("n")) p.Row--;
                                else if (parts[1].Equals("s")) p.Row++;
                                else if (parts[1].Equals("e")) p.Column++;
                                else if (parts[1].Equals("w")) p.Column--;

                                server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.Sector, p.Column, p.Row, parts[1], p.FuelPods), received.Sender);
                            }
                            else
                            {
                                server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.Sector, p.Column, p.Row, parts[1], p.FuelPods), received.Sender);
                            }
                        }
                        else if (parts[0].Equals("r"))
                        {
                            if (parts[1].Equals("n"))
                            {
                                p.Oriantation = "n";
                                server.Reply(String.Format("or:{0}", parts[1]), received.Sender);
                            }
                            else if (parts[1].Equals("s"))
                            {
                                p.Oriantation = "s";
                                server.Reply(String.Format("or:{0}", parts[1]), received.Sender);
                            }
                            else if (parts[1].Equals("e"))
                            {
                                p.Oriantation = "e";
                                server.Reply(String.Format("or:{0}", parts[1]), received.Sender);
                            }
                            else if (parts[1].Equals("w"))
                            {
                                p.Oriantation = "w";
                                server.Reply(String.Format("or:{0}", parts[1]), received.Sender);
                            }
                        }
                        else if (parts[0].Equals("s"))
                        {
                            if (parts[1].Equals("1"))
                            {
                                if (p.Sheilds != 0)
                                {
                                    p.Sheilds--;
                                    p.ShieldOn = true;
                                    server.Reply(String.Format("sh:{0}", parts[1]), received.Sender);
                                }
                                else
                                {
                                    parts[1] = "2";
                                    server.Reply(String.Format("sh:{0}", parts[1]), received.Sender);
                                }
                            }
                            else if (parts[1].Equals("0"))
                            {
                                p.ShieldOn = false;
                                server.Reply(String.Format("sh:{0}", parts[1]), received.Sender);
                            }
                        }
                        else if (parts[0].Equals("v"))
                        {
                            if (parts[1].Equals("u"))
                            {
                                //Display universe info
                            }
                            else
                            {
                                Galaxy sector = universe.getGalaxy(parts[1]);
                                server.Reply(String.Format("si:{0}:{1}:{2}", sector.StarLocations, sector.PlanetLocations, sector.BlackholeLocations),received.Sender);
                            }
                            

                        }
                        else if (parts[0].Equals("f"))
                        {
                            if (parts[1].Equals("p"))
                            {
                                //Add code for handelig shooting phasors
                                if (p.Phasors != 0)
                                {
                                    p.Phasors--;
                                }
                                else
                                {

                                }
                                server.Reply(String.Format("sh:{0}", parts[1]), received.Sender);
                            }
                            else if (parts[1].Equals("t"))
                            {
                                //Add code for handeling shooting torpedo
                                if (p.Torpedoes != 0)
                                {
                                    p.Torpedoes--;
                                }
                                else
                                {

                                }
                                server.Reply(String.Format("sh:{0}", parts[1]), received.Sender);
                            }
                        }
                        else if (parts[0].Equals("h"))
                        {
                            if (p.FuelPods >= 5){
                                p.Sector = rnd.Next(0, 255);
                                p.Column = rnd.Next(0, 9);
                                p.Row = rnd.Next(0, 9);
                                p.FuelPods -= 5;
                                server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.Sector, p.Column, p.Row, p.Oriantation, p.FuelPods), received.Sender);
                            }
                            else{ //Change this to a differnt reply in the future so user can be promted that they dont have enough fuel to hyperspace
                                server.Reply(String.Format("loc:{0}:{1}:{2}:{3}:{4}", p.Sector, p.Column, p.Row, p.Oriantation, p.FuelPods), received.Sender);
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
    }
}
