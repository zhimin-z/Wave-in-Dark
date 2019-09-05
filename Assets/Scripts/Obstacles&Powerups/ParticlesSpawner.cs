using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSpawner : MonoBehaviour {

    //static ParticlesSpawner spawner;

    public List<GameObject> ParticlePrefabs;
    public float Time = 2.50f;
    public Vector3 offsetPos;
    public bool startAutomatically = true;

    bool isSpawn;

	// Use this for initialization
	void Start () {
        //spawner = this;
        if (startAutomatically)
            StartSpawning();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartSpawning()
    {
        isSpawn = true;
        StartCoroutine(SpawnParticleJob());
    }

    public void StopSpawning()
    {
        isSpawn = false;
    }

    IEnumerator SpawnParticleJob()
    {
        while(isSpawn)
        {
            yield return new WaitForSeconds(Time);
            SpawnParticle(Random.Range(0, ParticlePrefabs.Count), transform.position + offsetPos);
        }
    }
    void SpawnParticle(int index, Vector3 worldLocation)
    {
        if(index < ParticlePrefabs.Count)
        {
            GameObject.Instantiate(ParticlePrefabs[index], worldLocation, Quaternion.identity);
        }
    }
}
