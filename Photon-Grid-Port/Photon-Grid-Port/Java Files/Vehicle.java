
package com.example.photongrid;

public class Vehicle {

	private int speed;
	private int direction;
	private Position position;
	private Position prevPosition;
	
	private final int playerId;
	private final int length;
	private final int width;
	private int boostCooldown;
	private int radius;
        private boolean destroyed;
        private boolean boost = false;
        private long current;
	public long last = -1;
	
	//Constructors
	
	Vehicle(int newPlayerId, int startDirection, Position startPosition, int fixedLength, int fixedWidth)
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
	
	
	// get Methods
	
	public boolean equals(Vehicle veh)
	{
		if(veh.getplayerId() == playerId)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public boolean equals(int pId)
	{
		if(pId == playerId)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
		
	public int getSpeed()
	{
		return speed;
	}
	
	public int getDirection()
	{
		return direction;
	}
	public Position getPosition()
	{
		return position;
	}
	
	public int getplayerId()
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
	
	// Member functions
	
	public Wall produceWall()
	{
		return new Wall(playerId, prevPosition, direction, Board.wallLength, Board.wallWidth);
	}
	
	
	//Sets new position of the vehicle based on the speed and direction
	public void setNewPosition(){
		
		position.x += (int)((double)speed*Math.cos(Math.toRadians(direction)));
		position.y += (int)((double)speed*Math.sin(Math.toRadians(direction)));
	}
	
	public int speedBoost()
	{   
                current = System.currentTimeMillis();
                if((current - last)>30000 || last == -1){
                    speed = speed * 2;
                    last = System.currentTimeMillis();
                    boost = true;
                }
		return 0;
	}
	
        public boolean isBoost(){
            return boost;
        }
        
        public void stopBoost(){
            speed = speed/2;
            boost = false;
        }
	public void changeDirection(int newDirection)
	{
		direction = newDirection;
                //System.out.println("Direction: "+direction);
	}


	public Position getPrevPosition() {
		return prevPosition;
	}


	public void setPrevPosition(Position prevPosition) {
		this.prevPosition = new Position(prevPosition.x, prevPosition.y);
	}


	public int getRadius() {
		return radius;
	}


	public void setRadius(int radius) {
		this.radius = radius;
	}
        
        public void destroyVehicle()
        {
            destroyed = true;
        }
        
        public boolean isAlive()
        {
            if(destroyed == false)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
	
}
