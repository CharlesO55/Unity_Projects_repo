using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 1f;

    private float currSpeed;
    private Vector3 moveDir = Vector3.zero;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.ListenInput();
    }

    private void FixedUpdate()
    {
        this.MoveSnowman();
    }

    private void MoveSnowman()
    {
        if (currSpeed > 0)
        {
            this.transform.Translate(this.moveDir * Time.deltaTime * currSpeed);
            //Decay
            currSpeed -= 1f;
        }

    }

    private void ListenInput()
    {
        switch (Input.inputString)
        {
            case "W":
            case "w":
                moveDir = Vector3.forward;
                currSpeed = this.moveSpeed;
                break;
            case "S":
            case "s":
                moveDir = Vector3.back;
                currSpeed = this.moveSpeed;
                break;
            case "A":
            case "a":
                moveDir = Vector3.left;
                currSpeed = this.moveSpeed;
                break;
            case "D":
            case "d":
                moveDir = Vector3.right;
                currSpeed = this.moveSpeed;
                break;
            default:
                break;
        }
    }
}
