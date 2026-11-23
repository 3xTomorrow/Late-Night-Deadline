using System;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Computer_Scripts
{
    public class PowerButton : MonoBehaviour, IScreenButton
    {
        public static event EventHandler PowerButtonPressed;

        private void Update()
        {
            if (Keyboard.current.cKey.wasPressedThisFrame && !InputBlocker.IsTyping)
            {
                PowerButtonPressed?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Clicked()
        {

        }

        public void ReleaseClick()
        {
        }
    }
}