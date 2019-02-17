using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used in lab entrance
public class DialogueStart : MonoBehaviour
{
    private DraggableTalk talkEvent;

    private IEnumerator Start()
    {
        if (ProgressManager.Instance.doorsLocked)
            yield break;

        talkEvent = GetComponent<DraggableTalk>();
        yield return null;
        BeginTutorial();
    }

    public void BeginTutorial()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        // Init settings
        GameManager.Instance.state = GameState.Event;
        talkEvent.OnDrag(GameManager.Instance.player.transform, false);
        ProgressManager.Instance.doorsLocked = true;
    }

}
