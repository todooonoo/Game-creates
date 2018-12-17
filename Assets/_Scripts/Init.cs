using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    [SerializeField] private string firstScene = "MainMenu";

    // Use this for initialization
    void Start()
    {
        LoadingScreen.Instance.LoadScene(firstScene);
    }
}
