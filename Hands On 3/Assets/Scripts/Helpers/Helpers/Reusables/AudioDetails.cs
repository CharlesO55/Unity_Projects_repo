using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDetails : MonoBehaviour {
    [SerializeField]
    private EAudioId _id;
    public EAudioId Id {
        get { return this._id; }
    }

    [SerializeField]
    private AudioSource _source;
    public AudioSource Source {
        get { return this._source; }
    }
}
