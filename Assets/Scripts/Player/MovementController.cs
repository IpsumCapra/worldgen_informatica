using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float speed = 0.5f;
    public float jForce = 2500f;
    public Vector2 cameralimiter;
    public float yaw = 0.0f;
    public float pitch = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        Vector3 dir = new Vector3(0, -1);
        float distance = 2f;

        if (Physics.Raycast(transform.position, dir, out hit, distance))
            return true;
        else
            return false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject cam = GameObject.Find("Player Camera");
        Rigidbody rbody = GetComponent<Rigidbody>();

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, cameralimiter.x, cameralimiter.y);

        transform.eulerAngles = new Vector3(0, yaw);
        cam.transform.localEulerAngles = new Vector3(pitch, 0);

        transform.position += transform.forward * Input.GetAxis("Vertical") * speed;
        transform.position += transform.right * Input.GetAxis("Horizontal") * speed;
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            
            rbody.AddForce(new Vector3(0, jForce, 0));
        }

    }
}
