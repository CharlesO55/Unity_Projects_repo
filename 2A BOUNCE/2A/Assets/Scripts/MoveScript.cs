using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float speed = 10f;
    public bool bRotating = false;

    private float horizontalInput;
    private float verticalInput;
    private float fTimePassed = 0f;


    private void ReadInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void MovePaddle()
    {
        Vector3 movement = new(horizontalInput, verticalInput, 0f);
        transform.Translate(speed * Time.deltaTime * movement, Space.World);
    }


    private void RotatePaddle()
    {
        if (bRotating)
        {
            this.transform.Rotate(0f, 0f, fTimePassed * 5 * Time.deltaTime);
        }
    }

    void Update()
    {
        ReadInput();
        fTimePassed += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        MovePaddle();
        RotatePaddle();
    }
}
