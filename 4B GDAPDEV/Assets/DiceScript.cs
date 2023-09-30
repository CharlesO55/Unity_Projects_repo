using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    /*private int[] _dieVal = {4 , 13 };
    private Vector3[] _angles = {Vector3.up, Vector3.down};
    */

    [SerializeField] private DieValues[] DieValuesList;


    void Update()
    {
        Vector3 dieRot = this.transform.rotation.eulerAngles;

        foreach (DieValues d in DieValuesList)
        {
            Vector3 currFacing = this.transform.rotation * d.FaceAngle;
            Debug.DrawRay(this.transform.position, this.transform.rotation * d.FaceAngle, Color.red);


            //COMPARE CURR ANGLE WITH THAT TO UP
            float angleToCam = Vector3.Dot(currFacing.normalized, Vector3.up);
            
            //IF POITNING UP , THEN IT IS THE FACE THAT CAMERA SEES
            if (angleToCam == 1)
            {
                Debug.Log("[ROLLED] : " + d.FaceValue);
            }
        }

        //Debug.DrawRay(this.transform.position, this.transform.rotation * Vector3.up, Color.red);

        /*if(Vector3.Dot(dieRot.normalized, Vector3.up) == 1)
        {
            Debug.Log("Up");
        }*/
        /*int i = 0;
        foreach (Vector3 angle in _angles)
        {
            if(Vector3.Dot(angle, dieRot.normalized) == 1)
            {
                Debug.Log(_dieVal[i]);
                break;
            }
            i++;
        }*/



    }
}
