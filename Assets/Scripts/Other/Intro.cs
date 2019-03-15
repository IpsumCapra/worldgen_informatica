using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour {
    public float max = 10;
    public float min = 1;
    private Vector3 NewScale;
    private bool SwitchScene = false;
    static float t = 0.0f;

	// Use this for initialization
	void Start () {
        AudioSource IntroAudio = GetComponent<AudioSource>();
        IntroAudio.Play();
        StartCoroutine(SceneWait());
        StartCoroutine(Blacken());
    }
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(Mathf.Lerp(min, max, t), Mathf.Lerp(min, max, t), 0);
        t += 0.01f * Time.deltaTime;
        if (SwitchScene == true || Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator SceneWait()
    {
        yield return new WaitForSecondsRealtime(14);
        SwitchScene = true;
    }

    IEnumerator Blacken()
    {
        yield return new WaitForSecondsRealtime(10);
        StartCoroutine(GameObject.Find("Canvas").GetComponent<Fader>().FadeIn());
    }
}
