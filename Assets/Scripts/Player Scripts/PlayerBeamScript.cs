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

	void Update () {
		transform.Translate(transform.forward.normalized * speed * Time.deltaTime , Space.World);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Enemy")) {
			Destroy (other.gameObject);
			Destroy (gameObject);
			//Debug.Log ("Destoyed with Enemy Tag");
		} else{
			string[] checkList = new string[]{"Player", "Scrambler"};
			foreach (string tag in checkList) {
				if (other.gameObject.CompareTag (tag)) {
					return;
				}
			}
			Destroy (gameObject);
		}
	}

	void OnDestroy() {
		Instantiate (destroyPs, transform.position, Quaternion.identity); 
	}
		
}
