using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Point : MonoBehaviour{
    private Control pointControl;
    private void Start(){ /*private void Start "RUNS ONCE AT RUNTIME_STARTUP"*/
        pointControl = GetComponent<Control>();
    }///<summary>///CODE_USED_TO_DETECT_POINTABLE_OBJECTS_AND_POINT///</summary>
    private void Update(){ /*private void update "UPDATES EVERY FRAME"*/
        OVRInput.Update();
        if (pointControl.interactables.Length > 0 && pointControl.interactables[0].tag == "Pointable" && pointControl.controller == OVRInput.Controller.LTouch ? OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) : OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)){
            print("print{PRINT_VOID([SERIAL_MONITOR]:POINTABLE_FOUND)}"); //Pointable Object {boolean "FOUND"(1)} Through Raycasting
            Interaction interaction = pointControl.interactables[0].GetComponent<Interaction>();
            if (interaction != null){
                interaction.Interact();
            }
        }
        else{
            print("print{PRINT_VOID([SERIAL_MONITOR]:POINTABLE_NOT_FOUND)}"); //Pointable Object {boolean "NOT_FOUND"(0)} Through Raycasting
        }
    }
}