using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Ending : MonoBehaviour
{
    public TextMeshProUGUI text;
    private float time;
    [SerializeField] private float timer;
    public GameObject gameobject;
    public bool c;
    private bool isOnce = true;
    public GameObject image_object;
    public GameObject APxL;
    public AudioClip sound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        c = gameobject.GetComponent<Trigger>().trigger;
        audioSource = GetComponent<AudioSource>();

        image_object.SetActive(false);
        APxL.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        c = gameobject.GetComponent<Trigger>().trigger;

        //Debug.Log(time);

        if (c == true)
        {
            if (isOnce)
            {
                time = 0.0f;
                isOnce = false;
            }
            EndingStart();
        }
    }

    void EndingStart()
    {
        audioSource.PlayOneShot(sound);
        image_object.SetActive(true);
        if (time >= timer / 4)
        {
            text.text = "team_chuka";
            image_object.SetActive(false);
        }
        if (time >= timer / 4 * 2)
        {
            text.text = "";
            APxL.SetActive(true);
        }
        if (time >= timer / 4 * 3)
        {
            text.text = "The End";
            APxL.SetActive(false);
        }
        if (time >= timer)
        {
            SceneManager.LoadScene("Launcher");
        }
    }
}