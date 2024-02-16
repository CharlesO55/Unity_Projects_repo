using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float zDistance = 25f;
    [SerializeField] private GameObject BallTarget;

    private Vector3 velRet = Vector3.zero;

    private void Start()
    {
        this.transform.position = BallTarget.transform.position + new Vector3(-100f, 0f, 0f);
    }
    void LateUpdate()
    {
        Vector3 newPos = BallTarget.transform.position + new Vector3(0f,5f, -zDistance * Mathf.Max(1f, BallTarget.transform.position.y/6)); 
        this.transform.position = Vector3.SmoothDamp(this.transform.position, newPos, ref velRet, .8f);

        this.transform.LookAt(BallTarget.transform.position);
    }
}