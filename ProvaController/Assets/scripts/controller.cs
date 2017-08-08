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

    public Transform camera;

    [SerializeField]
    float speed;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float trampForce;

    [SerializeField]
    float dash;

    [SerializeField]
    float rotationSpeed;

    [SerializeField]
    float dashCooldown;

    float dashmultiplier = 1.5f;
    public bool grounded = true;
    bool dashing = false;
    float dashTime = 0;
    float startTime = 0;
    float timeStamp = 0;

    Rigidbody rb;

    // Update is called once per frame
    void FixedUpdate () {

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

    }

    public void Move()
    {
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) moveDirection += camera.forward;
        if (Input.GetKey(KeyCode.S)) moveDirection += -camera.forward;
        if (Input.GetKey(KeyCode.A)) moveDirection += -camera.right;
        if (Input.GetKey(KeyCode.D)) moveDirection += camera.right;

        moveDirection.y = 0f;

        rb.AddForce(moveDirection*speed);
        if (moveDirection != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);
}

    Vector3 dashVector = Vector3.zero;

    public void dashMove()
    {
        if (dashTime>0)
        {
            Vector3 moveDirection = Vector3.zero;
            
            if (Input.GetKey(KeyCode.W)) moveDirection = camera.forward;
            if (Input.GetKey(KeyCode.S)) moveDirection = -camera.forward;
            if (Input.GetKey(KeyCode.A)) moveDirection = -camera.right;
            if (Input.GetKey(KeyCode.D)) moveDirection = camera.right;

            moveDirection.y = 0f;
            dashVector = new Vector3(moveDirection.x*dash,0, moveDirection.z * dash);

            rb.AddForce(dashVector, ForceMode.VelocityChange);


            if (moveDirection != Vector3.zero)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);

            dashTime = dashTime-Time.deltaTime;
        }
        else
        {
			rb.AddForce(-dashVector,ForceMode.VelocityChange);
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
            jumpForce = 3;
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
