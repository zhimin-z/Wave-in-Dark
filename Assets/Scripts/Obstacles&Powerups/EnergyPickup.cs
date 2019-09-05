using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : Powerups
{
	[SerializeField] private AudioClip m_WaveReloadSound;
	private AudioSource m_AudioSource;
	void Start ()
	{
		m_AudioSource = GetComponent<AudioSource>();

	}
    protected override void Awake()
    {

    }

	public void PlayWaveReloadSound()
	{
		if (m_WaveReloadSound)
		{
			m_AudioSource.PlayOneShot(m_WaveReloadSound);
		}
	}

    protected override void OnTriggerEnter(Collider other)
    {

		if (other.gameObject.tag == "Player") {
			SecondaryFire.secondFire.addEnergy ();
			PlayWaveReloadSound ();
			Destroy(gameObject);
		}


    }
}
