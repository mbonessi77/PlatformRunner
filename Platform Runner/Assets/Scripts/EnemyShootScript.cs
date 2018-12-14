using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    float timer;
    public GameObject bullet;

	// Use this for initialization
	void Start ()
    {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            Instantiate(bullet, transform.position, transform.rotation);
        }
	}
}
