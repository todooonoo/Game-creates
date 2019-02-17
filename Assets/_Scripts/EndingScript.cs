using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour
{
    public TextMesh text;
    public float charTime = 0.5f, endTime = 5.0f;
    public string nextScene;
    public AudioSource charSound;

    private string textStr;
    private bool showText, waitEnd;
    private float time;
    private int charCount;

    // Start is called before the first frame update
    void Start()
    {
        textStr = text.text;
        text.text = string.Empty;
    }

    public void ShowText()
    {
        showText = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(waitEnd)
        {
            time += Time.deltaTime;
            if(time >= endTime)
            {
                waitEnd = false;
                LoadingScreen.Instance.LoadScene(nextScene);
            }
            return;
        }

        if(showText)
        {
            time += Time.deltaTime;

            if(time > charTime)
            {
                text.text += textStr[charCount];
                charCount++;
                charSound.Play();

                if (charCount >= textStr.Length)
                {
                    showText = false;
                    waitEnd = true;
                }
                time = 0;
            }
        }
    }
}
