using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class CamHandler : MonoBehaviour
{
    public float rotationSpeed = 2f;
    public float speedH = 2f;
    public float speedV = 2f;
    public float yaw = 0.0f;
    public float pitch = 0.0f;
    public Vector2 cameralimiter;
    public bool autoTurn = true;
    public bool generate = true;

    public Canvas FadeCanvas;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BuildRandomWorld", 0.5f, 4f);
        StartCoroutine(GameObject.Find("Fader").GetComponent<Fader>().FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        Animator animator = GameObject.Find("Main Camera").GetComponent<Animator>();
        if (Input.GetKey(KeyCode.Escape) && animator.GetBool("Options"))
            animator.SetBool("Options", false);

        if (GetComponent<MapGenerator>().autoUpdate)
            GetComponent<MapGenerator>().GenerateMap();

        yaw -= speedH * Input.GetAxis("Horizontal");

        if (autoTurn && Input.GetAxis("Horizontal") == 0)
            yaw -= rotationSpeed * Time.deltaTime;

        pitch += speedV * Input.GetAxis("Vertical");
        pitch = Mathf.Clamp(pitch, cameralimiter.x, cameralimiter.y);

        transform.localEulerAngles = new Vector3(pitch, yaw);        
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
        Application.Quit();
    }

    void BuildRandomWorld()
    {
        MapGenerator generator = GetComponent<MapGenerator>();
        generator.seed = Random.Range(-1000000, 1000000);
        generator.GenerateMap();
        GameObject.Find("Terrain Manipulator").GetComponent<ValueUpdater>().UpdateSeed();
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

    public void SwitchToGame()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        GetComponent<WorldGenerator>().DestroyBlocks();
        StartCoroutine(FadeCanvas.GetComponent<Fader>().FadeIn());
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(2);
    }


}

