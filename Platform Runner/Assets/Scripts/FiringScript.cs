using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringScript : MonoBehaviour
{
    public bool firing;
    public GameObject bullet;
    bool canShoot;
    float timer;
	// Use this for initialization
	void Start ()
    {
        firing = false;
        canShoot = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if(timer >= .25f)
        {
            canShoot = true;
        }
        if(firing && canShoot)
        {
            canShoot = false;
            timer = 0;
            Instantiate(bullet, this.transform.position, this.transform.rotation);
        }
	}
}
