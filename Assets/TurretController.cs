using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour
{
	/// <summary>
	/// Attach this script to a gun barrel that will follow the player
	/// Position the gun barrel onto the player
	/// D0 NOT make the gun barrel a child of player
	/// </summary>

	//What GO to follow
	public Transform player;
	//keep a relative offset to player
	private Vector3 offset;
	//a plane to allow a raycast to hit for aiming
	Plane pointPlane;
	//laser prefab
	public GameObject laserPrefab;
	//how far from the barrel the laser spawns
	private float spawnDist = 1f;

	//how long before you can fire another normal shot
	float fireCooldown = 1f;
	//a variable to hold how long remaining cooldown
	float coolRemaining = 0f;

	[Header ("Upgrades")]
	//fire in 4 directions
	public bool compassFire = false;
	//hold down click
	public bool rapidFire = false;
	//how long before you can fire another rapid shot
	public float rapidFireRate = 0.25f;


	// Use this for initialization
	public void Start ()
	{
		//set the offset from the barrel and player
		offset = transform.position - player.transform.position;
	}

	void Update ()
	{
		//update the position of the barrel to the player
		transform.position = player.position + offset;
		//create a ray from the camera's perspective
		Ray pointRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		//set the plane on the same y positon as the barrel
		pointPlane.SetNormalAndPosition (Vector3.up, transform.position);

		//variable to hold the distance down the ray cast from camera
		float distance;
		if (pointPlane.Raycast (pointRay, out distance)) {	
			//store the positon where the ray hits the plane
			Vector3 lookPoint = pointRay.GetPoint (distance);
			//point the barrel towards that point
			transform.LookAt (lookPoint);
		}

		//if there is still time remamaining on cooldown
		if (coolRemaining > 0)
			coolRemaining -= Time.deltaTime;//remove some time from cooldown
		
		//if the mouse1 button is held down and rapidfire is enabled
		if (Input.GetButton ("Fire1") && rapidFire) {
			//if there is no cooldown remaining
			if (coolRemaining <= 0) {
				//add cooldown time
				coolRemaining = rapidFireRate;
				//fire at will!
				fire ();
			}

			//otherwise if mousebutton 1 was pressed
		} else if (Input.GetButtonDown ("Fire1")) {		
			if (coolRemaining <= 0) {
				coolRemaining = fireCooldown;
				fire ();
			}

		}



	}
	//update


	void fire ()
	{
		//create an array of all firing directions
		Vector3[] dirs = new Vector3[]{ transform.forward, transform.right, -transform.forward, -transform.right };
		//temp store of spawned object
		GameObject beam;

		//loop through all directions
		for (int i = 0; i < dirs.Length; i++) {
			//create an object on transform.position and the position around the GO, and set its rotation to the direction around the transform
			beam = GameObject.Instantiate (laserPrefab, transform.position + (dirs [i] * spawnDist), Quaternion.LookRotation (dirs [i])) as GameObject;

			//disable all but 1 of the audio sources (reduce noise)
			if (i != 0)
				beam.GetComponent<AudioSource> ().enabled = false;
			//if we are not compassfiring, just shoot one and done
			if (!compassFire)
				break;
		}	


	}


}