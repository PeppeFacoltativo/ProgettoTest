using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    public Transform player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

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
    }
}
