using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }

    float speed = 0.1F;
    float dash = 0.2F;
    int dashmultiplier = 1;
    float jumpForce = 15f;


    // Update is called once per frame
    void Update () {

        Rigidbody rb = GetComponent<Rigidbody>();

        float Vmov=0;
        float Hmov = 0;
        float jump = 0;
        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            dash = 0.2F;
        }
        else
        {
            dash = 0;
        }

        Vmov = Input.GetAxis("Vertical") * (speed+dash) * dashmultiplier;
        Hmov = Input.GetAxis("Horizontal") * (speed+dash) * dashmultiplier;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

        transform.Translate(Hmov, jump, Vmov); 

	}

    void OnTriggerEnter(Collider box)
    {
        if (box.tag == "dash")
        {
            dashmultiplier = 2;
            deactivateWalls();
        }

        if (box.tag=="tramp")
        {
            jumpForce = 80;
        }
    }

    void OnTriggerExit(Collider box)
    {
        if (box.tag == "dash")
        {
            dashmultiplier = 1;
            ActivateWalls();
        }

        if (box.tag == "tramp")
        {
            jumpForce = 15;
        }
    }

    void deactivateWalls()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("dashWall");
        foreach (GameObject go in walls)
        {
            go.GetComponent<Collider>().enabled = false;
        }
    }

    void ActivateWalls()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("dashWall");
        foreach (GameObject go in walls)
        {
            go.GetComponent<Collider>().enabled = true;
        }
    }


}
