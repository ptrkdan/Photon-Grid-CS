namespace Photon_Grid_Port.CS_Files
{
    public class Wall
    {
        private sealed int playerId;
        private sealed Position position;
        private sealed int direction;
        private sealed int length;
        private sealed int width;
        public sealed int radius;

        public Wall(int newPlayerId, Position newPosition, int newDirection, int newLength, int newWidth)
        {
            playerId = newPlayerId;
            position = new Position(newPosition.x, newPosition.y);
            direction = newDirection;
            length = newLength;
            width = newWidth;
            radius = 10;
        }

        /* NOTE: Get methods only; Walls do not ever change until destroyed at the end of the game. */
        public int getPlayerId()
        {
            return playerId;
        }

        public Position getPosition()
        {
            return position;
        }

        public int getDirection()
        {
            return direction;
        }

        public int getLength() 
        {
            return length;
        }

        public int getWidth()
        {
            return width;
        }
    }
}
