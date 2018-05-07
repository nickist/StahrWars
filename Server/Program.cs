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
                        p1.Sector = rnd.Next(0, 63);
                        p1.Column = rnd.Next(0, 9);
                        p1.Row = rnd.Next(0, 9);
                        players.Add(received.Sender.Address.MapToIPv4().ToString(), p1);
                        server.Reply(String.Format("connected:true:{0}:{1}:{2}", p1.Sector, p1.Column, p1.Row), received.Sender);
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

                            if (parts[1].Equals("n")) p.Row--;
                            else if (parts[1].Equals("s")) p.Row++;
                            else if (parts[1].Equals("e")) p.Column++;
                            else if (parts[1].Equals("w")) p.Column--;

                            server.Reply(String.Format("loc:{0}:{1}:{2}:{3}", p.Sector, p.Column, p.Row, parts[1]), received.Sender);
                        }
                        else if (parts[0].Equals("r"))
                        {
                            if (parts[1].Equals("n"))
                            {
                                //Change orientation variable for player
                                server.Reply(String.Format("or:{0}", parts[1]), received.Sender);
                            }
                            else if (parts[1].Equals("s"))
                            {
                                //Change orientation variable for player
                                server.Reply(String.Format("or:{0}", parts[1]), received.Sender);
                            }
                            else if (parts[1].Equals("e"))
                            {
                                //Change orientation variable for player
                                server.Reply(String.Format("or:{0}", parts[1]), received.Sender);
                            }
                            else if (parts[1].Equals("w"))
                            {
                                //Change orientation variable for player
                                server.Reply(String.Format("or:{0}", parts[1]), received.Sender);
                            }
                        }
                        else if (parts[0].Equals("s"))
                        {
                            p1.Sheilds--;
                            if (parts[1].Equals("1"))
                            {
                                //Set shieldOn for player to true
                                server.Reply(String.Format("sh:{0}", parts[1]), received.Sender);
                            }
                            else if (parts[1].Equals("0"))
                            {
                                //Set shieldOn for player to false
                                server.Reply(String.Format("sh:{0}", parts[1]), received.Sender);
                            }
                        }
                        else if (parts[0].Equals("v"))
                        {
                            
                            //Add if statements for each sector and one for whole universe 

                        }
                        else if (parts[0].Equals("f"))
                        {
                            if (parts[1].Equals("p"))
                            {
                                //Add code for handeling shooting phasor
                                p1.Phasors--;
                            }
                            else if (parts[1].Equals("t"))
                            {
                                //Add code for handeling shooting torpedo
                                p1.Torpedoes--;
                            }
                        }
                        else if (parts[0].Equals("h"))
                        {
                            p.setLocation();
                            server.Reply(String.Format("loc:{0}:{1}:{2}:{3}", p.Sector, p.Column, p.Row, parts[1]), received.Sender);
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
    }
}
