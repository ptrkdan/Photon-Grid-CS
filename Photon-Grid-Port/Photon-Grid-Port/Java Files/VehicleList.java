package com.example.photongrid;



import com.jme3.math.Quaternion;
import com.jme3.math.Vector3f;
import com.jme3.scene.Geometry;
import com.jme3.scene.Node;
import com.jme3.scene.Spatial;


public class VehicleList {
	Vector3f playerTranslation;
	Quaternion playerRotation;
	
	
	public VehicleList(Vector3f trans, Quaternion rot)
	{
		playerTranslation = new Vector3f(trans);  // we want new objects in order to avoid passing by reference
		playerRotation = new Quaternion(rot);
	}

	

}
