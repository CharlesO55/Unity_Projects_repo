using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //CREATE AN EMPTY AND ATTACH THIS SINGLETON

    public static SFXManager Instance;

    private AudioSource _audioSource;
    [SerializeField] AudioClip _hitSFX;
    [SerializeField] AudioClip _missSFX;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            this._audioSource = this.AddComponent<AudioSource>();
            this._audioSource.loop = false;
            this._audioSource.playOnAwake = false;
        }
        else
            Destroy(this.gameObject);
    }


    public void PlaySoundFX(bool isHit)
    {
        /*if(_audioSource.isPlaying)
        {
            Debug.LogWarning("Audio has not finished playing");
            return;
        }*/

        if(isHit)
        {
            this._audioSource.PlayOneShot(_hitSFX);
        }
        else
        {
            this._audioSource.PlayOneShot(_missSFX);
        }
    }
}
