using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour {

	public HeroRabit Rabit;

	void Update () 
	{
		Vector3 newCameraPosition = transform.position;
		newCameraPosition.x = Rabit.transform.position.x;
		newCameraPosition.y = Rabit.transform.position.y;
		transform.position = newCameraPosition;
	}
}