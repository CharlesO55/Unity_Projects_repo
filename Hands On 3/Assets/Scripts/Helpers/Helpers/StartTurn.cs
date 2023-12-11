using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                  You may NOT edit this script.                  |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class StartTurn : MonoBehaviour, ITappable {

    /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     |                         GENERAL METHODS                         |
     | ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ |
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

    public void OnTap(TapEventArgs args) {
        this.StartCoroutine(BoardManager.Instance.OpenBattleground());
    }
}
