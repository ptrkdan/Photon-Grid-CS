package com.example.photongrid;


import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;


public class Accelerometer implements SensorEventListener{

	private float mLastX, mLastY, mLastZ;

	private boolean mInitialized;

	private SensorManager mSensorManager;

	private Sensor mAccelerometer;	
	
	public Accelerometer() {
		mInitialized = false;
		mAccelerometer = mSensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
		mSensorManager.registerListener(this, mAccelerometer, SensorManager.SENSOR_DELAY_NORMAL);
		}
		
	
	public void onSensorChanged(SensorEvent event) {
		// TODO Auto-generated method stub
		float x = event.values[0];
		float y = event.values[1];
		float z = event.values[2];
		if (!mInitialized) {
		mLastX = x;
		mLastY = y;
		mLastZ = z;
		
		mInitialized = true;
		} else {
		float deltaX = Math.abs(mLastX - x);
		float deltaY = Math.abs(mLastY - y);
		float deltaZ = Math.abs(mLastZ - z);
		mLastX = deltaX;
		mLastY = deltaY;
		mLastZ = deltaZ;
		
		}
		
	}
	@Override
	public void onAccuracyChanged(Sensor sensor, int accuracy) {
	// can be safely ignored for this demo
	}
}
