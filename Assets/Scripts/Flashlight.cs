using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;
    [SerializeField] private int batteryCapacity = 3;
    [SerializeField] private int currentBattery = 3;
    
    private bool _flashlightOn;
    
    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (!_flashlightOn)
            {
                flashlight.SetActive(true);
                _flashlightOn = true;
            }
            else
            {
                flashlight.SetActive(false);
                _flashlightOn = false;
            }
        }

        //Debug Purposes
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            currentBattery--;
        }
    }
}
