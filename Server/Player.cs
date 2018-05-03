using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPDServer {
    class Player
    {

        private string name;
        private int sector;
        private int column; 
        private int row;
        private int health;
        private int sheilds;
        private int torpedoes;
        private int phasors;
        private int fuelPods;
        private string password;
        private string oriantation;

      

       public Player(string name, String password) {
            setName(name);
            setHealth(100);
            setSheilds(15);
            setFuelPods(50);
            setTorpedoes(10);
            setPhasors(50);
            setPassword(password);

        }

        public void setSector(int sector) {
            this.sector = sector;
        }

        public int getSector() {
            return sector;
        }

        public void setColumn(int column) {
            this.column = column;
        }

        public int getColumn() {
            return column;
        }

        public void setRow(int row) {
            this.row = row;
        }

        public int getRow() {
            return row;
        }

        public void setOriantation(string oriantation) {
            this.oriantation = oriantation;
        }

        public string getOriantation() {
            return oriantation;
        }

        public void setName(string name) {
            this.name = name;
        }

        public string getName() {
            return name;
        }

        public void setPassword(String password) {
            this.password = password;
        }

        public string getPassword() {
            return password;
        }

            // return sector:column:row
        public string getLocation() {
            return sector +":"+ column +":"+ row + ""; 
        }

        public void updateLocation(int column, int row, int sector) {
            this.column = column;
            this.row = row;
            this.sector = sector;
        }

        public void setHealth(int health) {
            this.health = health;
        }

        public int getHealth() {
            return health;
        }

        public void setSheilds(int sheilds) {
            this.sheilds = sheilds;
        }

        public int getSheilds() {
            return sheilds;
        }

        public void setFuelPods(int fuelPods) {
            this.fuelPods = fuelPods;
        }

        public int getFuelPods() {
            return fuelPods;
        }

        public void setTorpedoes (int torpedoes) {
            this.torpedoes = torpedoes;
        }

        public int getTorpedoes() {
            return torpedoes;
        }

        public void setPhasors(int phasors) {
            this.phasors = phasors;
        }

        public int getPhasors() {
            return phasors;
        }
    }
}
