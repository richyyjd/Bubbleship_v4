﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Damageable : MonoBehaviour, IDamageable{

	public int damage = 0;
	public int life = 0;

	public void Damage(int damageTaken){
		life -= damageTaken;
		if (gameObject.tag == "Player" && life>0 && life<4) {
			GameObject.FindGameObjectWithTag("HP"+(life-1)).GetComponent<Image>().sprite = null;
		}
		//Debug.Log ("Damageable "+life);
		//if his life less than 1, and is killable then kill it
		IKillable killable = gameObject.GetComponent<IKillable> ();
		if (killable != null && life <= 0) {
			killable.Kill();
		}
	}

	public int GetDamageTaken(){ return damage;}
	public int GetLife(){return life;}
}
