using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragEventArgs : EventArgs {
    private Touch _trackedFinger;
    public Touch TrackedFinger {
        get { return this._trackedFinger; }
    }

    public DragEventArgs(Touch trackedFinger) {
        this._trackedFinger = trackedFinger;
    }
}
