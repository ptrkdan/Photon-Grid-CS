using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Photon_Grid_Port.CS_Files
{

    public class Board {
	public static int size;
	private QuadTree objectsOnBoard;
	private static List<Player> connectedPlayers;
	public static List<Player> alivePlayers;
	public static List<Vehicle> vehicleList;
	private int ready;
	private int maxPlayers;
	public static int vehicleLength;
	public static int vehicleWidth;
	public static int wallLength;
	public static int wallWidth;
	public static int numberOfObjects;
	//constructor
	Board(){
		size = 200000;
		maxPlayers = 2;
		vehicleLength = 3;
		vehicleWidth = 2;
		alivePlayers = new List<Player>();
		connectedPlayers = new List<Player>();
		vehicleList = new List<Vehicle>();
		objectsOnBoard = new QuadTree();
		numberOfObjects = 0;
	}
	
	
	//member functions
	
	public void startgame(){   // may be unnecessary depending on our implementation
		if(checkReady())
		{
			
		}
	}
	
	public bool checkReady(){
		if(ready == 1) 
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public bool gameover(){
		if(alivePlayers.size() == 0)
		{
			return true;
		}
		else return false;
	}
        
      
	public void preparegame()
	{
		ready = 0;
		Player temp = null;
		Vehicle tempV = null;
		for(int i = 0; i < maxPlayers; i++)
		{
			temp = createPlayer(i);
			alivePlayers.Add(temp);
			connectedPlayers.Add(temp);
			Debug.WriteLine(temp.getId());
			//tempV = createSetVehicle(temp.getId(), (int)(Math.random()*360), new Position((int)(Math.random()*size),(int)(Math.random()*size)), 10, 10);
			tempV = createSetVehicle(temp.getId(), (i)*180, new Position(2000*(i+1), 2500), 10, 10);
			Debug.WriteLine("direction is: " + i*180);
			Debug.WriteLine(tempV.getplayerId());
			vehicleList.add(tempV);  // random directions and position for now
			objectsOnBoard.insertVehicle(new VehiclePos(tempV.getplayerId(), tempV.getDirection(), tempV.getPosition(), null), objectsOnBoard.getRoot());
			numberOfObjects++;
			objectsOnBoard.setNumberOfObjects(numberOfObjects);
			if(i == maxPlayers-1)  // this needs many more checks for security purposes
			{
				ready = 1;
				
			}
			
		}
		Debug.WriteLine("prepareGame Complete");
	}
	
	private Player createPlayer(int pId){  // still needs more work for other variables, such as Name
		return new Player(pId);
	}
	
	private Vehicle createSetVehicle(int iD, int direction, Position position, int length, int width){
		return new Vehicle(iD, direction, position, length, width);
	}
	
	
	public void updateBoard(Boolean addWall) throws InterruptedException
	{
		// this method will update the QuadTree structure after placing new walls and moving vehicles
		
		// First, each vehicle needs to move once
		Iterator<Vehicle> it = vehicleList.iterator();
		Vehicle tempV = null;
		for(int i = 0; i < vehicleList.size(); i++)
		{
				tempV = it.next();
                                if(tempV.isAlive())
                                {
                                    //System.out.println("" + tempV.getPosition().x + " " + tempV.getPosition().y);
                                    tempV.setPrevPosition(tempV.getPosition());
                                    tempV.setNewPosition();
                                    //System.out.println("" + tempV.getPosition().x + " " + tempV.getPosition().y);
                                }
				
		}		
		// Next, update QuadTree with new position of vehicle, which will automatically check for collisions
		it = vehicleList.iterator();
		tempV = null;
		for(int i = 0; i < vehicleList.size(); i++)
		{
                    tempV = it.next();
                    if(tempV.isAlive())
                    {
			objectsOnBoard.insertVehicle(new VehiclePos(tempV.getplayerId(), tempV.getDirection(), tempV.getPosition(), tempV.getPrevPosition()), objectsOnBoard.getRoot()); 
			 // inserting a Vehicle should also delete it's old position from the QuadTree.  This is taken care of by the method.
                    }
		}
		
		//Thread.sleep(10);
		
		if(addWall)
		{
			// Third, each vehicle needs to make a new wall and update the QuadTree with the new wall; the QuadTree will automatically check for collisions
			it = vehicleList.iterator();
	                tempV = null;
			for(int i = 0; i < vehicleList.size(); i++)
			{
	                        tempV = it.next();
	                        if(tempV.isAlive())
	                        {
	                            objectsOnBoard.insertWall(tempV.produceWall(), objectsOnBoard.getRoot());
	                            numberOfObjects++;
	                            objectsOnBoard.setNumberOfObjects(numberOfObjects);
	                        }
			}
			//System.out.println("done");
		}
		
		
		//ServerListeningThread.update = true;
		
		//Thread.sleep(10);
	}
	
	public QuadTree getQuadTree()
	{
		return objectsOnBoard;
	}
}

//Vehicle position
class VehiclePos{
	public int playerId;
	public int direction;
	public Position pos;
	public Position prevPos;
	public int radius;
	VehiclePos(int pId, int newDirection, Position position, Position prevPosition)
	{
		playerId = pId;
		direction = newDirection;
		pos = position;
		prevPos = prevPosition;
	}
}
}
