using Interfaces;
using UnityEngine;

public class BatteryDrawer : MonoBehaviour, IInteractable
{
    [SerializeField] private Flashlight flashlight;
    
    public void Interact()
    {
        flashlight.AddBattery(1);
    }
}
