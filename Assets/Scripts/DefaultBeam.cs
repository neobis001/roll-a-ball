using UnityEngine;
using System.Collections;

public class DefaultBeam: MonoBehaviour {
	//This is for player beam

	// Use this for initialization
	public int speed = 6;

	private GameObject player;

	void Start () {
		//Debug.Log (transform.forward);
		//Debug.Log (player.name);
		Destroy (gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (transform.forward);
		//transform.Translate (transform.forward * speed * Time.deltaTime);
		transform.Translate(transform.forward.normalized * speed * Time.deltaTime , Space.World);
	}

	void OnTriggerEnter(Collider other) {
		if (!other.gameObject.CompareTag ("Player")) {
			Destroy (gameObject);
		}
	}
}
