using System.Collections.Generic;
using System.Diagnostics;

namespace Photon_Grid_Port.CS_Files
{

    #region Properties
    
	
    public class Board {

        public readonly int MAX_PLAYERS = 2;
	    public static int size;
	    private QuadTree objectsOnBoard;
        public static List<Player> playerList;
	    private bool ready;
	    public static int vehicleLength;
	    public static int vehicleWidth;
	    public static int wallLength;
	    public static int wallWidth;
	    public static int numberOfObjects;

        #endregion

        #region Constructors
        //constructor
	    public Board(){
		    size = 200000;
		    vehicleLength = 3;
		    vehicleWidth = 2;
		    objectsOnBoard = new QuadTree();
		    numberOfObjects = 0;
	    }

        #endregion

        #region Get Methods

        public QuadTree getQuadTree()
        {
            return objectsOnBoard;
        }

        #endregion

        #region Game Functions
        //member functions
	
	    public void startGame(){   // may be unnecessary depending on our implementation
		    if(checkReady())
		    {
			
		    }
	    }
	
	    public bool checkReady(){
		    if(ready) 
		    {
			    return true;
		    }
		    else
		    {
			    return false;
		    }
	    }
	
	    public bool gameOver(){
            int alivePlayers = 0;
            foreach(Player player in playerList )
            {
                if(player.getIsAlive() == true)
                {
                    alivePlayers++;
                }
            }
		    if (alivePlayers == 0)
			    return true;
		    else 
                return false;
	    }
              
	    public void prepareGame()
	    {
		    ready = false;
		    Player newPlayer = null; 
		    Vehicle newVehicle = null;

            for (int i = 0; i < MAX_PLAYERS; i++)
		    {
                newVehicle = createSetVehicle(i, (i) * 180, new Position(2000 * (i + 1), 2500), vehicleLength, vehicleWidth);
			    newPlayer = createPlayer(i, newVehicle);
			    Debug.WriteLine("Created Player and Vehicle #" + newPlayer.getId());
                objectsOnBoard.insertVehicle(new VehiclePos(newPlayer.getVehicle().getPlayerId(), newPlayer.getVehicle().getDirection(), newPlayer.getVehicle().getPosition(), null), objectsOnBoard.getRoot());
			    numberOfObjects++;
			    objectsOnBoard.setNumberOfObjects(numberOfObjects);

                if (i == MAX_PLAYERS - 1)  // this needs many more checks for security purposes
			    {
				    ready = true;
			    }
		    }
		    if(ready) Debug.WriteLine("prepareGame Complete");
	    }

        public void updateBoard(bool addWall) // throws InterruptedException // this method will update the QuadTree structure after placing new walls and moving vehicles
        {
            moveVehicles();
            updateQuadTreeVehicles();
            //Thread.sleep(10);
            addWalls(addWall);
            //ServerListeningThread.update = true;
            //Thread.sleep(10);
        }
        #endregion

        #region Private Functions
        /// <summary>
        /// create a new player and include the previous vehicle reference
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        private Player createPlayer(int pId, Vehicle vehicle){  // still needs more work for other variables, such as Name
		    return new Player(pId, vehicle);
	    }
	    /// <summary>
        /// create a new vehicle based on the iteration.
	    /// </summary>
	    /// <param name="iD"></param>
	    /// <param name="direction"></param>
	    /// <param name="position"></param>
	    /// <param name="length"></param>
	    /// <param name="width"></param>
	    /// <returns></returns>
	    private Vehicle createSetVehicle(int iD, int direction, Position position, int length, int width){
		    return new Vehicle(iD, direction, position, length, width);
	    }

        /// <summary>
        /// First, each vehicle needs to move once 
        /// </summary>
        private void moveVehicles() // First, each vehicle needs to move once
        {
            foreach (Player player in playerList)
            {
                if (player.getIsAlive() == true)
                {
                    //System.out.println("" + tempV.getPosition().x + " " + tempV.getPosition().y);
                    player.getVehicle().setPrevPosition(player.getVehicle().getPosition());
                    player.getVehicle().setNewPosition();
                    //System.out.println("" + tempV.getPosition().x + " " + tempV.getPosition().y);
                }

            }
        }
	    /// <summary>
        /// Next, update QuadTree with new position of vehicle, which will automatically check for collisions
	    /// </summary>
        private void updateQuadTreeVehicles()
        {
            foreach (Player player in playerList)
            {
                if (player.getIsAlive() == true)
                {
                    objectsOnBoard.insertVehicle(new VehiclePos(player.getVehicle().getPlayerId(), player.getVehicle().getDirection(), player.getVehicle().getPosition(),
                                                        player.getVehicle().getPrevPosition()), objectsOnBoard.getRoot());
                    // inserting a Vehicle should also delete it's old position from the QuadTree.  This is taken care of by the method.
                }
            }
        }
            /// <summary>
            /// Third, each vehicle needs to make a new wall and update the QuadTree with the new wall; the QuadTree will automatically check for collisions
            /// </summary>
            /// <param name="addWall"></param>
        private void addWalls(bool addWall)
        {
            if (addWall)
            {

                foreach (Player player in playerList)
                {
                    if (player.getIsAlive() == true)
                    {
                        objectsOnBoard.insertWall(player.getVehicle().produceWall(), objectsOnBoard.getRoot());
                        numberOfObjects++;
                        objectsOnBoard.setNumberOfObjects(numberOfObjects);
                    }
                }
                //System.out.println("done");
            }
        }
	

        #endregion

    }

}
