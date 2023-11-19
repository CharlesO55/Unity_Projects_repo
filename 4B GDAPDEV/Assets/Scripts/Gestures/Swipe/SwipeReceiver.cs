using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeReceiver : MonoBehaviour
{
    public void OnSwipe(object sender, SwipeEventArgs args)
    {
        Debug.Log("SwipeReceier received");
    }

    // Start is called before the first frame update
    void Start()
    {
        GestureManager.Instance.OnSwipe += this.OnSwipe;
    }

    // Update is called once per frame
    void OnDisable()
    {
        GestureManager.Instance.OnSwipe -= this.OnSwipe;
    }
}
