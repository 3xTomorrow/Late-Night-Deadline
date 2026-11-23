using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static PlayerInputActions _input;
    
    public static bool IsMoving { get; private set; }
    public static bool IsMovingBackward { get; private set; }
    public static bool Interacted { get; private set; }
    public static bool OpenedPhone {get; private set;}
    public static bool FlashlightTurnedOn {get; private set;}
    public static bool BatteryReplaced {get; private set;}
    
    private void Awake()
    {
        _input = new PlayerInputActions();
        _input.Enable();
    }

    private void Update()
    {
        IsMoving = _input.Player.MoveForward.IsPressed();
        IsMovingBackward = _input.Player.MoveBackward.IsPressed();
        Interacted = _input.Player.Interact.WasPressedThisFrame();
        OpenedPhone = _input.Player.OpenPhone.WasPressedThisFrame();
        FlashlightTurnedOn = _input.Player.Flashlight.WasPressedThisFrame();
        BatteryReplaced = _input.Player.ReplaceBattery.WasPressedThisFrame();
    }

}
