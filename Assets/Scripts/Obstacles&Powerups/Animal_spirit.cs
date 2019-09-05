//hgjh
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Animal_spirit : Powerups
{

    bool active;
    int pathcnt1 = 0;
    int pathcnt2 = 0;
    // GameObject Path = new GameObject();
    Transform[] path1 = new Transform[4];
    Transform[] path2 = new Transform[4];
    int timer = 0;
    // Use this for initialization
    [SerializeField] private AudioClip m_CollideSound;
    [SerializeField] private AudioClip m_RunningSound;
    [SerializeField] private AudioClip m_DestroySound;
    private AudioSource m_AudioSource;
    void Start()
    {
        active = false;
       // Path = GameObject.FindGameObjectsWithTag("Path1")[0];
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Path1")[0].transform.childCount; i++)
        {
            path1[i] = GameObject.FindGameObjectsWithTag("Path1")[0].transform.GetChild(i).transform;
           
           
        }
        for (int i=0;i< GameObject.FindGameObjectsWithTag("Path2")[0].transform.childCount; i++)
        {
            path2[i] = GameObject.FindGameObjectsWithTag("Path2")[0].transform.GetChild(i).transform;
        }
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            float speed = 0.1f;
            if (this.gameObject.tag == "AnimalSpirit1")
            {
                
                Vector3 dir = Vector3.Normalize(path1[pathcnt1].position - this.transform.position);

                if (Math.Abs(path1[pathcnt1].position.x - this.transform.position.x) > 0.1 || Math.Abs(path1[pathcnt1].position.z - this.transform.position.z) > 0.1)
                {
					this.transform.LookAt(path1[pathcnt1]);
					this.transform.Rotate (new Vector3(0.0f, -90.0f, 0.0f));
                    this.transform.position += dir * speed;
                }
                else
                {

                    if (pathcnt1 < GameObject.FindGameObjectsWithTag("Path1")[0].transform.childCount - 1)
                        pathcnt1++;

                }
            }
            else
            {
                Vector3 dir = Vector3.Normalize(path2[pathcnt2].position - this.transform.position);
                if (Math.Abs(path2[pathcnt2].position.x - this.transform.position.x) > 0.1 || Math.Abs(path2[pathcnt2].position.z - this.transform.position.z) > 0.1)
                {
					this.transform.LookAt(path2[pathcnt2]);
					this.transform.Rotate (new Vector3(0.0f, -90.0f, 0.0f));
                    this.transform.position += dir * speed;
                    
                }

                else
                {

                    if (pathcnt2 < GameObject.FindGameObjectsWithTag("Path2")[0].transform.childCount - 1)
                        pathcnt2++;

                }
            }
        }
    }

    public void PlayDestroySound()
    {
        if (m_DestroySound)
        {
            m_AudioSource.PlayOneShot(m_DestroySound);
        }
    }

    public void PlayRunningSound()
    {
        if (m_RunningSound)
        {
            m_AudioSource.clip = m_RunningSound;
            m_AudioSource.Play();
        }
    }

    public void PlayCollideSound()
    {
        if (m_CollideSound)
        {
            m_AudioSource.PlayOneShot(m_CollideSound);
        }
    }

    protected override void Awake()
    {

    }

    protected override void OnTriggerEnter(Collider Collide)
    {
        
        if (Collide.gameObject.tag == "Player")
        {
            active = true;
        }

    }

	void OnCollisionEnter(Collision other){
		if (other.collider.tag == "Player") {
			active = true;
		}
	}

}