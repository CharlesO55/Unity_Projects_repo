using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanEventArgs
{
    private Touch[] _trackedFingers;
    public Touch[] TrackedFingers
    {
        get { return _trackedFingers; }
    }


    public PanEventArgs(Touch[] trackedFingers)
    {
        _trackedFingers = trackedFingers;
    }
}
