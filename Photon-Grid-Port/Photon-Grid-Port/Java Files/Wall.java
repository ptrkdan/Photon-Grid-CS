package com.example.photongrid;
public class Wall {

	private final int playerId;
	private final Position position;   // or we can make a Struct for this
	private final int direction;
	private final int length;
	private final int width;
	public final int radius;
        
	//Constructor
	Wall(int newPlayerId, Position newPosition, int newDirection, int newLength, int newWidth)
	{
		playerId = newPlayerId;
		position = new Position(newPosition.x, newPosition.y);
		direction = newDirection;
		length = newLength;
		width = newWidth;
        radius = 10;
	}
	
	//Get Methods only;  Walls do not ever change until destroyed at the end of the game
	
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

	public int getWidth() {
		return width;
	}
	
}
