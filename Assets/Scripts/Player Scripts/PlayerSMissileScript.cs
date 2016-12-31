﻿using UnityEngine;
using System.Collections;

public class PlayerSMissileScript : PlayerWeaponScript {
	public GameObject firePrefab;
	public GameObject missileToBeFired;
	public int phlebotinumPercentage; //how much to increase damage

	public override void fireBeam(RaycastHit hit, GameObject autoedEnemy = null) {
		if (!canFireShot) {
			pcs.successFire = false;
			return;
		}
		StartCoroutine (FireDelay ());

		GameObject mtbf = (GameObject) Instantiate (missileToBeFired, currentSpawner.transform.position, Quaternion.Euler (-90, 0, 0));
		PlayerMissileScript pms = mtbf.GetComponent<PlayerMissileScript> ();
		pms.isEnemyTarget (hit, autoedEnemy);
		if (phlebotinum) {
			pms.givePhlebotinumBoost (phlebotinumPercentage);
		}

		Instantiate (firePrefab, currentSpawner.transform.position, Quaternion.identity);

		setAmmo ("d");
		pcs.successFire = true;
	}


}
