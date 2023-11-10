using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeReceiver : MonoBehaviour {
    //SWIPE RECEIVER FOR PLAYING AUDIO AND INCREASING SCORE
    //[TO DO] : SET THE CUBE PREFAB TO A new "CubeTarget" Tag

    private int _scoreLog = 0;


    private GameObject TryShootRay()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity);
        
        if(hit.collider == null)
        {
            return null;
        }
        return hit.collider.gameObject;
    }

    private void OnSwipe(object sender, SwipeEventArgs swipeEventArgs) {
        Debug.Log("[SwipeReceiver] : General swipe detected.");


        GameObject hitObj = this.TryShootRay();

        if(hitObj != null && hitObj.CompareTag("CubeTarget"))
        {
            if(hitObj.TryGetComponent<CubeManager>(out CubeManager script))
            {
                _scoreLog++;
                GUIManager.Instance.SetScore(_scoreLog);
                SFXManager.Instance.PlaySoundFX(true);
                script.Slice();
            }   
        }
        else
        {
            SFXManager.Instance.PlaySoundFX(false);
        }

        /* * * * * * [TODO][4] * * * * * *
         * ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ */
        /*if (swipeEventArgs.HitObject == null)
        {
            SFXManager.Instance.PlaySoundFX(false);
        }
        else if (swipeEventArgs.HitObject.CompareTag("CubeTarget"))
        {
            _scoreLog++;
            SFXManager.Instance.PlaySoundFX(true);
            GUIManager.Instance.SetScore(_scoreLog);
        }*/
    }

    private void Start() {
        GestureManager.Instance.OnSwipe += this.OnSwipe;
    }

    private void OnDisable() {
        GestureManager.Instance.OnSwipe -= this.OnSwipe;
    }
}
