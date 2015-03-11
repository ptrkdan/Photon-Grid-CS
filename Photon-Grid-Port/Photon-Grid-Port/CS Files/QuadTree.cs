using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace Photon_Grid_Port.CS_Files
{
public class QuadTree : ISerializable {

	private TreeNode root = null;
	private int numberOfObjects;
	
	
	public QuadTree()
	{
		root = new TreeNode();
		setNumberOfObjects(0);
	}
	
	public TreeNode getRoot() {
		return root;
	}
	
/*	public String toString()
	{
		return "";
	}
*/	
	public bool insertWall(Wall wall, TreeNode startNode)
	
	{
		startNode = findNode(wall.getPosition(), startNode);
		if(startNode.isEmpty == false)  // check for collision
		{
			if(startNode.getIsWall() == false && detectCollision1(wall, startNode) == true && wall.getPlayerId() != startNode.getPlayerId())
			{		
				// vehicle collision when placing a wall! how unlucky for the other player :P
				// TODO what to do in this situation?
				
					destroyVehicle(startNode.getPlayerId());
					emptyNode(startNode);
				
				
			}
			else
			{				
					TreeNode newstartNode = new TreeNode();
					int child1=0,child2=0;
					while(child1 == child2)
					{
						startNode.createChildren(); 
						child1 = chooseChild(wall.getPosition(), startNode);
						child2 = chooseChild(startNode.getPosition(), startNode);
						switch(child2)
						{
						case 1:
							copyNodeToChild(startNode, startNode.getChild1());
							newstartNode = startNode.getChild1();
							break;
						case 2:
							copyNodeToChild(startNode, startNode.getChild2());
							newstartNode = startNode.getChild2();
							break;
						case 3:
							copyNodeToChild(startNode, startNode.getChild3());
							newstartNode = startNode.getChild3();
							break;
						case 4:
							copyNodeToChild(startNode, startNode.getChild4());
							newstartNode = startNode.getChild4();
							break;
						}
						if(child1 != child2)
						{//If the new wall and existing wall are not getting the same child number
							switch(child1){
							case 1:
								fillNodeWithWall(wall, startNode.getChild1());
								break;
							case 2:
								fillNodeWithWall(wall, startNode.getChild2());
								break;
							case 3:
								fillNodeWithWall(wall, startNode.getChild3());
								break;
							case 4:
								fillNodeWithWall(wall, startNode.getChild1());
								break;
							}
							emptyNode(startNode); // only leaves can have data!!!
						}
						else{
							emptyNode(startNode); // only leaves can have data!!!
							startNode = newstartNode;
						}
					}
			}
			
		}
		else
		{
			fillNodeWithWall(wall, startNode); // should MARK that node as full and as a wall
		}
		
		return false;
	}
	
	private void copyNodeToChild(TreeNode startNode, TreeNode child ) {
		
		child.setDirection(startNode.getDirection());
		child.setIsWall(startNode.getIsWall());
		child.setPosition(startNode.getPosition());
		//child.setLength(startNode.getLength());
		//child.setWidth(startNode.getWidth());
		child.setPlayerId(startNode.getPlayerId());
		child.isEmpty = startNode.isEmpty;
		//child.setParent(startNode.getParent());		
		
	}
	
	private void copyNodeToParent(TreeNode startNode, TreeNode parent) {
		
		parent.setDirection(startNode.getDirection());
		parent.setIsWall(startNode.getIsWall());
		parent.setPosition(startNode.getPosition());
		//parent.setLength(startNode.getLength());
		//parent.setWidth(startNode.getWidth());
		parent.setPlayerId(startNode.getPlayerId());
		parent.isEmpty = startNode.isEmpty;		
		
	}

	public bool insertVehicle(VehiclePos vehicle, TreeNode startNode)
	{
		//FIRST must remove vehicle's previous position from the QuadTree
		
            if(vehicle.prevPos != null)
		{
			TreeNode tempNode = null;
			tempNode = findNode(vehicle.prevPos, root);
			emptyNode(tempNode);
			if(tempNode.hasChildren())
                        {
                            cleanupLeaves(tempNode.getParent());
                        }
                        
			
		}
                // Next, insert the vehicle into the tree
                if(vehicle.pos.x<0 || vehicle.pos.x>5000 || vehicle.pos.y<0 || vehicle.pos.y>5000){
                    destroyVehicle(vehicle.playerId);
                }
                else{
                    startNode = findNode(vehicle.pos, startNode);
                    if(startNode.isEmpty == false)  // check for collision
                    {
			
                                    if(detectCollision2(vehicle, startNode) == true)// TODO check to see if collision is within tolerance
                                    {
                                            if(startNode.getIsWall() == true)  // we need to know if it's a vehicle collision or not
                                            {
                                            
                                               Debug.WriteLine("Wall destroys playerrr!");
                                              destroyVehicle(vehicle.playerId);
                                            }
                                            else
                                            {
                                                    destroyVehicle(vehicle.playerId);
                                                    destroyVehicle(startNode.getPlayerId());
                                                    emptyNode(startNode);
                                                    cleanupLeaves(startNode.getParent());
                                                    // vehicle collision for both players! how unlucky for the other player :P
                                            }
                                    }
                                    else // no collision, create more leaves
                                    {
                                            TreeNode newstartNode = new TreeNode();
                                            int child1=0,child2=0;
                                            while(child1 == child2){
                                                    startNode.createChildren(); 
                                                    child1 = chooseChild(vehicle.pos, startNode);
                                                    child2 = chooseChild(startNode.getPosition(), startNode);
                                                    switch(child2){
                                                    case 1:
                                                        copyNodeToChild(startNode, startNode.getChild1());
							newstartNode = startNode.getChild1();
							break;
                                                    case 2:
							copyNodeToChild(startNode, startNode.getChild2());
							newstartNode = startNode.getChild2();
							break;
                                                    case 3:
							copyNodeToChild(startNode, startNode.getChild3());
							newstartNode = startNode.getChild3();
							break;
                                                    case 4:
							copyNodeToChild(startNode, startNode.getChild4());
							newstartNode = startNode.getChild4();
							break;
                                                    }
                                                    if(child1 != child2){//If the new wall and existing wall are not getting the same child number
                                                            switch(child1){
                                                            case 1:
                                                                fillNodeWithVehicle(vehicle, startNode.getChild1());
								break;
                                                            case 2:
								fillNodeWithVehicle(vehicle, startNode.getChild2());
								break;
                                                            case 3:
								fillNodeWithVehicle(vehicle, startNode.getChild3());
								break;
                                                            case 4:
								fillNodeWithVehicle(vehicle, startNode.getChild1());
								break;
                                                            }
                                                            emptyNode(startNode); // only leaves can have data!!!
                                                    }
                                                    else{
                                                            emptyNode(startNode); // only leaves can have data!!!
                                                            startNode = newstartNode;
                                                    }
                                            }
                                    }			
                    }
                    else
                    {
                            fillNodeWithVehicle(vehicle, startNode); // should MARK that node as full and as a vehicle
                    }
                }                                     
		return false;
	}
	
	public void destroyVehicle(int playerId)
	{
		// This method will remove the vehicle from the vehicle arraylist in the Board.java class
		// This will also kill the player and remove his name from the alivePlayers arrayList
		foreach(Vehicle  i in Board.vehicleList)
		{
			if(i.equals(playerId))
			{
				Debug.WriteLine("Vehicle Removed"); // for debugging purposes
				i.destroyVehicle();
				Board.numberOfObjects--;
				numberOfObjects--;
			}
		}

		foreach(Player i in Board.alivePlayers)
		{
			if(i.equals(playerId))
			{
                Debug.WriteLine("Player Killed"); // for debugging purposes
				Board.alivePlayers.Remove(i);
			}
		}
	}
	
	public void cleanupLeaves(TreeNode parentNode)
	{
		int count = 0;
		if(parentNode.getChild1() != null) {if(parentNode.getChild1().isEmpty == true) count++;}
		if(parentNode.getChild2() != null) if(parentNode.getChild2().isEmpty == true) count++;
		if(parentNode.getChild3() != null) if(parentNode.getChild3().isEmpty == true) count++;
		if(parentNode.getChild4() != null) if(parentNode.getChild4().isEmpty == true) count++;
		if(count >= 3)
		{
			if(parentNode.getChild1() != null) if(parentNode.getChild1().isEmpty == false) copyNodeToParent(parentNode.getChild1(), parentNode);
			if(parentNode.getChild2() != null) if(parentNode.getChild2().isEmpty == false) copyNodeToParent(parentNode.getChild2(), parentNode);
			if(parentNode.getChild3() != null) if(parentNode.getChild3().isEmpty == false) copyNodeToParent(parentNode.getChild3(), parentNode);
			if(parentNode.getChild4() != null) if(parentNode.getChild4().isEmpty == false) copyNodeToParent(parentNode.getChild4(), parentNode);
			parentNode.setChild1(null);
			parentNode.setChild2(null);
			parentNode.setChild3(null);
			parentNode.setChild4(null);
			
		}
	}

	public bool emptyNode(TreeNode node)
	{
		node.setDirection(0);
		node.setIsWall(true);
		//node.setLength(0);
		node.setPlayerId(0);
		node.setPosition(null);
		//node.setWidth(0);
		node.isEmpty = true;
		node.setRadius(0);
		return true;
	}
	
	
	public bool fillNodeWithWall(Wall wall, TreeNode node)// should MARK that node as full and as a wall
	{
		
		node.setDirection(wall.getDirection());
		node.setIsWall(true);
		//node.setLength(wall.getLength());
		node.setPlayerId(wall.getPlayerId());
		node.setPosition(new Position(wall.getPosition().x, wall.getPosition().y));
		//node.setWidth(wall.getWidth());
		node.setRadius(wall.radius);
		node.isEmpty = false;
		
		return false;
	}
	
	public bool fillNodeWithVehicle(VehiclePos vehicle, TreeNode node)// should MARK that node as full and as a vehicle
	{
		node.setDirection(vehicle.direction);
		node.setIsWall(false);
		//node.setLength(Board.vehicleLength);
		node.setPlayerId(vehicle.playerId);
		node.setPosition(new Position(vehicle.pos.x, vehicle.pos.y));
		//node.setWidth(Board.vehicleWidth);
		node.isEmpty = false;
		node.setRadius(vehicle.radius);
		return false;
	}
	
	public bool detectCollision2(VehiclePos vehicle, TreeNode node)
	{
		// TODO the math and collision tolerance checks
		double distance = Math.Sqrt(Math.Pow(node.getPosition().y-vehicle.pos.y,2)+Math.Pow(node.getPosition().x-vehicle.pos.x,2));
		if(!(distance > (vehicle.radius+node.getRadius())))
                {
			Debug.WriteLine("COLLISION");
			return true;
		}
            double x1,x2,x3,x4,y1,y2,y3,y4;
            x1 = vehicle.pos.x-25*Math.Cos(vehicle.direction);
            x2 = vehicle.pos.x+25*Math.Cos(vehicle.direction);
            y1 = vehicle.pos.y-25*Math.Sin(vehicle.direction);
            y2 = vehicle.pos.y+25*Math.Sin(vehicle.direction);
            x3 = node.getPosition().x-25*Math.Cos(node.getDirection());
            x4 = node.getPosition().x+25*Math.Cos(node.getDirection());
            y3 = node.getPosition().y-25*Math.Sin(node.getDirection());
            y4 = node.getPosition().y+25*Math.Sin(node.getDirection());
            if(((x1-x2)*(y3-y4)-(y1-y2)*(x3-x4)) == 0 || ((x1-x2)*(y3-y4)-(y1-y2)*(x3-x4)) == 0)
                return false;
            double x = ((x1*y2-y1*x2)*(x3-x4)-(x1-x2)*(x3*y4-y3*x4))/((x1-x2)*(y3-y4)-(y1-y2)*(x3-x4));
            double y = ((x1*y2-y1*x2)*(y3-y4)-(y1-y2)*(x3*y4-y3*x4))/((x1-x2)*(y3-y4)-(y1-y2)*(x3-x4));
            if(x1>=x2){
                if(!(x2<=x&&x<=x1))  return false;
            }else {
                if(!(x1<=x&&x<=x2))  return false;
            }
            if(y1>=y2){
                if(!(y2<=y && y<=y1)) return false;
            } else{
                if(!(y1<=y&&y<=y2))  return false;
            }
            if(x3>=x4){
                if(!(x4<=x && x<=x3)) return false;
            } else{
                if(!(x3<=x&&x<=x4))  return false;
            }
            if(y3>=y4){
                if(!(y4<=y && y<=y3)) return false;
            } else{
                if(!(y3<=y&&y<=y4))  return false;
            }
            Debug.WriteLine("COLLISION");
            return true;
	}
	
	public bool detectCollision1(Wall wall, TreeNode node)
	{
		double distance = Math.Sqrt(Math.Pow(node.getPosition().y - wall.getPosition().y,2)+Math.Pow(node.getPosition().x - wall.getPosition().x,2));
		if(!(distance > (wall.radius + node.getRadius()))){
			
			Debug.WriteLine("COLLISION");
			return true;
		}
                double x1,x2,x3,x4,y1,y2,y3,y4;
                x1 = wall.getPosition().x-25*Math.Cos(wall.getDirection());
                x2 = wall.getPosition().x+25*Math.Cos(wall.getDirection());
                y1 = wall.getPosition().y-25*Math.Sin(wall.getDirection());
                y2 = wall.getPosition().y+25*Math.Sin(wall.getDirection());
                x3 = node.getPosition().x-25*Math.Cos(node.getDirection());
                x4 = node.getPosition().x+25*Math.Cos(node.getDirection());
                y3 = node.getPosition().y-25*Math.Sin(node.getDirection());
                y4 = node.getPosition().y+25*Math.Sin(node.getDirection());
                if(((x1-x2)*(y3-y4)-(y1-y2)*(x3-x4)) == 0 || ((x1-x2)*(y3-y4)-(y1-y2)*(x3-x4)) == 0)
                return false;
                double x = ((x1*y2-y1*x2)*(x3-x4)-(x1-x2)*(x3*y4-y3*x4))/((x1-x2)*(y3-y4)-(y1-y2)*(x3-x4));
                double y = ((x1*y2-y1*x2)*(y3-y4)-(y1-y2)*(x3*y4-y3*x4))/((x1-x2)*(y3-y4)-(y1-y2)*(x3-x4));
                if(x1>=x2){
                    if(!(x2<=x&&x<=x1))  return false;
                }else {
                    if(!(x1<=x&&x<=x2))  return false;
                }
                if(y1>=y2){
                    if(!(y2<=y && y<=y1)) return false;
                } else{
                    if(!(y1<=y&&y<=y2))  return false;
                }
                if(x3>=x4){
                    if(!(x4<=x && x<=x3)) return false;
                } else{
                    if(!(x3<=x&&x<=x4))  return false;
                }
                if(y3>=y4){
                    if(!(y4<=y && y<=y3)) return false;
                } else{
                    if(!(y3<=y&&y<=y4))  return false;
                }
                Debug.WriteLine("COLLISION");
                return true;
	}
	
	public TreeNode findNode(Position pos, TreeNode startNode)
	{
		if (startNode.hasChildren() == true)
		{
			TreeNode childNode = new TreeNode();
			// check for proper child to go to
			switch(chooseChild(pos, startNode))
			{
			case 1:
				childNode = startNode.getChild1();
				startNode = findNode(pos, childNode);
				break;
			case 2:
				childNode = startNode.getChild2(); 
				startNode = findNode(pos, childNode);
				break;
			case 3:
				childNode = startNode.getChild3(); 
				startNode = findNode(pos, childNode);
				break;
			case 4:
				childNode = startNode.getChild4(); 
				startNode = findNode(pos, childNode);
				break;
			}
		}
		else
		{
			return startNode;
		}
		
		return startNode;
	}
	
	public int chooseChild(Position pos, TreeNode parentNode)
	{
		if(pos.x < parentNode.getCenter().x)
		{
			if(pos.y < parentNode.getCenter().y)
			{
				return 1;
			}
			else
			{
				return 3;
			}
		}
		else
		{
			if(pos.y < parentNode.getCenter().y)
			{
				return 2;
			}
			else
			{
				return 4;
			}
		}
	}

	public int getNumberOfObjects() {
		return numberOfObjects;
	}

	public void setNumberOfObjects(int numberOfObjects) {
		this.numberOfObjects = numberOfObjects;
	}
	
	public void printleaves(TreeNode node){
		if(node.hasChildren()){
			printleaves(node.getChild1());
			printleaves(node.getChild2());
			printleaves(node.getChild3());
			printleaves(node.getChild4());
		}
		else{
			if(node.isEmpty == false){
                Debug.WriteLine("(" + node.getPosition().x + "," + node.getPosition().y + ")");
			}
		}
	}
	

}
}

