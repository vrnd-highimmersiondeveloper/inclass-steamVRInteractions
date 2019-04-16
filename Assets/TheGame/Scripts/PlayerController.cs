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
        //Debug.Log("Update");
        Debug.Log("ActionSTate Teleport " + SteamVR_Actions._default.Teleport.GetActiveBinding(SteamVR_Input_Sources.Any));
        Debug.Log("ActionSTate movepaddle " + SteamVR_Actions._default.MovePaddle.GetActiveBinding(SteamVR_Input_Sources.Any));
        Debug.Log("ActionSTate Teleport " + SteamVR_Actions._default.Teleport.GetActiveBinding(SteamVR_Input_Sources.Any));
        Debug.Log("ActionSTate movepaddle active " + SteamVR_Actions._default.MovePaddle.GetActive(SteamVR_Input_Sources.Any));
        //Debug.Log(SteamVR_Actions._default.Teleport.GetActive);
        Debug.Log(SteamVR_Actions._default.MovePaddle.GetAxis(SteamVR_Input_Sources.LeftHand));
   
        Debug.Log("Update");
        var leftHoriz = movePaddleAction.GetAxis(SteamVR_Input_Sources.LeftHand);
        var rightHoriz = movePaddleAction.GetAxis(SteamVR_Input_Sources.RightHand);
        Debug.Log("Update " + leftHoriz.ToString() + ", " + rightHoriz.ToString());
        //Debug.Log(sensitivity + "linear mapping " + linearMapping.value.ToString());

        if (leftHoriz.x < -sensitivity || rightHoriz.x < -sensitivity)
        {
            //move paddle left
            if (linearMapping.value < maxMapping)
            {
                linearMapping.value += mapStep;
                Debug.Log("move Left: " + linearMapping.value);
            }
        }
        else if (leftHoriz.x > sensitivity || rightHoriz.x > sensitivity)
        {
            if (linearMapping.value > minMapping)
            {
                linearMapping.value -= mapStep;
                Debug.Log(linearMapping.value);
            }
        }

        Debug.Log(linearMapping.value);
        //thisRb.velocity = new Vector3(0,0,linearMapping.value);
        thisTransform.position = new Vector3(thisTransform.position.x, thisTransform.position.y, Mathf.Clamp(-linearMapping.value,minMapping,maxMapping));
    }
}
