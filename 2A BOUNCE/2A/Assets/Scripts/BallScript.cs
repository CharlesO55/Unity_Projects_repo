using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody rbBall;
    public float startPushForce = 50f;

    private ParticleSystem moneyParticles;


    // Start is called before the first frame update
    void Start()
    {
        rbBall.AddForce(new Vector3(startPushForce, 0, 0), ForceMode.Impulse);
        rbBall.AddTorque(Vector3.up * 10f, ForceMode.Impulse);

        moneyParticles = this.GetComponent<ParticleSystem>();
        moneyParticles.Stop();
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            moneyParticles.Play();
        }
        if (collision != null && collision.gameObject.CompareTag("Paddle"))
        {
            ScoreManager.instance.IncrementScore();

            rbBall.AddForce(collision.contacts[0].normal, ForceMode.Impulse);
            rbBall.AddTorque(collision.contacts[0].normal * 30f, ForceMode.Impulse);
        }        
    }
}