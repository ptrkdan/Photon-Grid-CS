using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Photon_Grid_Port.CS_Files
{
    public class TreeNode : ISerializable {

	    private	Position position;
	    private int direction;
	    private int length;
	    private int width;
	    private bool isWall;
	    private int playerId;
	    public bool isEmpty;
	    private int radius;
	
	    private TreeNode parent;
	    private TreeNode child1;
	    private TreeNode child2;
	    private TreeNode child3;
	    private TreeNode child4;
	
	    private Position center;
	
	    public TreeNode()
	    {
		    position = null;
		    direction = 0;
		    length = Board.size;
		    width = Board.size;
		    isWall = true;
		    playerId = 0;
		    isEmpty = true;
		    radius = Board.wallLength;
		
		    setCenter(new Position(width/2, length/2));
		
		    parent = null;
		    child1 = null;
		    child2 = null;
		    child3 = null;
		    child4 = null;
	    }
	    /*
	    public TreeNode(Position pos, int dir, int len, int wid, int wall, int pId)
	    {
		    position = pos;
		    direction = dir;
		    length = len;
		    width = wid;
		    isWall = wall;
		    playerId = pId;
		
		    parent = null;
		    child1 = null;
		    child2 = null;
		    child3 = null;
		    child4 = null;
	    }
	    */

	    public Position getPosition() {
		    return position;
	    }

	    public void setPosition(Position position) {
		    this.position = position;
	    }

	    public int getDirection() {
		    return direction;
	    }

	    public void setDirection(int direction) {
		    this.direction = direction;
	    }

	    public int getLength() {
		    return length;
	    }

	    public void setLength(int length) {
		    this.length = length;
	    }

	    public int getWidth() {
		    return width;
	    }

	    public void setWidth(int width) {
		    this.width = width;
	    }

	    public bool getIsWall() {
		    return isWall;
	    }

	    public void setIsWall(bool isWall) {
		    this.isWall = isWall;
	    }

	    public int getPlayerId() {
		    return playerId;
	    }

	    public void setPlayerId(int playerId) {
		    this.playerId = playerId;
	    }

	    public TreeNode getParent() {
		    return parent;
	    }

	    public void setParent(TreeNode parent) {
		    this.parent = parent;
	    }

	    public TreeNode getChild1() {
		    return child1;
	    }

	    public void setChild1(TreeNode child1) {
		    this.child1 = child1;
	    }

	    public TreeNode getChild2() {
		    return child2;
	    }

	    public void setChild2(TreeNode child2) {
		    this.child2 = child2;
	    }

	    public TreeNode getChild3() {
		    return child3;
	    }

	    public void setChild3(TreeNode child3) {
		    this.child3 = child3;
	    }

	    public TreeNode getChild4() {
		    return child4;
	    }

	    public void setChild4(TreeNode child4) {
		    this.child4 = child4;
	    }
	
	    public bool hasChildren()
	    {
		    if(child1 == null && child2 ==null && child3 == null && child4 ==null)
			    return false;
		    else 
			    return true;
	    }
	
	    public void createChildren()  // VERY important method.  This must calculate the centers for each child it creates.
	    {
		    child1 = new TreeNode();
		    child2 = new TreeNode();
		    child3 = new TreeNode();
		    child4 = new TreeNode();
		    child1.setLength(length/2);
		    child1.setWidth(width/2);
		    child1.setCenter(new Position(center.x-child1.getWidth()/2,center.y-child1.getLength()/2));
		    child2.setLength(length/2);
		    child2.setWidth(width/2);
		    child2.setCenter(new Position(center.x+child2.getWidth()/2,center.y-child2.getLength()/2));
		    child3.setLength(length/2);
		    child3.setWidth(width/2);
		    child3.setCenter(new Position(center.x-child3.getWidth()/2,center.y+child3.getLength()/2));
		    child4.setLength(length/2);
		    child4.setWidth(width/2);
		    child4.setCenter(new Position(center.x+child4.getWidth()/2,center.y+child4.getLength()/2));
		    child1.setParent(this);
		    child2.setParent(this);
		    child3.setParent(this);
		    child4.setParent(this);
	    }

	    public Position getCenter() {
		    return center;
	    }

	    public void setCenter(Position center) {
		    this.center = center;
	    }

	    public int getRadius() {
		    return radius;
	    }

	    public void setRadius(int radius) {
		    this.radius = radius;
	    }

    
    }

}
