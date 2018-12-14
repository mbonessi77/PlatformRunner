using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
	// Use this for initialization
	void Start ()
    {
        speed = 50;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(Camera.main.ViewportToWorldPoint(this.transform.position).y < 0)
        {
            Destroy(this.gameObject);
        }
	}
}
