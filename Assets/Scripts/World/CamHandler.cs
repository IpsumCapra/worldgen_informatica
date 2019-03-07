using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamHandler : MonoBehaviour
{
    public float rotationSpeed = 2f;
    public bool autoTurn = true;
    public bool generate = true;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BuildRandomWorld", 0, 4f);        
    }

    // Update is called once per frame
    void Update()
    {
        Animator animator = GameObject.Find("Main Camera").GetComponent<Animator>();
        if (Input.GetKey(KeyCode.Escape) && animator.GetBool("Options"))
            animator.SetBool("Options", false);

        if (autoTurn)
            transform.Rotate(transform.up * (rotationSpeed * Time.deltaTime));

        


        if (GetComponent<MapGenerator>().autoUpdate)
            GetComponent<MapGenerator>().GenerateMap();

    }

    public void GoToOptions()
    {
        Animator animator = GameObject.Find("Main Camera").GetComponent<Animator>();
        if (animator.GetBool("Options"))
            animator.SetBool("Options", false);
        else
            animator.SetBool("Options", true);
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    void BuildRandomWorld()
    {
        MapGenerator generator = GetComponent<MapGenerator>();
        generator.seed = Random.Range(-1000000, 1000000);
        generator.GenerateMap();
    }

    public void SetAutoTurn(bool turn)
    {
        autoTurn = turn;
    }

    public void SetRandomWorldGeneration(bool generate)
    {
        this.generate = generate;
        if (generate)
            InvokeRepeating("BuildRandomWorld", 0, 4f);
        else
            CancelInvoke("BuildRandomWorld");
    }
        

}

