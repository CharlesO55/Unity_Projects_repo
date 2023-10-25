using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnTap : MonoBehaviour , ITapable
{
    public void OnTap(TapEventArgs tapEventArgs)
    {
        Destroy(this.gameObject);
    }

}
