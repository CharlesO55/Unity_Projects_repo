using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                         DO NOT EDIT THIS SCRIPT                         *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class Transluscify : MonoBehaviour {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                             GENERAL METHODS                             *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float _alpha = 1.0f;
    public float Alpha {
        get { return this._alpha; }
        set { this._alpha = value; }
    }

    [SerializeField]
    private MeshRenderer _renderer;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
     *                            LIFECYCLE METHODS                            *
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    private void Update() {
        if(this._renderer.materials[0].color.a != this._alpha) {
            foreach(Material material in this._renderer.materials) {
                material.color = new Color(1.0f, 1.0f, 1.0f, this._alpha);
            }
        }
    }
}
