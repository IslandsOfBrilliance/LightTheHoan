using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("print{PRINT_VOID([SERIAL_MONITOR]:TRUE_BUTTON)}");
        print("print{PRINT_VOID([SERIAL_MONITOR]:FALSE_BUTTON)}");
    }
}
