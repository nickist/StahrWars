﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmMain : Form
    {
        int sector = 0, col = 3, row = 7, shipAngle = 0, boxCount = 10, shields = 30, torpedos = 10, phasors = 50, fuelPods = 50, health = 50,
            pFull = 100, tFull = 100, fFull = 100, hFull = 100;
        bool gameOn = false, shieldOn = false, phasorsEquiped = true, sectorView = true;
        string sectorStars = "", sectorPlanets="", sectorBlackholes="";
        string sectorStr = "";
        string tmp = "";
        UdpUser client = null;
        Pen gridPen = new Pen(System.Drawing.Color.White, 1);
        int gridSize = 0;
        Image planet = Image.FromFile("jupiter.png");
        Image star = Image.FromFile("star.png");
        Image background = Image.FromFile("background.jpg");
        Image blackhole = Image.FromFile("blackhole.jpg");
        Image shipNorth = Image.FromFile("ShipNorth.png");
        Image shipSouth = Image.FromFile("ShipSouth.png");
        Image shipEast = Image.FromFile("ShipEast.png");
        Image shipWest = Image.FromFile("ShipWest.png");
        Image torpedo = Image.FromFile("torpedo.png");
        Image universe = Image.FromFile("universe.jpg");
        //Image phasor = Image.FromFile("laser.png");

        //List<Point> points = new List<Point>();
        //List<Point> myShipPts = new List<Point>();

        #region ====================================================================================== UDP Class Setup

        // Structure
        public struct Received
        {
            public IPEndPoint Sender;
            public string Message;
        }

        // UDP Abstract class
        abstract class UdpBase
        {
            protected UdpClient Client;

            protected UdpBase()
            {
                Client = new UdpClient();
            }

            public async Task<Received> Receive()
            {
                var result = await Client.ReceiveAsync();
                return new Received()
                {
                    Message = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length),
                    Sender = result.RemoteEndPoint
                };
            }
        }

        // Client
        class UdpUser : UdpBase
        {
            private UdpUser() { }

            public static UdpUser ConnectTo(string hostname, int port)
            {
                var connection = new UdpUser();
                connection.Client.Connect(hostname, port);
                return connection;
            }

            public void Send(string message)
            {
                var datagram = Encoding.ASCII.GetBytes(message);
                Client.Send(datagram, datagram.Length);
            }
        }

        #endregion

        public frmMain()
        {
            InitializeComponent();

            // set up the grid look - 1 dot, 4 spaces, 1 dot, 4 spaces
            gridPen.DashPattern = new float[] { 1, 4 };
            gridSize = panCanvas.Width / boxCount;

            // Design out my ship... We could just use an image here
            //myShipPts.Add(new Point(0, 0));
            //myShipPts.Add(new Point(10, 5));
            //myShipPts.Add(new Point(0, -20));
            //myShipPts.Add(new Point(-10, 5));

        }

        private void beginMessages()
        {
            if (txtIP.Text.Trim().Length == 0)
            {
                MessageBox.Show("You must enter the IP address of the server",
                                    "Connection Aborted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            client = UdpUser.ConnectTo(txtIP.Text.Trim(), 32123);
            client.Send("Connected: " + Environment.UserName);

            // 10.33.7.192

            string msg;
            string[] parts;
            Task.Factory.StartNew(async () => {
                while (true)
                {
                    try {
                        msg = (await client.Receive()).Message.ToString();

                        parts = msg.Split(':');
                        fixParts(parts);
                        if (parts[0].Equals("quit")) {
                            break;
                        } else if (parts[0].Equals("connected")) {
                            gameOn = parts[1].Equals("true");

                            if (gameOn) {
                                txtIP.Invoke(new Action(() => txtIP.BackColor = Color.Green));
                                btnConnect.Invoke(new Action(() => btnConnect.BackColor = Color.Green));
                                txtIP.Invoke(new Action(() => txtIP.ReadOnly = true));
                                txtIP.Invoke(new Action(() => txtIP.Enabled = false));
                                btnConnect.Invoke(new Action(() => btnConnect.Enabled = false));

                                addText("You have joined the game...\n");
                                sectorStr = parts[2];
                                col = Convert.ToInt32(parts[3]);
                                row = Convert.ToInt32(parts[4]);
                                lblSector.Invoke(new Action(() => lblSector.Text = sectorStr));
                                prbHealth.Invoke(new Action(() => prbHealth.Value = hFull));
                                prbFuel.Invoke(new Action(() => prbFuel.Value = fFull)); //fuel pod
                                prbTorpedo.Invoke(new Action(() => prbTorpedo.Value = tFull)); //torpedo
                                prbPhasor.Invoke(new Action(() => prbPhasor.Value = pFull)); //phasor

                            } else {
                                addText("Connection not established\n");
                                sector = row = col = -1;
                            }

                            panCanvas.Invoke(new Action(() => panCanvas.Refresh()));

                        } else if (parts[0].Equals("unv")) {
                            for (int i = 1; i < parts.Length; i++) {
                                tmp += parts[i].PadRight(7);
                                if (i % 2 != 0) { tmp += " "; }
                                if (i % 16 == 0) { tmp += "\n\n"; }
                            }
                        } else if (parts[0].Equals("loc")) {
                            if (parts[1].Equals("star")) {
                                hitStar();
                            } else if (parts[1].Equals("planet")) {
                                hitPlanet();
                                client.Send("yuh");
                            } else {
                                sectorStr = parts[1];
                                lblSector.Invoke(new Action(() => lblSector.Text = sectorStr));
                                col = Convert.ToInt32(parts[2]);
                                row = Convert.ToInt32(parts[3]);
                                if (parts[4].Equals("n")) {
                                    shipAngle = 0;
                                } else if (parts[4].Equals("s")) {
                                    shipAngle = 180;
                                } else if (parts[4].Equals("e")) {
                                    shipAngle = 90;
                                } else if (parts[4].Equals("w")) {
                                    shipAngle = 270;
                                }
                            }
                            panCanvas.Invoke(new Action(() => panCanvas.Refresh()));
                        } else if (parts[0].Equals("or")) {
                            if (parts[1].Equals("n")) {
                                shipAngle = 0;
                            } else if (parts[1].Equals("s")) {
                                shipAngle = 180;
                            } else if (parts[1].Equals("e")) {
                                shipAngle = 90;
                            } else if (parts[1].Equals("w")) {
                                shipAngle = 270;
                            }
                            panCanvas.Invoke(new Action(() => panCanvas.Refresh()));
                        } else if (parts[0].Equals("sh")) {
                            if (parts[1].Equals("0")) {
                                shieldOn = false;
                            } else if (parts[1].Equals("1")) {
                                shieldOn = true;
                            } else if (parts[1].Equals("2")) {
                                shieldOn = false;
                            }
                        } else if (parts[0].Equals("si")) {
                            sectorStars = parts[1];
                            sectorPlanets = parts[2];
                            sectorBlackholes = parts[3];
                            panCanvas.Invoke(new Action(() => panCanvas.Refresh()));
                        } else if (parts[0].Equals("me")) {
                            //Handle Changes to health/shields/ammo/fuel 
                            //Health = parts[1];
                            //Shields = parts[2]
                            //Phasors = parts[3];
                            //Torpeados = parts[4]
                            //fuel = parts[5];
                        } else if (parts[0].Equals("star")) {
                            health = 0;
                            hFull = health / 2;
                            panCanvas.Invoke(new Action(() => panCanvas.Refresh()));

                        } else if (parts[0].Equals("planet")) {
                            fFull = 100; fuelPods = 50;
                            hFull = 100; health = 50;
                            tFull = 100; torpedos = 50;
                            pFull = 100; phasors = 50;
                            panCanvas.Invoke(new Action(() => panCanvas.Refresh()));

                        } else {
                            addText(msg);
                        }
                    } catch (Exception e) {
                        MessageBox.Show(e.Message);
                    }
                }

                Environment.Exit(0);
            });
        }

        private void drawUniverse() {
            frmUniverse frm = new frmUniverse();
            frm.setText(tmp);
            frm.Show();
        }

        private void gridDraw(PaintEventArgs e) {
            for (int i = gridSize; i < panCanvas.Height; i += gridSize) {
                e.Graphics.DrawLine(gridPen, 0, i, panCanvas.Width, i);
                e.Graphics.DrawLine(gridPen, i, 0, i, panCanvas.Height);
            }
        }

        private void backgroundDraw(Image imageName, PaintEventArgs e) {
            e.Graphics.DrawImage(imageName, 0, 0);
        }



        private void panCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (gameOn)
            {
                if (chkShowBackground.Checked)
                    backgroundDraw(background, e);
                if (chkShowGrid.Checked)
                {
                    // Draw the grid
                    gridDraw(e);
                    
                }


                // Place Planets 
                
                for (int i = 0; i < sectorPlanets.Length; i++)
                {

                    String temp = sectorPlanets[i].ToString();
                    if (i + 1 != sectorPlanets.Length && sectorPlanets[i + 1] != ',')
                    {
                        temp = temp + sectorPlanets[i + 1].ToString();
                    }
                    int cellNum;
                    Int32.TryParse(temp, out cellNum);
                    e.Graphics.DrawImage(planet, loc(cellNum % 10, cellNum / 10, gridSize / 1.5));
                    i++;
                }
                
                // Place Stars
                for (int i = 0; i < sectorStars.Length; i++)
                {

                    String temp = sectorStars[i].ToString();
                    if (i + 1 != sectorStars.Length && sectorStars[i + 1] != ',')
                    {
                        temp = temp + sectorStars[i + 1].ToString();
                    }
                    int cellNum;
                    Int32.TryParse(temp, out cellNum);
                    e.Graphics.DrawImage(star, loc(cellNum % 10, cellNum / 10, star.Width / 4));
                    i++;
                }
                /*
			     * Draw the ship
			     */
                if (shipAngle == 0) e.Graphics.DrawImage(shipNorth, loc(col, row, shipNorth.Width / 2));
                else if (shipAngle == 90) e.Graphics.DrawImage(shipEast, loc(col, row, shipEast.Width / 2));
                else if (shipAngle == 180) e.Graphics.DrawImage(shipSouth, loc(col, row, shipSouth.Width / 2));
                else e.Graphics.DrawImage(shipWest, loc(col, row, shipWest.Width / 2));

                if (shieldOn)
                    e.Graphics.DrawEllipse(new Pen(Brushes.Gold, 2), loc(col, row, gridSize / 1.5));
              
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!gameOn) return false;

            try
            {

                switch (keyData)
                {
                    case Keys.V: 
                        client.Send("v");
                        drawUniverse();
                        break;

					case Keys.Up:
						if (shipAngle != 0) {
							shipAngle = 0;
							panCanvas.Refresh();
							client.Send("rn");
						} else {
                            move("n");
                            fuelLoss();
                        }
                        break;
                    case Keys.Right:
                        if (shipAngle != 90)
                        {
                            shipAngle = 90;
                            panCanvas.Refresh();
                            client.Send("re");
                        }
                        else
                        {
                            move("e");
                            fuelLoss();
                        }
                        break;
                    case Keys.Down:
                        if (shipAngle != 180)
                        {
                            shipAngle = 180;
                            panCanvas.Refresh();
                            client.Send("rs");
                        }
                        else
                        {
                            move("s");
                            fuelLoss();

                        }
                        break;
                    case Keys.Left:
                        if (shipAngle != 270)
                        {
                            shipAngle = 270;
                            panCanvas.Refresh();
                            client.Send("rw");
                        }
                        else
                        {
                            move("w");
                            fuelLoss();

                        }
                        break;
                    case Keys.S:
                        switchShields();
                        break;
                    case Keys.H:
                        client.Send("h");
                        hFuelLoss();
                        break;
                    case Keys.Q:
                        phasorsEquiped = !phasorsEquiped;
                        client.Send((phasorsEquiped ? "PHASOR" : "TORPEDO") + " equipped");
                        break;
                    case Keys.Space:
                        fireWeapon();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.Text = ex.Message;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void move(string dir)
        {
            if (!shieldOn) client.Send("mov:" + dir);
        }



        #region ====================================================================================== <Shields>

        private void switchShields()
        {
            if (!gameOn) return;
            if (shields == 0)
            {
                client.Send("Out of Sheilds!");
            }
            else
            {
                shieldOn = !shieldOn;
                client.Send("s" + (shieldOn ? "1" : "0"));
                lblShielsUp.ForeColor = (shieldOn ? Color.Green : Color.Red);
                lblShielsUp.Text = (shieldOn ? "ON" : "OFF");
                panCanvas.Refresh();
                picShields.Refresh();
                shields--;
            }

        }
        private void panCanvas_Click(object sender, EventArgs e) {

        }
        private void picShields_Click(object sender, EventArgs e)
        {
            switchShields();
        }

        private void lblShielsUp_Click(object sender, EventArgs e)
        {
            switchShields();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e) {

        }

        private void picShields_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(shieldOn ? Brushes.Green : Brushes.Red, 2, 2, picShields.Width - 4, picShields.Height - 4);
            e.Graphics.DrawEllipse(Pens.Gray, 2, 2, picShields.Width - 4, picShields.Height - 4);
        }



        #endregion

        #region ================================================================================= <Fire Weapons>
        private void fireWeapon()
        {
            if (!gameOn) return;
            if (phasorsEquiped == true)
            {
                if (phasors != 0 && pFull >= 2)
                {
                    client.Send("fp");
                    pFull -= 2;
                    phasors--;
                    label10.Text = "" + phasors;
                    prbPhasor.Invoke(new Action(() => prbPhasor.Value = pFull)); //phasor
                }
                else
                {
                    client.Send("Out of PHASORS!");
                }
            }
            else
            {
                if (phasors != 0 && tFull >= 10)
                {
                    client.Send("ft");
                    tFull -= 10;
                    torpedos--;
                    prbTorpedo.Invoke(new Action(() => prbTorpedo.Value = tFull)); //torpedo
                    label9.Text = "" + torpedos;

                }
                else
                {
                    client.Send("Out of TORPEDOS!");
                }
            }
              }
       

        #endregion

        #region ================================================================================== <fuel>
        private void fuelLoss()
        {
            if (!gameOn) return;
            if (!shieldOn)
            {
                if (fuelPods != 0 && fFull > 0)
                {
                    if (fFull >= 2)
                        fFull -= 2;

                    else if (fFull == 1)
                        fFull -= 1;

                    fuelPods--;
                    label11.Text = "" + fuelPods;
                    prbFuel.Invoke(new Action(() => prbFuel.Value = fFull)); //fuel pod
                }
                else
                {
                    client.Send("Out of Fuel!");
                }

                label11.Text = "" + fuelPods;
                prbFuel.Invoke(new Action(() => prbFuel.Value = fFull)); //fuel pod
            }
        }

        private void hFuelLoss()
        {
            if (!gameOn) return;
            if (!shieldOn)
            {
                if (fuelPods >= 5 && fFull >= 10)
                {
                    fFull -= 10;
                    fuelPods -= 5;
                    label11.Text = "" + fuelPods;
                    prbFuel.Invoke(new Action(() => prbFuel.Value = fFull)); //fuel pod
                }
                else if (fuelPods > 0 && fuelPods < 5)
                {
                    client.Send("Not enough fuel!");

                }
                else if (fuelPods == 0)
                {
                    client.Send("Out of fuel!");

                }

                label11.Text = "" + fuelPods;
                prbFuel.Invoke(new Action(() => prbFuel.Value = fFull)); //fuel pod
            }
        }
        #endregion

        #region ================================================================================== <health>

        private void hitByPhasor()
        {
            if (health != 0 && hFull >= 10)
            {
                hFull -= 10;
                health -= 5;
                label12.Text = "" + health;
                prbHealth.Invoke(new Action(() => prbHealth.Value = hFull));
            }

            else
            {
                client.Send("YOU LOSE!");
            }
        }

        private void hitByTorpedo()
        {
            if (health != 0 && hFull >= 30)
            {
                hFull -= 30;
                health -= 15;
                prbHealth.Invoke(new Action(() => prbHealth.Value = hFull));
            }

            else
            {
                client.Send("YOU LOSE!");
            }
        }

       

        #endregion

        #region ============================================================================== <star>

        private void hitStar()
        {
            hFull = 0; health = 0;
            prbHealth.Invoke(new Action(() => prbHealth.Value = hFull));
            //label12.Text = "" + health;
            label12.BeginInvoke(new Action(() => label12.Text = "" + health));

            client.Send("You Lose!");
        }

        #endregion


        #region ============================================================================== <planet>

        private void hitPlanet()
        {
            fFull = 100; fuelPods = 50;
            hFull = 100; health = 50;
            tFull = 100; torpedos = 10;
            pFull = 100; phasors = 50;

            prbHealth.Invoke(new Action(() => prbHealth.Value = hFull));
            //label12.Text = "" + health;
            label12.BeginInvoke(new Action(() => label12.Text = "" + health));

            prbTorpedo.Invoke(new Action(() => prbTorpedo.Value = tFull)); //torpedo
            //label9.Text = "" + torpedos;
            label9.BeginInvoke(new Action(() => label9.Text = "" + torpedos));

            prbPhasor.Invoke(new Action(() => prbPhasor.Value = pFull)); //phasor                           
            //label10.Text = "" + phasors;
            label10.BeginInvoke(new Action(() => label10.Text = "" + phasors));

            prbFuel.Invoke(new Action(() => prbFuel.Value = fFull)); //fuel pod
            //label11.Text = "" + fuelPods;
            label11.BeginInvoke(new Action(() => label11.Text = "" + fuelPods));


            client.Send("Resources replinished!");
        }

        #endregion

        #region ================================================== <GUI>
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void prbHealth_Click(object sender, EventArgs e)
        {

        }



        private void progressBar3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region ====================================================================================== <Local Settings>

        private void chkShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            panCanvas.Refresh();
        }

        private void chkShowBackground_CheckedChanged(object sender, EventArgs e)
        {
            panCanvas.Refresh();
        }

        #endregion


        #region ====================================================================================== <Local Methods>

        private void addText(string msg)
        {
            txtServerMessages.Invoke(new Action(() => txtServerMessages.ReadOnly = false));
            txtServerMessages.Invoke(new Action(() => txtServerMessages.AppendText(" > " + msg + "\n")));
            txtServerMessages.Invoke(new Action(() => txtServerMessages.ReadOnly = true));
            this.Invoke(new Action(() => this.Focus()));
        }


        private Rectangle loc(double col, double row, double size)
        {
            double offset = (gridSize - size) / 2;
            return new Rectangle((int)(col * gridSize + offset),
                                    (int)(row * gridSize + offset),
                                    (int)size,
                                    (int)size);
        }

        private static void fixParts(string[] parts)
        {
            for (int i = 0; i < parts.Length; i++)
                parts[i] = parts[i].Trim().ToLower();
        }

        #endregion


        #region ====================================================================================== Local Button Clicks

        private void btnConnect_Click(object sender, EventArgs e)
        {
            beginMessages();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


        #region ====================================================================================== Closing Application

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (client != null) client.Send("quit");
        }

        #endregion
    }
}

