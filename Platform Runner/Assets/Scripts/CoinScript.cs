using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int speed;
	// Use this for initialization
	void Start ()
    {
        speed = 50;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.right * speed * Time.deltaTime);
	}
}
