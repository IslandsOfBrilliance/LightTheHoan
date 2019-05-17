using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static bool A { get; protected set; }
    public static bool LeftGrab { get; protected set; }
    public static bool RightGrab { get; protected set; }
    public static Vector2 Move { get; protected set; }
    public static Vector2 Look { get; protected set; }
    public static Vector2 MoveRaw { get; protected set; }
    public static Vector2 LookRaw { get; protected set; }

    private void Update()
    {
        OVRInput.Update();
        LeftGrab = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger);
        RightGrab = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);
        A = OVRInput.Get(OVRInput.Button.One);

        Move = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Look = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        MoveRaw = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        LookRaw = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        print("Input: " + LeftGrab + " " + RightGrab);
    }
}
