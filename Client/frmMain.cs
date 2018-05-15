using System;
using System.Collections.Generic;
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
        int sector = 0, col = 3, row = 7, shipAngle = 0, weaponAngle = 0, boxCount = 10, shields, torpedos, phasors, fuelPods, health;
        bool gameOn = false, shieldOn = false, phasorsEquiped = true, isAlive = true;
        string sectorStars = "", sectorPlanets="", sectorBlackholes="", playerLocations="";
        string sectorStr = "", bulletLocations = "";
        UdpUser client = null;
        Pen gridPen = new Pen(System.Drawing.Color.White, 1);
        int gridSize = 0;
        String unvString = "";
        Image planet = Image.FromFile("jupiter.png");
        Image star = Image.FromFile("star.png");
        Image deadShip = Image.FromFile("deadship.png");
        Image background = Image.FromFile("background.jpg");
        Image blackhole = Image.FromFile("blackhole.jpg");
        Image shipNorth = Image.FromFile("ShipNorth.png");
        Image shipSouth = Image.FromFile("ShipSouth.png");
        Image shipEast = Image.FromFile("ShipEast.png");
        Image shipWest = Image.FromFile("ShipWest.png");
        Image torpedoNorth = Image.FromFile("torpedoNorth.png");
        Image torpedoSouth = Image.FromFile("torpedoSouth.png");
        Image torpedoEast = Image.FromFile("torpedoEast.png");
        Image torpedoWest = Image.FromFile("torpedoWest.png");
        Image phasorNorth = Image.FromFile("phasorNorth.jpg");
        Image phasorSouth = Image.FromFile("phasorSouth.jpg");
        Image phasorEast = Image.FromFile("phasorEast.jpg");
        Image phasorWest = Image.FromFile("phasorWest.jpg");

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
                    try
                    {
                        msg = (await client.Receive()).Message.ToString();

                        parts = msg.Split(':');
                        fixParts(parts);
                        if (parts[0].Equals("quit"))
                        {
                            break;
                        }
                        else if (parts[0].Equals("connected"))
                        {
                            gameOn = parts[1].Equals("true");

                            if (gameOn)
                            {
                                txtIP.Invoke(new Action(() => txtIP.BackColor = Color.Green));
                                btnConnect.Invoke(new Action(() => btnConnect.BackColor = Color.Green));
                                txtIP.Invoke(new Action(() => txtIP.ReadOnly = true));
                                txtIP.Invoke(new Action(() => txtIP.Enabled = false));
                                btnConnect.Invoke(new Action(() => btnConnect.Enabled = false));

                                addText("You have joined the game...\n");
                                addText("Phasors equipped!\n");
                                sectorStr = parts[2];
                                col = Convert.ToInt32(parts[3]);
                                row = Convert.ToInt32(parts[4]);
                                lblSector.Invoke(new Action(() => lblSector.Text = sectorStr));
                                prbHealth.Invoke(new Action(() => prbHealth.Value = 100));
                                progressBar1.Invoke(new Action(() => progressBar1.Value = 100)); //fuel pod
                                progressBar2.Invoke(new Action(() => progressBar2.Value = 100)); //torpedo
                                progressBar3.Invoke(new Action(() => progressBar3.Value = 100)); //phasor

                            }
                            else
                            {
                                addText("Connection not established\n");
                                sector = row = col = -1;
                            }

                            panCanvas.Invoke(new Action(() => panCanvas.Refresh()));

                        }
                        else if (parts[0].Equals("setup"))
                        {
                            Int32.TryParse(parts[1], out health);
                            Int32.TryParse(parts[2], out fuelPods);
                            Int32.TryParse(parts[3], out phasors);
                            Int32.TryParse(parts[4], out torpedos);
                            Int32.TryParse(parts[5], out shields);
                        }
                        else if (parts[0].Equals("dead"))
                        {
                            isAlive = false;
                            panCanvas.Invoke(new Action(() => panCanvas.Refresh()));


                        }
                        else if (parts[0].Equals("unv"))
                        {
                            for (int i = 1; i < parts.Length; i++)
                            {
                                unvString += parts[i].PadRight(7);
                                if (i % 2 != 0) { unvString += " "; }
                                if (i % 16 == 0) { unvString += "\n\n"; }
                            }
                        }
                        else if (parts[0].Equals("update"))
                        { // all update commands should be update:variable:{newamount}
                            if (parts[1].Equals("shields"))
                            {
                                Int32.TryParse(parts[2], out shields);
                            }
                            else if ((parts[1].Equals("fuelpods")))
                            {
                                Int32.TryParse(parts[2], out fuelPods);
                                label11.Invoke(new Action(() => label11.Text = "" + fuelPods));
                                progressBar1.Invoke(new Action(() => progressBar1.Value = fuelPods * 2)); //fuel pod

                            }
                            else if ((parts[1].Equals("health")))
                            {
                                Int32.TryParse(parts[2], out health);
                                label12.Invoke(new Action(() => label12.Text = "" + health));
                                prbHealth.Invoke(new Action(() => prbHealth.Value = health));


                            }
                            else if ((parts[1].Equals("phasors")))
                            {
                                Int32.TryParse(parts[2], out phasors);
                                label10.Invoke(new Action(() => label10.Text = "" + phasors));
                                progressBar3.Invoke(new Action(() => progressBar3.Value = phasors * 2)); //phasor

                            }
                            else if ((parts[1].Equals("torpedos")))
                            {
                                Int32.TryParse(parts[2], out torpedos);
                                progressBar2.Invoke(new Action(() => progressBar2.Value = torpedos * 10)); //torpedo
                                label9.Invoke(new Action(() => label9.Text = "" + torpedos));
                            }

                        }
                        else if (parts[0].Equals("loc"))
                        {
                            sectorStr = parts[1];
                            lblSector.Invoke(new Action(() => lblSector.Text = sectorStr));
                            col = Convert.ToInt32(parts[2]);
                            row = Convert.ToInt32(parts[3]);
                            if (parts[4].Equals("n"))
                            {
                                shipAngle = 0;
                            }
                            else if (parts[4].Equals("s"))
                            {
                                shipAngle = 180;
                            }
                            else if (parts[4].Equals("e"))
                            {
                                shipAngle = 90;
                            }
                            else if (parts[4].Equals("w"))
                            {
                                shipAngle = 270;
                            }
                            panCanvas.Invoke(new Action(() => panCanvas.Refresh()));
                        }
                        else if (parts[0].Equals("or"))
                        {
                            if (parts[1].Equals("n"))
                            {
                                shipAngle = 0;
                                weaponAngle = 0;
                                client.Send("");
                            }
                            else if (parts[1].Equals("s"))
                            {
                                shipAngle = 180;
                                weaponAngle = 180;

                            }
                            else if (parts[1].Equals("e"))
                            {
                                shipAngle = 90;
                                weaponAngle = 90;

                            }
                            else if (parts[1].Equals("w"))
                            {
                                shipAngle = 270;
                                weaponAngle = 270;

                            }
                            panCanvas.Invoke(new Action(() => panCanvas.Refresh()));
                        }
                        else if (parts[0].Equals("sh"))
                        {
                            if (parts[1].Equals("0"))
                            {
                                shieldOn = false;
                            }
                            else if (parts[1].Equals("1"))
                            {
                                shieldOn = true;
                            }
                            else if (parts[1].Equals("2"))
                            {
                                shieldOn = false;
                            }
                        }
                        else if (parts[0].Equals("si"))
                        {
                            sectorStars = parts[1];
                            sectorPlanets = parts[2];
                            sectorBlackholes = parts[3];
                            panCanvas.Invoke(new Action(() => panCanvas.Refresh()));
                        }
                        else if (parts[0].Equals("ni")) //Update planet and players in sector
                        {
                            sectorPlanets = parts[1];
                            playerLocations = parts[2];
                            bulletLocations = parts[3];
                            panCanvas.Invoke(new Action(() => panCanvas.Refresh()));
                        }
                        else
                        {
                            addText(msg);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }

                Environment.Exit(0);
            });
        }


        private void drawUniverse()
        {
            frmUniverse frm = new frmUniverse();
            frm.setText(unvString);
            frm.Show();
        }

        private void drawGrid(PaintEventArgs e)
        {
            for (int i = gridSize; i < panCanvas.Height; i += gridSize)
            {
                e.Graphics.DrawLine(gridPen, 0, i, panCanvas.Width, i);
                e.Graphics.DrawLine(gridPen, i, 0, i, panCanvas.Height);
            }
        }


        private void panCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (gameOn)
            {
                if (chkShowBackground.Checked)
                    e.Graphics.DrawImage(background, 0, 0);
                if (chkShowGrid.Checked)
                {
                    // Draw the grid
                    drawGrid(e);
                }


                // Place Planets 
                if (sectorPlanets.Length != 0)
                {
                    String[] cellsP = sectorPlanets.Split(',');
                    for (int i = 0; i < cellsP.Length; i++)
                    {
                        int cellNum;
                        Int32.TryParse(cellsP[i], out cellNum);
                        e.Graphics.DrawImage(planet, loc(cellNum % 10, cellNum / 10, gridSize / 1.5));
                        
                    }
                }

                // Place Stars
                if (sectorStars.Length != 0)
                {
                    String[] cellsS = sectorStars.Split(',');
                    for (int i = 0; i < cellsS.Length; i++)
                    {
                        int cellNum;
                        Int32.TryParse(cellsS[i], out cellNum);
                        e.Graphics.DrawImage(star, loc(cellNum % 10, cellNum / 10, star.Width / 4));
                    }
                }

                
                //Place Blackholes
                if (sectorBlackholes.Length != 0)
                {
                    String[] cellsB = sectorBlackholes.Split(',');
                    for (int i = 0; i < cellsB.Length; i++)
                    {
                        int cellNum;
                        Int32.TryParse(cellsB[i], out cellNum);
                        e.Graphics.DrawImage(blackhole, loc(cellNum % 10, cellNum / 10, gridSize / 1.25));
                    }
                }
                
                //Place Phasors/Torps
                if (bulletLocations.Length != 0)
                {
                    String[] cellsW = bulletLocations.Split(',');
                    for (int i = 0; i < cellsW.Length; i++)
                    {
                        int cellNum;
                        Int32.TryParse(cellsW[i].Substring(0, cellsW[i].Length - 2), out cellNum);
                        Char wepType = cellsW[i][cellsW[i].Length - 2];
                        Char wepAngle = cellsW[i][cellsW[i].Length - 1];
                        if (wepType == 't')
                        {
                            if (wepAngle == 'n')
                            {
                                e.Graphics.DrawImage(torpedoNorth, loc(cellNum % 10, cellNum / 10, gridSize / 3));
                            }
                            else if (wepAngle == 's')
                            {
                                e.Graphics.DrawImage(torpedoSouth, loc(cellNum % 10, cellNum / 10, gridSize / 3));
                            }
                            else if (wepAngle == 'e')
                            {
                                e.Graphics.DrawImage(torpedoEast, loc(cellNum % 10, cellNum / 10, gridSize / 3));
                            }
                            else
                            {
                                e.Graphics.DrawImage(torpedoWest, loc(cellNum % 10, cellNum / 10, gridSize / 3));
                            }
                        }
                        else
                        {
                            if (wepAngle == 'n')
                            {
                                e.Graphics.DrawImage(phasorNorth, loc(cellNum % 10, cellNum / 10, gridSize / 3));
                            }
                            else if (wepAngle == 's')
                            {
                                e.Graphics.DrawImage(phasorSouth, loc(cellNum % 10, cellNum / 10, gridSize / 3));
                            }
                            else if (wepAngle == 'e')
                            {
                                e.Graphics.DrawImage(phasorEast, loc(cellNum % 10, cellNum / 10, gridSize / 3));
                            }
                            else
                            {
                                e.Graphics.DrawImage(phasorWest, loc(cellNum % 10, cellNum / 10, gridSize / 2));
                            }
                        }

                    }
                }
                /*
                 *
			     * Draw the ship
			     */
                if (isAlive == false) e.Graphics.DrawImage(deadShip, loc(col, row, shipNorth.Width / 2)); 
            else if (shipAngle == 0) e.Graphics.DrawImage(shipNorth, loc(col, row, shipNorth.Width / 2));
            else if (shipAngle == 90) e.Graphics.DrawImage(shipEast, loc(col, row, shipEast.Width / 2));
            else if (shipAngle == 180) e.Graphics.DrawImage(shipSouth, loc(col, row, shipSouth.Width / 2));
            else e.Graphics.DrawImage(shipWest, loc(col, row, shipWest.Width / 2));
               

                if (shieldOn)
                    e.Graphics.DrawEllipse(new Pen(Brushes.Gold, 2), loc(col, row, gridSize / 1.5));
            }
            //Draw Enemy Ships
            /**
            if (playerLocations.Length != 0)
            {
                String[] playerCells = playerLocations.Split(',');
                for (int i = 0; i < playerCells.Length; i++)
                {
                    int cellNum;
                    Int32.TryParse(playerCells[i].Substring(0,playerCells[i].Length - 1), out cellNum);
                    Char playerAngle = playerCells[i][playerCells[i].Length - 1];
                    if (cellNum != (col % 10)+(row * 10))
                    {
                        if (playerAngle == 'n')
                        {
                            e.Graphics.DrawImage(shipNorthEnemy, loc(cellNum % 10, cellNum / 10, shipNorthEnemy.Width / 2));
                        } else if (playerAngle == 's')
                        {
                            e.Graphics.DrawImage(shipSouthEnemy, loc(cellNum % 10, cellNum / 10, shipSouthEnemy.Width / 2));
                        }
                        else if (playerAngle == 'e')
                        {
                            e.Graphics.DrawImage(shipEastEnemy, loc(cellNum % 10, cellNum / 10, shipEastEnemy.Width / 2));
                        }
                        else
                        {
                            e.Graphics.DrawImage(shipWestEnemy, loc(cellNum % 10, cellNum / 10, shipWestEnemy.Width / 2));
                        }
                    }
                }
            } **/
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
                            weaponAngle = 0;
							panCanvas.Refresh();
							client.Send("r:n");
						} else {
                            move("n");
                            fuelLoss();
                        }
                        break;
                    case Keys.Right:
                        if (shipAngle != 90)
                        {
                            shipAngle = 90;
                            weaponAngle = 90;
                            panCanvas.Refresh();
                            client.Send("r:e");
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
                            weaponAngle = 180;
                            panCanvas.Refresh();
                            client.Send("r:s");
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
                            weaponAngle = 270;
                            panCanvas.Refresh();
                            client.Send("r:w");
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
                        client.Send("h:");
                        fuelLoss();
                        break;
                    case Keys.Q:
                        phasorsEquiped = !phasorsEquiped;
                        client.Send("q:" + (phasorsEquiped ? "p" : "t"));
                        break;
                    case Keys.F:
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
            if (shields > 0)
            {
                shieldOn = !shieldOn;
                client.Send("s:" + (shieldOn ? "1" : "0"));
                lblShielsUp.ForeColor = (shieldOn ? Color.Green : Color.Red);
                lblShielsUp.Text = (shieldOn ? "ON" : "OFF");
                panCanvas.Refresh();
                picShields.Refresh();
            } else
            {
                label13.Invoke(new Action(() => label13.Text = "" + shields));
                shieldOn = false;
                client.Send("s:2");
                lblShielsUp.ForeColor = Color.Red;
                lblShielsUp.Text = "OFF";
                panCanvas.Refresh();
                picShields.Refresh();

            }
            label13.Invoke(new Action(() => label13.Text = "" + shields));

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

        private void drawTorpedo(PaintEventArgs e)
        {

        }

        private void picShields_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(shieldOn ? Brushes.Green : Brushes.Red, 2, 2, picShields.Width - 4, picShields.Height - 4);
            e.Graphics.DrawEllipse(Pens.Gray, 2, 2, picShields.Width - 4, picShields.Height - 4);
        }

        #endregion

                
        #region ====================================================== <Fire Weapons>
        private void fireWeapon()
        {
            if (phasorsEquiped == true)
            {

                if (phasors >= 0)
                {
                    client.Send("f:p");
                    label10.Invoke(new Action(() => label10.Text = "" + phasors));
                    progressBar3.Invoke(new Action(() => progressBar3.Value = phasors*2)); //phasor
                }

            }
            else
            {
                if (torpedos >= 0)
                {
                    client.Send("f:t");
                    progressBar2.Invoke(new Action(() => progressBar2.Value = torpedos*10)); //torpedo
                    label9.Invoke(new Action(() => label9.Text = "" + torpedos ));
                }
            }
        }


        #endregion

        #region =============================================== <fuel>
        private void fuelLoss()
        {
            if (fuelPods > 0)
            {
                label11.Invoke(new Action(() => label11.Text = "" + fuelPods));
                progressBar1.Invoke(new Action(() => progressBar1.Value = fuelPods*2)); //fuel pod
            }
 

        }

        #endregion

        #region ==================================================================================================== <Local click>

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

