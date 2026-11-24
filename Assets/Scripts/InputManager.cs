using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions _input;
    private static PlayerInputActions.PlayerActions _player;
    
    public static bool IsMoving { get; private set; }
    public static bool IsMovingBackward { get; private set; }
    public static bool Interacted { get; private set; }
    public static bool OpenedPhone {get; private set;}
    public static bool FlashlightTurnedOn {get; private set;}
    public static bool BatteryReplaced {get; private set;}
    public static bool LeftComputer { get; private set; }
    public static bool Dragging {get; private set;}
    public static bool Clicked {get ; private set;}
    public static bool ClickReleased {get; private set;}
    
    private void Awake()
    {
        _input = new PlayerInputActions();
        _player = _input.Player;
        _player.Enable();
    }

    private void Update()
    {
        IsMoving = _player.MoveForward.IsPressed();
        IsMovingBackward = _player.MoveBackward.IsPressed();
        Dragging = _player.Click.IsPressed();
        
        Interacted = _player.Interact.WasPressedThisFrame();
        OpenedPhone = _player.OpenPhone.WasPressedThisFrame();
        FlashlightTurnedOn = _player.Flashlight.WasPressedThisFrame();
        BatteryReplaced = _player.ReplaceBattery.WasPressedThisFrame();
        LeftComputer = _player.LeaveComputer.WasPressedThisFrame();
        Clicked = _player.Click.WasPressedThisFrame();
        
        ClickReleased = _player.Click.WasReleasedThisFrame();
    }

}
