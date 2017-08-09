using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {


    public Transform player;       //Public variable to store a reference to the player game object
    private Vector3 offset;


    [Space]
    [Header("Position")]
    public float camPosX;
    public float camPosY;
    public float camPosZ;

    [Space]
    [Header("Rotation")]
    public float camRotationX;
    public float camRotationY;
    public float camRotationZ;

    [Space]
    [Range(0f, 10f)]
    public float turnSpeed;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = new Vector3(player.position.x + camPosX, player.position.y + camPosY, player.position.z + camPosZ);
        transform.rotation = Quaternion.Euler(camRotationX, camRotationY, camRotationZ);
        transform.LookAt(player.position);
    }


    // LateUpdate is called after Update each frame
    void LateUpdate()
    {

        if (transform.position.y <= 0.5f)    //if the camera is too close of the ground, put it back to 0.5 on top of it
        {
            camPosX = transform.position.x;    // keep the X and Z position
			camPosZ = transform.position.z - player.position.z;
			transform.position = new Vector3(player.position.x + camPosX, camPosY, player.position.z + camPosZ);

            if (transform.rotation.x < 0f)
            {
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
        }
        else if (transform.position.y >= 8f) //if it is too far from the ground, put it back to 8 unit on top of it
        {
            camPosX = transform.position.x;    //we keep the X and Z position
            camPosZ = transform.position.z;
            transform.position = new Vector3(player.position.x + camPosX, 8f, player.position.z + camPosZ);
        } 

		offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offset;
		transform.position = player.position + offset;
		
		transform.LookAt(player.position);
        
    }


	//@PEPPEFACOLTATIVO SIMO PROVA A FAR COLLIDERE LA TELECAMERA CON IL TERRENO IN MODO CHE LEI NON POSSA ANDARE PIù GIù DI ESSO E AFFONDARCISI
	/*void OnTriggerEnter(Collider box){
		Debug.Log (box.tag);
		if (box.tag=="ground")
		{

		}

	}*/
}


//transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime); Per far tornare al centro la camera