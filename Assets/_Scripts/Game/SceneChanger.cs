using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public KeyCode prevKey = KeyCode.F9, nextKey = KeyCode.F10;
    public string prevScene, nextScene;
    
    
    void Update()
    {
        if(Input.GetKeyDown(prevKey) && prevScene.Length > 0)
        {
            LoadingScreen.Instance.LoadScene(prevScene);
            TutorialManager.Instance.HideTutorial();
            TransitionScreen.Instance.dialogueWindow.ForceHide();
        } else if(Input.GetKeyDown(nextKey) && nextScene.Length > 0)
        {
            LoadingScreen.Instance.LoadScene(nextScene);
            TutorialManager.Instance.HideTutorial();
            TransitionScreen.Instance.dialogueWindow.ForceHide();
        }
    }
}
