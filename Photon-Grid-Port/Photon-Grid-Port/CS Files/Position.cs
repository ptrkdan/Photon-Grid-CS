using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Photon_Grid_Port.CS_Files
{
    public class Position : ISerializable
    {

        public int x;
        public int y;

        Position(int newX, int newY)
        {
            x = newX;
            y = newY;
        }

    }
}
