using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInGround : Powerups
{
    protected override void Awake()
    {

    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerCharacter.pc.LoseLife();
            Destroy(gameObject);
            //Debug.Log("xxx");
        }
    }
}
