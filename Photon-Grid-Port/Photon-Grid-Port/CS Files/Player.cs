using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon_Grid_Port.CS_Files
{
    public class Player
    {

        private sealed int id;
        private int status;
        private int score;
        private sealed String name;

        //Constructor
        Player()
        {
            id = 0;
            name = new String();
            status = 0;
            score = 0;
        }

        Player(int pId)
        {
            id = pId;
            name = "testPlayer";
            status = 0;
            score = 0;
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
    }
}
