using UnityEngine;

public class DragEventArgs
{
    private Touch _trackedFinger;
    private GameObject _objHit;

    public Touch TrackedFinger
    {
        get { return _trackedFinger; }
    }
    public GameObject ObjHit { 
        get { return _objHit; } 
        set { _objHit = value; }
    }

    public DragEventArgs(Touch trackedFinger, GameObject objHit = null)
    {
        _trackedFinger = trackedFinger;
        _objHit = objHit;
    }

}
