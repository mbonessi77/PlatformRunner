using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public GameObject spot1;
    public GameObject spot2;

    Vector3 pos1;
    Vector3 pos2;
    int speed;

	// Use this for initialization
	void Start ()
    {
        speed = 1;
        pos1 = spot1.transform.position;
        pos2 = spot2.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed * .5f, 1));
	}
}
