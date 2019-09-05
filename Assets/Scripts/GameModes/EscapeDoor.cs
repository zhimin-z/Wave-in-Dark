using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeDoor : MonoBehaviour {

    public bool isReached = false;

    void Update()
    {
        //print("Escape Door");
    }

    void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
            isReached = true;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            isReached = true;
        }
    }
}
