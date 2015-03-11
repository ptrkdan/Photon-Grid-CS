namespace Photon_Grid_Port.CS_Files
{
    public class VehiclePos
    {
        public int playerId;
        public int direction;
        public Position pos;
        public Position prevPos;
        public int radius;
        
        public VehiclePos(int pId, int newDirection, Position position, Position prevPosition)
        {
            playerId = pId;
            direction = newDirection;
            pos = position;
            prevPos = prevPosition;
        }
    }
}
