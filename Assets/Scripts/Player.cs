using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

	public static Player instance;

	Rigidbody body;
	GameObject Cam;
	Vector3 moveDirection;
	Animator animator;
	public float speed;
	public float mouseSensitivity = 0.3f;
	public Transform pov, camContain;
	float xClamp = 0;
	[Range(0.0f, 1.0f)]
	public float Smoothing = 0;
	public float jumpSpeed = 20;
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;
	[HideInInspector]
	public bool isPaused = false;
	[HideInInspector]
	public bool canPlay = false;

	static Vector3 mVel = Vector3.zero;

	// Use this for initialization
	void Start () {
		instance = this;

		body = GetComponent<Rigidbody>();
		Cam = GameObject.FindGameObjectWithTag("MainCamera");
		animator = GetComponentInChildren<Animator>();
	}

	public void reloadObjects() {
		body = GetComponent<Rigidbody>();
		Cam = GameObject.FindGameObjectWithTag("MainCamera");
		animator = GetComponentInChildren<Animator>();
	}

	private void Update() {
		if (!isPaused && canPlay) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;

			RotateCamera();
			move();
		}
	}

	// Update is called once per frame
	void FixedUpdate () {

		//if (Input.GetKey(KeyCode.W)) {
		//	body.velocity = transform.forward * speed;
		//}

		//if (Input.GetKey(KeyCode.S)) {
		//	body.velocity = -transform.forward * speed;
		//}

		//if (Input.GetAxis("Mouse X") < 0) {
		//	//Code for action on mouse moving left

		//}
		//if (Input.GetAxis("Mouse X") > 0) {
		//	//Code for action on mouse moving right
		//	print("Mouse moved right");
		//}

	}

	private void LateUpdate() {
		//jump higher on hold
		if (body.velocity.y < 0) {
			body.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		} else if (body.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
			body.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
	}

	void move() {
		//moveDirection = new Vector3((transform.forward * (Input.GetKey(KeyCode.D) ? speed : (Input.GetKey(KeyCode.A) ? -speed : 0))).x, 0, (transform.forward * (Input.GetKey(KeyCode.W) ? speed : (Input.GetKey(KeyCode.S)? -speed : 0))).z);
		moveDirection = (transform.forward * Input.GetAxis("Vertical") * speed) + (transform.right * Input.GetAxis("Horizontal") * speed);
		//moveDirection = transform.TransformDirection(moveDirection);

		if (Input.GetKeyDown(KeyCode.Space)) {
			moveDirection.y = jumpSpeed;
		} else {
			moveDirection.y = body.velocity.y;
		}

		body.velocity = Vector3.SmoothDamp(body.velocity, moveDirection, ref mVel, Smoothing);

		//update animations
		animator.SetBool("isRunning", body.velocity.x > 0 || body.velocity.z > 0);
	}

	void RotateCamera() {
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		float rotX = mouseX * mouseSensitivity;
		float rotY = mouseY * mouseSensitivity;

		xClamp -= rotY;

		Vector3 rotPlayer = transform.rotation.eulerAngles;
		Vector3 rotCam = pov.rotation.eulerAngles;
		//Vector3 rotCamC = camContain.rotation.eulerAngles;

		rotCam.x -= rotY;
		//rotCamC.x -= rotY;
		rotPlayer.z = 0;
		rotCam.z = 0;
		//rotCamC.z = 0;
		rotPlayer.y += rotX;
		rotCam.y += rotX;
		//rotCamC.y += rotX;

		if (xClamp > 90) {
			rotCam.x = 90;
			//rotCamC.x = 90;
			xClamp = 90;
		} else if (xClamp < -89) {
			rotCam.x = 270;
			//rotCamC.x = 270;
			xClamp = -89;
		}

		rotPlayer.z = 0;
		rotCam.z = 0;

		transform.rotation = Quaternion.Euler(rotPlayer);
		pov.rotation = Quaternion.Euler(rotCam);
		//camContain.rotation = Quaternion.Euler(rotCamC);
 	}
}
