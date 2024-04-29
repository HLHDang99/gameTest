using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioClip shortSound1;
    [SerializeField] AudioClip shortSound2;
    [SerializeField] bool stop;

    AudioSource shortSound1Source;
    AudioSource shortSound2Source;

    // Use this for initialization
    void Start ()
    {
        // Start playing the background music
        backgroundMusic.Play ();

        // Create audio sources for short sounds
        shortSound1Source = gameObject.AddComponent<AudioSource> ();
        shortSound1Source.clip = shortSound1;

        shortSound2Source = gameObject.AddComponent<AudioSource> ();
        shortSound2Source.clip = shortSound2;
    }
    void Update(){
        if(!(shortSound1Source.isPlaying || shortSound2Source.isPlaying) && !stop){
            if((int)Random.Range(0, 4) != 2){
                PlayShortSound1();
            }
            if((int)Random.Range(0, 4) != 3){
                PlayShortSound2();
            }
        }
    }
    // Play the first short sound
    void PlayShortSound1 ()
    {
        shortSound1Source.volume = Random.Range(0, 0.6f);
        shortSound1Source.Play ();
    }

    // Play the second short sound
    void PlayShortSound2 ()
    {
        shortSound2Source.volume = Random.Range(0, 0.6f);
        shortSound2Source.Play ();
    }

    // Toggle sound effects on/off
    public void ToggleSoundEffects (bool isOn)
    {
        shortSound1Source.mute = !isOn;
        shortSound2Source.mute = !isOn;
    }

    // Stop playing all sounds
    public void StopAllSounds ()
    {
        backgroundMusic.Stop ();
        shortSound1Source.Stop ();
        shortSound2Source.Stop ();
    }
}
