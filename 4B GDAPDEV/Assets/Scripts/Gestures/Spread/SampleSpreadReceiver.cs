using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSpreadReceiver : MonoBehaviour , ISpreadable
{
    [SerializeField] float _resizeSpeed = 30f;

    public void OnSpreadInterface(SpreadEventArgs args)
    {
        float scale = args.DistanceDelta / Screen.dpi;
        scale *= Time.deltaTime * _resizeSpeed;

        this.transform.localScale += new Vector3(scale, scale, scale);
    }
}
