
package com.example.photongrid;
import java.io.Serializable;


public class Position implements Serializable{
	public int x;
	public int y;
	
	Position(int newX, int newY)
	{
		x = newX;
		y = newY;
	}
}
