using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureSpawnReceiver : MonoBehaviour , ITapable , ISwipeable , IDraggable
{
    [SerializeField]
    private float _speed = 10.0f;
    private Vector3 _tagetPosition = Vector3.zero;


    private Vector3 moveDir;

    public void OnTap(TapEventArgs _tapEventArgs)
    {
        Debug.Log(this.gameObject.name + " was tapped");

        Destroy(this.gameObject);
    }

    public void OnSwipe(SwipeEventArgs _args)
    {
        Debug.Log(this.gameObject.name + " was swiped");

        switch (_args.MoveType)
        {
            case 0:
                this.MovePerpendicular(_args);
                break;
            case 1:
                this.MoveDiagonal(_args);
                break;
            default: 
                break;
        }
    }

    public void OnDragInterface(DragEventArgs _args)
    {
        Debug.Log(this.gameObject.name + " was dragged");

        if (_args.ObjHit == this.gameObject)
        {
            Vector2 fingerPos = _args.TrackedFinger.position;
            Ray ray = Camera.main.ScreenPointToRay(fingerPos);
            Vector3 worldPos = ray.GetPoint(10);

            this._tagetPosition = worldPos;
            this.transform.position = worldPos;
        }
    }

    private void MovePerpendicular(SwipeEventArgs args) {
        Vector3 vecDir;

        switch (args.Direction)
        {
            case ESwipeDirection.RIGHT:
                vecDir = Camera.main.transform.rotation * Vector3.right;
                break;
            case ESwipeDirection.LEFT:
                vecDir = Camera.main.transform.rotation * Vector3.left;
                break;
            case ESwipeDirection.UP:
                vecDir = Camera.main.transform.rotation * Vector3.up;
                break;
            case ESwipeDirection.DOWN:
                vecDir = Camera.main.transform.rotation * Vector3.down;
                break;
            default:
                vecDir = Vector3.zero;
                break;
        }

        this._tagetPosition = this.transform.position + (vecDir * 5f);
    }
    private void MoveDiagonal(SwipeEventArgs args) {
        Vector3 target = Camera.main.transform.rotation * args.RawDirection.normalized;
        this._tagetPosition = transform.position + (target * 5f);
    }



    private void OnEnable()
    {
        this._tagetPosition = this.transform.position;
    }

    private void Update()
    {
        if(this.transform.position  != _tagetPosition)
        {
            this.transform.position = Vector3.MoveTowards(
                this.transform.position,
                this._tagetPosition,
                this._speed * Time.deltaTime
                );
        }
    }
}