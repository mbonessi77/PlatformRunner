using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public int rad;
    public int speed;
    public GameObject player;
	// Use this for initialization
	void Start ()
    {
        rad = 5;
        speed = 5;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(player.transform.position);
        if(Vector3.Distance(player.transform.position, this.transform.position) < rad)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
	}
}
