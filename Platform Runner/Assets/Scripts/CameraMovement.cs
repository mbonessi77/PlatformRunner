using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector2 lookDirection;
    Vector2 smoothingVector;
    public float sensitivity;
    public float smoothing;
    GameObject player;
    // Use this for initialization
    void Start()
    {
        sensitivity = 2.5f;
        smoothing = 2f;
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<PlayerController>().isDead && Time.timeScale == 1)
        {
             Vector2 mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
             mouseDirection = Vector2.Scale(mouseDirection, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
             smoothingVector.x = Mathf.Lerp(smoothingVector.x, mouseDirection.x, 1f / smoothing);
             smoothingVector.y = Mathf.Lerp(smoothingVector.y, mouseDirection.y, 1f / smoothing);
             lookDirection += smoothingVector;
             lookDirection.y = Mathf.Clamp(lookDirection.y, -50f, 0);
             transform.localRotation = Quaternion.AngleAxis(-lookDirection.y, Vector3.right);
             player.transform.localRotation = Quaternion.AngleAxis(lookDirection.x, player.transform.up);
           }
    }
}
