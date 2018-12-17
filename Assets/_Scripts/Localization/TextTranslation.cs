using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextTranslation : MonoBehaviour {

    public Language lang;
    public string translatedText;

	void Start ()
    {
        if (SaveManager.Instance.language == lang)
        {
            Text text = GetComponent<Text>();
            text.text = translatedText;
        }
	}
}
