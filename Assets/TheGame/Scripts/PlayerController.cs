using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 movePaddleAction;

    Vector2 axis;
    private bool pressed = false;

    public LinearMapping linearMapping;
    public float maxMapping = 0.9f;
    public float minMapping = 0.1f;
    public float mapStep = 0.05f;
    public float sensitivity = 0.2f;

    private Rigidbody thisRb;
    private Transform thisTransform;

    private void Start()
    {
        thisRb = GetComponent<Rigidbody>();
        thisTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        var leftHoriz = movePaddleAction.GetAxis(SteamVR_Input_Sources.LeftHand);
        var rightHoriz = movePaddleAction.GetAxis(SteamVR_Input_Sources.RightHand);

        if (leftHoriz.x < -sensitivity || rightHoriz.x < -sensitivity)
        {
            //move paddle left
            if (linearMapping.value < maxMapping)
            {
                linearMapping.value += mapStep;
            }
        }
        else if (leftHoriz.x > sensitivity || rightHoriz.x > sensitivity)
        {
            if (linearMapping.value > minMapping)
            {
                linearMapping.value -= mapStep;
            }
        }

        //thisRb.velocity = new Vector3(0,0,linearMapping.value);
        thisTransform.position = new Vector3(thisTransform.position.x, thisTransform.position.y, Mathf.Clamp(-linearMapping.value,minMapping,maxMapping));
    }
}
