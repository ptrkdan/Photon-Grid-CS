package com.example.photongrid;

public class UpdateQuadTree implements Runnable {

Board board;
	
	UpdateQuadTree(Board gameBoard)
	{
		board = gameBoard;
	}
	
	@Override
	public void run() {
		// TODO Auto-generated method stub
		while(Board.alivePlayers.size() > 1)
		{
		try {
			// First pull direction change since last update from the Server, where it has been storing it from the user input messages
			// Next, calculate the new direction from current direction and direction change and reset Server's variable back to zero
			// Next, calculate the AI's direction change.
			// Then updateBoard()
			//Board.vehicleList.get(0).changeDirection(Board.vehicleList.get(0).getDirection() + Server.dirChangeSinceLastUpdate);
			//Server.dirChangeSinceLastUpdate = 0;
			//System.out.println("updating game board");
			//int aiChange = (int)(Math.random()*60 - 30);
			//System.out.println("AI will change direction by " + aiChange + " degrees");
			//Board.vehicleList.get(1).changeDirection(Board.vehicleList.get(1).getDirection() + aiChange); 
			if(StateManager.c == StateManager.wallSpacing)
			{

				board.updateBoard(true);
			}
			else
			{
				board.updateBoard(false);
			}
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		/*try {
			Thread.sleep(500);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}*/
		}
		System.out.println("Game is Over!");
		//ConnectionThread connThread = new ConnectionThread(1);
		//Thread conn = new Thread(connThread);
		//conn.start();
	}

}
