using UnityEngine;
using System.Collections;

public class PlayerBeamScript: MonoBehaviour {
	public int speed = 6;
	public GameObject fireSound;
	public GameObject destroyPs;

	void Start () {
		Destroy (gameObject, 2);
		Instantiate (fireSound, transform.position, Quaternion.identity);
	}

	//move forward in World Space
	void Update () {
		transform.Translate(transform.forward.normalized * speed * Time.deltaTime , Space.World);
	}
		
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Enemy")) {
			Destroy (other.gameObject);
			Destroy (gameObject);
		} else{
			string[] checkList = new string[]{"Player", "Scrambler", "Temporary"};
			foreach (string tag in checkList) {
				if (other.gameObject.CompareTag (tag)) {
					return;
				}
			}
			Destroy (gameObject);
		}
	}

	//when the beam is destroyed, instantiate destroyPs as an explosion
	void OnDestroy() {
		Instantiate (destroyPs, transform.position, Quaternion.identity); 
	}
		
}
