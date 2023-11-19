using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanSubscriber : MonoBehaviour
{
    [SerializeField] private float _speed = 50.0f;

    void Start()
    {
        GestureManager.Instance.OnPanDelegate += OnPanFunc;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnPanDelegate -= OnPanFunc;
    }

    public void OnPanFunc(object sender, PanEventArgs args)
    {
        Vector2 pos0 = args.TrackedFingers[0].deltaPosition;
        Vector2 pos1 = args.TrackedFingers[1].deltaPosition;

        Vector2 averagePos = (pos0 + pos1) / 2 / Screen.dpi;

        

        Vector3 newPos = averagePos * this._speed * Time.deltaTime;
        this.transform.position += newPos;

    }
}
