using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour
{
	public float speed = 1;
	private Rigidbody2D myBody;
	private SpriteRenderer sprite;
	
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		sprite = GetComponent<SpriteRenderer>();
	}
	
	void FixedUpdate () {
		float value = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}
		
		if(value < 0) {
			sprite.flipX = true;
		} else if(value > 0) {
			sprite.flipX = false;
		}
	}
	
	void Update () {
		
	}
}
