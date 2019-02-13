using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamHandler : MonoBehaviour
{
    public float rotationSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("buildworld", 0, 4f);        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up * (rotationSpeed * Time.deltaTime));
    }

    public void buildworld()
    {
        if (System.Math.Abs(System.Math.Round(System.Math.Truncate(transform.rotation.y * 10))) % 2 == 0)
        {
            MapGenerator generator = GetComponent<MapGenerator>();
            generator.seed = Random.Range(-1000000, 1000000);
            generator.GenerateMap();
        }
    }
}

