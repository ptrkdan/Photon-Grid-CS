package com.example.photongrid;



import com.jme3.math.Quaternion;
import com.jme3.math.Vector3f;
import com.jme3.scene.Geometry;
import com.jme3.scene.Node;
import com.jme3.scene.Spatial;


public class WallList {
	Vector3f wallTranslation;
	Quaternion wallRotation;
	int playerID;
	
	
	public WallList(Vector3f trans, Quaternion rot, int pID)
	{
		wallTranslation = new Vector3f(trans);  // we want new objects in order to avoid passing by reference
		wallRotation = new Quaternion(rot);
		playerID = pID;
	}

	

}
