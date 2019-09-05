using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerups : MonoBehaviour {

    protected abstract void Awake();

    protected abstract void OnTriggerEnter(Collider Collide);

}
