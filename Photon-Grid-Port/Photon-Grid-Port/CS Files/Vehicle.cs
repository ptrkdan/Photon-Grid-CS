using System;
namespace Photon_Grid_Port.CS_Files
{
    public class Vehicle
    {
        private int speed;
        private int direction;
        private Position position;
        private Position prevPosition;

        private sealed int playerId;
        private sealed int length;
        private sealed int width;
        private int boostCooldown;
        private int radius;
        private bool destroyed;
        private bool boost = false;
        private long current;
        public long last = -1;

        public Vehicle(int newPlayerId, int startDirection, Position startPosition, int fixedLength, int fixedWidth)
        {
            playerId = newPlayerId;
            direction = startDirection;
            position = startPosition;
            length = fixedLength;
            width = fixedWidth;
            radius = 10;
            speed = 7;
            destroyed = false;

        }

        #region GET methods

        public int getDirection()
        {
            return direction;
        }
        public Position getPosition()
        {
            return position;
        }

        public int getPlayerId()
        {
            return playerId;
        }

        public int getLength()
        {
            return length;
        }

        public int getWidth()
        {
            return width;
        }

        public int getCooldown()
        {
            return boostCooldown;
        }

        #endregion

        public bool equals(Vehicle vehicle)
        {
            if (vehicle.getPlayerId() == playerId)
                return true;
            else
                return false;
        }

        public bool equals(int pId)
        {
            if (pId == playerId)
                return true;
            else
                return false;
        }

        public Wall produceWall()
        {
            return new Wall(playerId, prevPosition, direction, Board.wallLength, Board.wallWidth);
        }

        public void setNewPosition()
        {
            position.x += (int)((double)speed * Math.Cos(Math.PI * direction / 180));
            position.y += (int)((double)speed * Math.Sin(Math.PI * direction / 180));
        }

        public int speedBoost()
        {
            current = Environment.TickCount;
            if ((current - last) > 30000 || last == -1)
            {
                speed = speed * 2;
                last = Environment.TickCount;
                boost = true;
            }

            return 0;
        }

        public bool isBoost()
        {
            return boost;
        }

        public void stopBoost()
        {
            speed = speed / 2;
            boost = false;
        }

        public void changeDirection(int newDirection)
        {
            direction = newDirection;
        }

        public Position getPrevPosition()
        {
            return prevPosition;
        }

        public void setPrevPosition(Position prevPosition)
        {
            this.prevPosition = new Position(prevPosition.x, prevPosition.y);
        }

        public int getRadius()
        {
            return radius;
        }

        public void setRadius(int radius)
        {
            this.radius = radius;
        }

        public void destroyVehicle()
        {
            destroyed = true;
        }

        public bool isAlive()
        {
            if (destroyed == false)
                return true;
            else
                return false;
        }
    }
}
