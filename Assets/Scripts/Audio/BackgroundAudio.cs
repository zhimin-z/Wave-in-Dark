using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundAudio : MonoBehaviour {

	GameObject player;
	Vector3 playerPosition;
	float playerDist;
    public AudioMixerSnapshot BGM1;
    public AudioMixerSnapshot BGM2;

    public float bpm = 128;


    private float m_TransitionIn;
    private float m_TransitionOut;
    private float m_QuarterNote;


    // Use this for initialization
    void Start () {
		player = GameObject.FindWithTag("Player");

        m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote * 8;
        m_TransitionOut = m_QuarterNote * 8;
    }
	
	// Update is called once per frame
	void Update () {
		SeekerAI[] seekerObjects = FindObjectsOfType(typeof(SeekerAI)) as SeekerAI[];
		playerPosition = player.transform.position;
        bool isSeekerInProximity = false;
		foreach (SeekerAI seeker in seekerObjects)
		{
			playerDist = (playerPosition - seeker.transform.position).magnitude;
            if (playerDist<15.0f)
            {
                isSeekerInProximity |= true;
                //Debug.Log(playerDist);
            }
            else
            {
                isSeekerInProximity |= false;
            }
		}
        if(isSeekerInProximity)
        {
            BGM2.TransitionTo(m_TransitionIn);
        }
        else
        {
            BGM1.TransitionTo(m_TransitionOut);
        }
	}
}
