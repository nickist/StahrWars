using System;
using System.Net;

namespace UPDServer {
    class Player
    {

        private string name;
        private int sector;
        private string sectorStr;
        private IPEndPoint connection;
        private int column; 
        private int row;
        private int health;
        private int sheilds;
        private int torpedoes;
        private int phasors;
        private int fuelPods;
        private string oriantation;
        private bool shieldOn;

        public void setLocation() {

        }

        public Player(string name) {
            Name = name;
            Health = 100;
            Sheilds = 15;
            FuelPods = 500;
            Torpedoes = 10;
            Phasors = 50;
        }

        public string Name {
            get {
                return name;
            }

            set {
                name = value;
            }
        }

        public int Sector {
            get {
                return sector;
            }

            set {
                sector = value;
            }
        }

        public IPEndPoint Connection {
            get {
                return connection;
            }

            set {
                connection = value;
            }
        }

        public int Column {
            get {
                return column;
            }

            set {
                column = value;
            }
        }

        public int Row {
            get {
                return row;
            }

            set {
                row = value;
            }
        }

        public int Health {
            get {
                return health;
            }

            set {
                health = value;
            }
        }

        public int Sheilds {
            get {
                return sheilds;
            }

            set {
                sheilds = value;
            }
        }

        public int Torpedoes {
            get {
                return torpedoes;
            }

            set {
                torpedoes = value;
            }
        }

        public int Phasors {
            get {
                return phasors;
            }

            set {
                phasors = value;
            }
        }

        public int FuelPods {
            get {
                return fuelPods;
            }

            set {
                fuelPods = value;
            }
        }
        public string Oriantation {
            get {
                return oriantation;
            }

            set {
                oriantation = value;
            }
        }
        public bool ShieldOn{
            get{
                return shieldOn;
            }

            set{
                shieldOn = value;
            }
        }
        public string SectorStr
        {
            get
            {
                return sectorStr;
            }

            set
            {
                sectorStr = value;
            }
        }
    }
}
