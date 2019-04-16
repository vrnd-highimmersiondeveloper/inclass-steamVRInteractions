using UnityEngine;

public class BallController : MonoBehaviour
{
    private enum Direction { InDirection, AgainstDirection};

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private Rigidbody rb;
    private float yPos;
    private bool hasStarted;
    private float direction; // +1 From Bottom to Top; -1 From Top to Bottom
    private bool dirToggelt;

    public float xPush = 10.0f;
    public float zPush = 15.0f;
    public float speed = 0.10f;
    public float randomFactor = 0.5f;


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
            rb.velocity = new Vector3 (xPush*direction*speed, yPos, zPush);
            hasStarted = false;
        }
    }

    public void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if(collision.gameObject.name == "WallPingTop" || collision.gameObject.name == "WallPongBottom")
            {
                BounceOf(Direction.AgainstDirection);
            }
            else
            {
                BounceOf(Direction.InDirection);
            }
            
        }
        else if (collision.gameObject.name == "PingPlayer" || collision.gameObject.name == "PongEnemy")
        {
            BounceOf(Direction.AgainstDirection);
        }
    }

    private void BounceOf(Direction direction)
    {
        Debug.Log("Before dirchange: " + this.direction);
        if (direction == Direction.AgainstDirection)
        {
            ToogleDirection(); 
            Debug.Log("against Direction");
            Debug.Log("After dirchange: " + this.direction);
        }

        AddTweak();
        Debug.Log("Added Tweak: " + this.direction);
    }

    public void ToogleDirection()
    {
        direction = direction * -1.0f;
    }

    public void AddTweak()
    {
        Vector3 velocityTweak = new Vector3
                                   (Random.Range(0.0f, randomFactor),
                                    0.0f,
                                    Random.Range(0.0f, randomFactor));
        rb.velocity = new Vector3(xPush * direction, yPos, zPush ) + velocityTweak;
        //rb.velocity += velocityTweak;
        Debug.Log("add tewak");
    }
}
