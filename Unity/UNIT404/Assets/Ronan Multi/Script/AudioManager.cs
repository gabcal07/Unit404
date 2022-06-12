using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip BtwRound;
    public AudioClip BossSpawn;
    public AudioClip Ambiant;
    public AudioClip BossFight;
    public AudioClip Prevent;
    public AudioClip tp;
    public AudioClip button;
    public AudioClip damage;
    public AudioClip death;
    public AudioClip dash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonPress()
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(button);
    }
    public void dashing()
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(dash);
    }
}
