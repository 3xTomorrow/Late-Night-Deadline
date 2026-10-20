using System;
using Interfaces;
using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    public static event EventHandler OnComputerInteract;
    
    public void Interact()
    {
        OnComputerInteract?.Invoke(this, EventArgs.Empty);
    }
}
