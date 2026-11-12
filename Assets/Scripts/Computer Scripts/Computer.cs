using System;
using Interfaces;
using UnityEngine;

namespace Computer_Scripts
{
    public class Computer : MonoBehaviour, IInteractable
    {
        public static event EventHandler OnComputerInteract;

        public void Interact()
        {
            OnComputerInteract?.Invoke(this, EventArgs.Empty);
        }
    }
}