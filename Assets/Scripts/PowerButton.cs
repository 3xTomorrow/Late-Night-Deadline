using System;
using Interfaces;
using UnityEngine;

public class PowerButton : MonoBehaviour, IScreenButton
{
    public static event EventHandler PowerButtonPressed;
    
    public void Clicked()
    {
        PowerButtonPressed?.Invoke(this, EventArgs.Empty);
    }
}
