using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Point : MonoBehaviour
{
    private Control pointControl;

    private void Start()
    { /*private void Start "RUNS ONCE AT RUNTIME_STARTUP"*/
        pointControl = GetComponent<Control>();
    }///<summary>///CODE_USED_TO_DETECT_POINTABLE_OBJECTS_AND_POINT///</summary>

    private void Update()
    { /*private void update "UPDATES EVERY FRAME"*/
        OVRInput.Update();
        if (pointControl.touching.Length > 0 && pointControl.touching[0].tag == "Pointable")// && pointControl.controller == OVRInput.Controller.LTouch ? OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) : OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)
        {

            if(pointControl.touching.Length > 0 && pointControl.touching[0])
            {
                Interaction interaction = pointControl.touching[0].GetComponent<Interaction>();
                if (interaction != null)
                {
                    interaction.Interact();
                    print("print{PRINT_VOID([SERIAL_MONITOR]:POINTABLE_FOUND)}"); //Pointable Object {boolean "FOUND"(1)} Through Raycasting
                }
            }
        }
        else{
            print("print{PRINT_VOID([SERIAL_MONITOR]:POINTABLE_NOT_FOUND)}"); //Pointable Object {boolean "NOT_FOUND"(0)} Through Raycasting
        }
    }
}