using UnityEngine;

public class BallController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private Rigidbody rb;
    private float yPos;
    private bool hasStarted;
    private float direction; // +1 From Bottom to Top; -1 From Top to Bottom
    private bool dirToggelt;

    public float xPush = 2.0f;
    public float zPush = 15.0f;
    public float speed = 0.10f;
    public float randomFactor = 0.2f;


    private void Start ()
    {
        rb = GetComponent<Rigidbody> ();
        yPos = GetComponent<Transform> ().position.y;
        hasStarted = true;
        direction = 1.0f;
        dirToggelt = false;
    }

    private void FixedUpdate()
    {
        if (hasStarted)
        {
            rb.velocity = new Vector3 (xPush*direction, yPos, zPush);
            hasStarted = false;
        }
    }

    public void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.name == "WallPingTop" || collision.gameObject.name == "WallPongBottom")
        {
            ToogleDirection();
        }
        else if (collision.gameObject.name == "PingPlayer")
        {
            ToogleDirection();
        }
    }

    public void ToogleDirection()
    {
        Vector3 velocityTweak = new Vector3
           (Random.Range(0.0f, randomFactor),
            yPos,
            Random.Range(0.0f, randomFactor));

        direction = direction * -1.0f;
        rb.velocity = new Vector3(xPush * direction, yPos, zPush)+ velocityTweak;
        rb.velocity += velocityTweak;
    }
}
