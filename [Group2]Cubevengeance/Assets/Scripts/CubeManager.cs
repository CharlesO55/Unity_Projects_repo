using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeManager : MonoBehaviour {
    /* * * * * * [TODO][4] * * * * * *
     * ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ */

    private float _forceAmount = 500;

    public void Slice()
    {
        this.GetComponent<MeshRenderer>().enabled = false;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);

            Vector3 forceVec = (this.transform.position - child.transform.position).normalized;
            child.GetComponent<Rigidbody>().AddForce(forceVec * _forceAmount, ForceMode.Impulse);
        }
    }
}
