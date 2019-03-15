using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    public Vector2 superblock;
    bool flashlight;

    public AudioClip[] clips;

    public AudioClip[] steps;
    Vector3 oldStep;

    public Transform block;
    GameObject cursor;

    public Transform sblock;
    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("Cursor Cube");
        Cursor.lockState = CursorLockMode.Locked;
        UpdateSuperblocks();
        oldStep = transform.position;
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
        Camera cam = Camera.main;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        int cubeLayerIndex = LayerMask.NameToLayer("Blocks");
        int layerMask = (1 << cubeLayerIndex);

        if (Physics.Raycast(ray, out hit, 5.5f, layerMask))
        {
            Transform objectHit = hit.transform;
            if (!cursor.activeSelf)
                cursor.SetActive(true);
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (objectHit.parent != null)
                    objectHit.parent.GetComponent<Superblock>().blocks.Remove(objectHit.transform);
                Destroy(objectHit.gameObject);
                AudioSource[] sources;
                sources = GetComponents<AudioSource>();
                sources[0].clip = clips[Random.Range(0, clips.Length)];
                sources[0].Play();
            }

            Vector3 localPoint = hit.transform.InverseTransformPoint(hit.point);
            Vector3 localDir = localPoint.normalized;

            float upDot = Vector3.Dot(localDir, Vector3.up);
            float fwdDot = Vector3.Dot(localDir, Vector3.forward);
            float rightDot = Vector3.Dot(localDir, Vector3.right);

            float upPower = Mathf.Abs(upDot);
            float fwdPower = Mathf.Abs(fwdDot);
            float rightPower = Mathf.Abs(rightDot);

            Vector3 pos;

            if (upPower > fwdPower && upPower > rightPower)
                pos = new Vector3(0, 1, 0);
            else if (fwdPower > upPower && fwdPower > rightPower)
            {
                if (fwdDot > 0)
                    pos = new Vector3(0, 0, 1);
                else
                    pos = new Vector3(0, 0, -1);
            }
            else
            {
                if (rightDot > 0)
                    pos = new Vector3(1, 0, 0);
                else
                    pos = new Vector3(-1, 0, 0);
            }
            cursor.transform.position = hit.transform.position + pos;

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Instantiate(block, hit.transform.position + pos, Quaternion.identity);
                AudioSource[] sources;
                sources = GetComponents<AudioSource>();
                sources[0].clip = clips[Random.Range(0, clips.Length)];
                sources[0].Play();
            }


        }
        else
            cursor.SetActive(false);
        if (Mathf.Round(transform.position.x / 10) != superblock.x || Mathf.Round(transform.position.z / 10) != superblock.y)
        {
            UpdateSuperblocks();
            superblock = new Vector2(Mathf.Round(transform.position.x / 10), Mathf.Round(transform.position.z / 10));
        }


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

        if (Input.GetKeyUp(KeyCode.F))
        {
            flashlight = !flashlight;
            GameObject.Find("Flashlight").GetComponent<Light>().enabled = flashlight;
        }

        if (Input.GetKey(KeyCode.Escape))
            BackToMenu();

        if(Vector3.Distance(new Vector3(oldStep.x, 0, oldStep.z), new Vector3(transform.position.x, 0, transform.position.z)) > 3)     
        {
            oldStep = transform.position;
            if(IsGrounded())
            {
                AudioSource[] sources;
                sources = GetComponents<AudioSource>();
                sources[1].clip = steps[Random.Range(0, steps.Length)];
                sources[1].Play();
            }
        }

    }

    void UpdateSuperblocks()
    {
        for(int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                int cubeLayerIndex = LayerMask.NameToLayer("Superblocks");
                int layerMask = (1 << cubeLayerIndex);

                Collider[] colliders;
                
                colliders = Physics.OverlapSphere(new Vector3((x + superblock.x - 4) * 10 + 5, 0, (y + superblock.y - 4) * 10 + 5), 1f, layerMask);
                if (colliders.Length == 0)
                {
                    Transform go;
                    go=Instantiate(sblock, new Vector3((x + superblock.x - 4) * 10, 0, (y + superblock.y - 4) * 10), Quaternion.identity);
                    colliders = new Collider[] { go.transform.GetComponent<Collider>() };
                }

                if (x > 1 && x < 6 && y > 1 && y < 7)
                    colliders[0].GetComponent<Superblock>().Check(true);
                else
                    colliders[0].GetComponent<Superblock>().Check(false);
            }
        }
    }

    public void BackToMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(1);
    }
}
