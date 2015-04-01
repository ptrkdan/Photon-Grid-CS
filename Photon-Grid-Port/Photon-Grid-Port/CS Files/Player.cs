using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon_Grid_Port.CS_Files
{
    public class Player
    {

        private readonly int id;
        private int status;
        private int score;
        private readonly String name;
        private bool isConnected;
        private bool isAlive;
        private Vehicle vehicle;

        //Constructor
        public Player()
        {
            id = 0;
            name = System.String.Empty;
            status = 0;
            score = 0;
            isConnected = false;
            isAlive = false;
            vehicle = null;
        }

        public Player(int pId, Vehicle newVehicle)
        {
            id = pId;
            name = "testPlayer";
            status = 0;
            score = 0;
            isConnected = true;
            isAlive = true;
            vehicle = newVehicle;
        }


        // set methods
        void setStatus(int newStatus)
        {

        }
        public bool equals(int pId)
        {
            if (pId == id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int getId()
        {
            return id;
        }

        public int getStatus()
        {
            return status;
        }

        public int getScore()
        {
            return score;
        }

        public String getName()
        {
            return name;
        }

        void setScore(int newScore)
        {

        }

        public bool getIsConnected()
        {
            return isConnected;
        }

        public bool getIsAlive()
        {
            return isAlive;
        }

        public Vehicle getVehicle()
        {
            return Vehicle;
        }
    }
}
