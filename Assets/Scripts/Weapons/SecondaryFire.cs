using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryFire : MonoBehaviour {
    public static SecondaryFire secondFire;

	// public
	public int maxShots;
	public GameObject LightWaveBullet;

	[HideInInspector]
	public int remainingShots;

    //private
    [SerializeField] private AudioClip m_WaveShotSound;
    [SerializeField] private AudioClip m_WaveReloadSound;
    private AudioSource m_AudioSource;

    // Use this for initialization
    void Start ()
    {
		secondFire = this;
        remainingShots = maxShots;
        m_AudioSource = GetComponent<AudioSource>();

    }

	public void addEnergy(){
		if (remainingShots < maxShots) {
			remainingShots++;
		}
	}

    public void PlayWaveShotSound(){
        if (m_WaveShotSound) {
            m_AudioSource.PlayOneShot(m_WaveShotSound);
        }
    }

    public void PlayWaveReloadSound()
    {
        if (m_WaveReloadSound)
        {
            m_AudioSource.PlayOneShot(m_WaveReloadSound);
        }
    }

    public void Fire(){
		if (remainingShots > 0) {
			GameObject.Instantiate (LightWaveBullet, transform.position + new Vector3(0.0f, 1.5f), transform.rotation);
            PlayWaveShotSound();
            remainingShots--;
            if (remainingShots == 0) {
				StartCoroutine ("recharge");
			}
		}
	}

	IEnumerator recharge(){
		yield return new WaitForSeconds (4.0f);
        PlayWaveReloadSound();
        remainingShots++;
	}
}
