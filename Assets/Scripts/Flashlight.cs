using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;
    
    [Header("Battery and Charge")]
    [SerializeField] private int batteryCapacity = 3;
    [SerializeField] private int currentBattery = 3;
    [SerializeField, Min(0)] private float depleteSpeed = 1;
    
    [Header("UI Elements")]
    [SerializeField] private TMP_Text batteryText;
    [SerializeField] private Slider chargeSlider;
    
    private bool _flashlightOn;
    private bool _chargeDepleted;
    private void Update()
    {
        //Depletes flashlight charge when flashlight is on
        if(_flashlightOn)
            chargeSlider.value -= Time.deltaTime * depleteSpeed;
        
        //Changes the UI to show how many batteries the player still has
        batteryText.text = currentBattery + " / " + batteryCapacity;
        
        //Input to change the flashlight turning on and off
        if (Keyboard.current.fKey.wasPressedThisFrame && !_chargeDepleted)
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

        //Add battery replacement function here
        if (chargeSlider.value == 0)
        {
            flashlight.SetActive(false);
            _flashlightOn = false;
            _chargeDepleted = true;
            if (Keyboard.current.gKey.wasPressedThisFrame && currentBattery > 0)
            {
                chargeSlider.value = chargeSlider.maxValue;
                _chargeDepleted = false;
                RemoveBattery(1);
            }
        }

        //Debug Purposes
        // if (Keyboard.current.lKey.wasPressedThisFrame)
        // {
        //     if(currentBattery > 0)
        //         currentBattery--;
        // }
    }

    //Adds battery. Called by BatteryDrawer.cs
    public void AddBattery(int batteryAmount)
    {
        if(currentBattery + batteryAmount > batteryCapacity)
            currentBattery = batteryCapacity;
        else
            currentBattery += batteryAmount;
    }

    public void RemoveBattery(int batteryAmount)
    {
        if(currentBattery - batteryAmount < 0)
            currentBattery = 0;
        else
            currentBattery -= batteryAmount;
    }
}
