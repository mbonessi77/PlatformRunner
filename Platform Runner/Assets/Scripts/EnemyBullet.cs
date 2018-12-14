using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    int speed;
	// Use this for initialization
	void Start ()
    {
        speed = 25;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(this.gameObject, 5f);
	}
}
