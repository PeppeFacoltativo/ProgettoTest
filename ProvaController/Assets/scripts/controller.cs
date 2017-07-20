using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour {

	// Use this for initialization

	void Start () {
        Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
        //Player
        rb = GetComponent<Rigidbody>();
    }

    [SerializeField]
    float speed = 0.1F;

    [SerializeField]
    float jumpForce = 15f;

    [SerializeField]
    float trampForce = 80f;

    [SerializeField]
    float dash = 5F;

    [SerializeField]
    float dashCooldown = 1.5f;

    float dashmultiplier = 1.5f;
    bool grounded = true;
    bool dashing = false;
    float dashTime = 0;
    float startTime = 0;
    float timeStamp = 0;

    Rigidbody rb;

    // Update is called once per frame
    void FixedUpdate () {

        //Get Horizontal Rotation from mouse
        float Hrot = 0;
        Hrot = Input.GetAxisRaw("Mouse X")/2;

        if (timeStamp <= Time.time)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                startTime = Time.time;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                dashTime = (Time.time - startTime)/4;
                timeStamp = Time.time + dashCooldown + dashTime;
                //Maximum dashTime = 0.5 sec
                if (dashTime > 0.5)
                {
                    dashTime = 0.5f;
                }
                dashing = true;
            }
        }

        if (dashing)
        {
            dashMove();
        }
        else
        {
            Move();
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        transform.Rotate(new Vector3(0, Hrot, 0));

    }

    public void Move()
    {
        float Vmov = 0;
        float Hmov = 0;
        Vmov = Input.GetAxis("Vertical") * speed * dashmultiplier;
        Hmov = Input.GetAxis("Horizontal") * speed;
        transform.Translate(Hmov, 0, Vmov);
    }

    public void dashMove()
    {
        if (dashTime>0)
        {
            float Vmov = 0;
            float Hmov = 0;
            Vmov = (speed + dash) * dashmultiplier;
            Hmov = Input.GetAxis("Horizontal") * speed;
            transform.Translate(Hmov, 0, Vmov);
            /*if (Vmov != 0)
            { rb.AddForce(new Vector3(Hmov, 0, Vmov), ForceMode.Impulse); }*/
            dashTime = dashTime-Time.deltaTime;
        }
        else
        {
            startTime = 0;
            dashTime = 0;
            dashing = false;
        }   
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
            jumpForce = trampForce;
        }

        if (box.tag=="ground")
        {
            grounded = true;
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

        if (box.tag == "ground")
        {
            grounded = false;
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
