using Interfaces;
using UnityEngine;

public class BatteryDrawer : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        print("Battery added to inventory");
    }
}
