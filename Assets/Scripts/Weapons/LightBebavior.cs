using UnityEngine;
using System.Collections;

public class LightBebavior : MonoBehaviour
{
    // public
    public Light lt;
    public float growing_speed;
    public float LightIntensity;
    public float maxTimer;
	public float timer;
    public float maxRange;
    public float intensity_fading_speed;

    // private
    private float LightRange = 0.0f;
    private float duration = 0;
	//private float intensity;
    //private bool isMaxRange = false;


    // Use this for initialization
    void Start()
    {
        lt.range = LightRange;
        lt.intensity = LightIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (lt.intensity <= 0)
        {
            Destroy(gameObject);
        }

        if (LightRange <= maxRange)
        {
			LightRange += Time.deltaTime * growing_speed * maxRange;
            lt.range = LightRange;
			this.GetComponent<SphereCollider> ().radius = lt.range;
        }
        else
        {
            //isMaxRange = true;
            if(duration < maxTimer)
            {
                duration += Time.deltaTime;
            }
            else
            {
				lt.intensity -= Time.deltaTime * intensity_fading_speed;
            }
        }
    }
    
}