using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager myInstance = null;
    
    public AudioClip bullet;
    public AudioClip shoot;
    public AudioClip explosion;

    private AudioSource soundEffectAudio;

    // Start is called before the first frame update
    void Start()
    {

        if (myInstance == null)
        {
            myInstance = this;
        } else if (myInstance != this)
        {
            Destroy(gameObject);
        }

        AudioSource theSource = GetComponent<AudioSource>();
        soundEffectAudio = theSource;
                
    }
    
    public void playOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);

    }
}
