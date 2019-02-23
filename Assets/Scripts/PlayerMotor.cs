using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

	private CharacterController controller;
	private Vector3 moveVector;
	private float animationDuration = 1.2f;
	private float speed= 6.0f;
	private float verticalVelocity=0.0f;
	private float gravity =1.0f;
	public bool hiton=false;
	private float deadfor=0.0f;
	public bool star = false;
	private float startTime;


	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		
		if (Time.time-startTime < animationDuration) {
			verticalVelocity -= gravity*Time.deltaTime;
			moveVector.z = speed;
			controller.Move ( moveVector * Time.deltaTime);
			return;
		}
		moveVector = Vector3.zero;
		if (controller.isGrounded) {
			//verticalVelocity = -0.5f;
		} else {
			verticalVelocity -= gravity*Time.deltaTime;
			deadfor += Time.deltaTime;
			if (((int)deadfor) == 2)
				Death ();

		}
		moveVector.x = Input.GetAxisRaw ("Horizontal")*speed;
		moveVector.z = speed;
		if (!hiton && controller.isGrounded) {
			hiton = true;
			moveVector.y = 0;

		}else if (hiton) { //yavaşlaması gerekiyor
			moveVector.y = 80.0f;
			hiton = false;
			deadfor = 0.0f;
		} else if (!hiton &&!controller.isGrounded) {
			moveVector.z = speed;
			moveVector.y = verticalVelocity;
		}


		controller.Move ( moveVector * Time.deltaTime);


	}
	public void SetSpeed(int modifier){
		speed = 6+modifier/2.0f;
	}
	private void OnControllerColliderHit(ControllerColliderHit hit){
		if (Time.time >2.0f)
			return;
		 hiton = true;

	}
	private void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Coin")) {
			other.gameObject.SetActive (false);
			star = true;
		} 
	}
	public void Death(){
		GetComponent<Score> ().onDeath ();
	}


}
