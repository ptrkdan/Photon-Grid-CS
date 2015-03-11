package com.example.photongrid;
public class Player {
	
	private final int id;
	private int status;
	private int score;
	private final String name;
	
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
	public boolean equals(int pId)
	{
		if(pId == id)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	public int getId() {
		return id;
	}

	public int getStatus() {
		return status;
	}

	public int getScore() {
		return score;
	}

	public String getName() {
		return name;
	}

	void setScore(int newScore)
	{
		
	}
	
}
