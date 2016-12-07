using UnityEngine;
using System.Collections;

public class DefaultBeam: MonoBehaviour
{
	//This is for player beam

	// Use this for initialization
	public int speed = 6;
	public GameObject fireSound;
	public GameObject destroyPs;

	private GameObject player;

	void Start ()
	{
		//Debug.Log (transform.forward);
		//Debug.Log (player.name);
		Destroy (gameObject, 2);
		Instantiate (fireSound, transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log (transform.forward);
		//transform.Translate (transform.forward * speed * Time.deltaTime);
		transform.Translate (transform.forward.normalized * speed * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter (Collider other)
	{
		//ignore player shields
		if (other.CompareTag ("Shield"))
			return;

		if (!other.CompareTag ("Player")) {
			Destroy (gameObject);
			Instantiate (destroyPs, transform.position, Quaternion.identity);
		}
	}
}
