using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UniversalAudio : MonoBehaviour
{
    public AudioClip audioClip;      // Assign any audio you want in the Inspector
    public bool playOnClick = true;  // Set to false if you want it to stop instead

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }
    }

    public void TriggerAudio()
    {
        if (audioClip == null)
        {
            Debug.LogWarning("No audio clip assigned on " + gameObject.name);
            return;
        }

        if (playOnClick)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}