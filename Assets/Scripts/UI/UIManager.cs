using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Slider HealthBar;
	public Slider PrimaryWeaponCooldown;

	public GameObject shotImage1, shotImage2, shotImage3, shotImage4, shotImage5;

	// Use this for initialization
	//healthPoint / maxHealthPoint
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HealthBar.value = 100.0f * PlayerCharacter.pc.healthPoint / PlayerCharacter.pc.maxHealthPoint;
		PrimaryWeaponCooldown.value = 100.0f * PrimaryFire.primaryFire.currentHeat / PrimaryFire.primaryFire.maxHeat;
		int SecondFireShots = SecondaryFire.secondFire.remainingShots;

		shotImage1.SetActive (true);
		shotImage2.SetActive (true);
		shotImage3.SetActive (true);
		shotImage4.SetActive (true);
		shotImage5.SetActive (true);


		if(SecondFireShots < 1) shotImage5.SetActive (false);
		if(SecondFireShots < 2) shotImage4.SetActive (false);
		if(SecondFireShots < 3) shotImage3.SetActive (false);
		if(SecondFireShots < 4) shotImage2.SetActive (false);
		if(SecondFireShots < 5) shotImage1.SetActive (false);
	}
}
