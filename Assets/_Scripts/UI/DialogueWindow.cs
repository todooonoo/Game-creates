using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{
    public float animationTime = 0.5f;
    public Text text;
    public InputAction[] skipActions;
    private List<InputPair> skipInputPairs;

    private Vector3 delta, start, end;
    private bool initComplete;

    public void InitUI()
    {
        if (initComplete)
            return;
        initComplete = true;

        skipInputPairs = new List<InputPair>();
        for (int i = 0; i < skipActions.Length; i++)
        {
            InputPair pair = InputHandler.Instance.GetInput(skipActions[i]);

            if(pair != null)
                skipInputPairs.Add(pair);
        }

        delta = Vector3.up * GetComponent<RectTransform>().sizeDelta.y;
        transform.localPosition -= delta;
        start = transform.localPosition;
        end = start + delta;
        gameObject.SetActive(false);
    }

    public void SetDialogue(string[] lines, bool reloadScene = false)
    {
        gameObject.SetActive(true);
        StartCoroutine(PlayDialogue(lines, reloadScene));
    }

    private IEnumerator PlayDialogue(string[] lines, bool reloadScene)
    {
        GameManager.Instance.state = GameState.Event;
        GameManager.Instance.player.Stop();
        yield return ShowWindow();
        
        // Show each lines
        for(int i = 0; i < lines.Length; i++)
        {
            yield return ShowLine(lines[i]);
        }

        yield return HideWindow();
        ForceHide();

        if (reloadScene)
            LoadingScreen.Instance.ReloadScene();
    }

    public void ForceHide()
    {
        GameManager.Instance.state = GameState.Idle;
        GameManager.Instance.player.playerState = PlayerState.Idle;
        gameObject.SetActive(false);
    }

    private IEnumerator ShowWindow()
    {
        text.text = string.Empty;
        transform.localPosition = start;

        // Animate
        float t = 0f;
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = transform.localPosition + delta;

        while (t < animationTime)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, t / animationTime);
            yield return null;
        }
    }

    private IEnumerator HideWindow()
    {
        // Animate
        float t = 0f;
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = transform.localPosition - delta;

        while (t < animationTime)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, t / animationTime);
            yield return null;
        }
    }

    private IEnumerator ShowLine(string line)
    {
        string temp = text.text = string.Empty;
        int index = 0;

        while(index < line.Length)
        {
            temp += line[index];
            text.text = temp;
            index++;

            if(SkipLine())
            {
                text.text = line;
                index = line.Length;
            }
            yield return null;
        }

        while (!SkipLine())
            yield return null;
        yield return null;
    }

    private bool SkipLine()
    {
        for (int i = 0; i < skipInputPairs.Count; i++)
        {
            if (skipInputPairs[i].GetAxisDown)
            {
                return true;
            }
        }
        return false;
    }
}
