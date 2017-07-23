using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    public Transform player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    [SerializeField]
    int maxRotation = 20;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = new Vector3(player.position.x, player.position.y + 2.0f, player.position.z + 9.0f);
    }


    // LateUpdate is called after Update each frame
    void LateUpdate()
    {

        float movement = Input.GetAxis("Horizontal") * Time.deltaTime;
        if (!Mathf.Approximately(movement, 0f))
        {
            transform.RotateAround(player.position, Vector3.up, movement);
            offset = transform.position - player.position;
        }

        float Vrot = Input.GetAxisRaw("Mouse Y") * -1;
        transform.Rotate(new Vector3(Vrot, 0, 0));

        if (transform.localEulerAngles.x > maxRotation && transform.localEulerAngles.x < 180)
        {
            transform.localEulerAngles = new Vector3(20, 0, 0);
        }
        else
        {
            if (transform.localEulerAngles.x < 360-maxRotation && transform.localEulerAngles.x > 180)
            {
                transform.localEulerAngles = new Vector3(-20, 0, 0);
            }
        }
    }
}


//transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime); Per far tornare al centro la camera