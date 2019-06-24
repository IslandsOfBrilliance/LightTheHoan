using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Point : MonoBehaviour{
    private Control pointControl;
    private void Start(){ /*private void Start "RUNS ONCE AT RUNTIME_STARTUP"*/
        pointControl = GetComponent<Control>();
    }///<summary>///CODE_USED_TO_DETECT_POINTABLE_OBJECTS_AND_POINT///</summary>
    private void Update(){ /*private void update "UPDATES EVERY FRAME"*/
        if (pointControl.interactables.Length >= 1 && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && pointControl.interactables[0].tag == "Pointable"){
            print("print{PRINT_VOID([SERIAL_MONITOR]:POINTABLE_FOUND)}"); //Pointable Object {boolean "FOUND"(1)} Through Raycasting
            print("print{PRINT_VOID([SERIAL_MONITOR]:POINTABLE_NOT_FOUND)}"); //Pointable Object {boolean "NOT_FOUND"(0)} Through Raycasting
        }
    }
}