using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour {

	public HeroRabit rabit;

	void Update () 
	{
		Vector3 new_camera_position = transform.position;
		new_camera_position.x = rabit.transform.position.x;
		new_camera_position.y = rabit.transform.position.y;
		transform.position = new_camera_position;
	}
}