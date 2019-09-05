using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PrimaryCooldownUI : MonoBehaviour {

    PrimaryFire primaryGun;

	// Use this for initialization
	void Start () {
        primaryGun = PlayerCharacter.pc.GetComponentInChildren<PrimaryFire>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Slider>().value = (1 - primaryGun.currentHeat / primaryGun.maxHeat) * 100;
	}
}
