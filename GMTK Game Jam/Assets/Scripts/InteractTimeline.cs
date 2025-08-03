using UnityEngine;
using UnityEngine.Playables;

public class InteractTimeline : MonoBehaviour, IInteractable
{
    public PlayableDirector playableDirector;

    public void Interact()
    {
        if (playableDirector != null)
        {
            playableDirector.Play();
        }
    }
}
