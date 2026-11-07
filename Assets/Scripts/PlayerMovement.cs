using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera worldCam;
    [SerializeField] private float moveSpeed = 2.75f;
    [SerializeField] private float rotationSpeed;
    [SerializeField, Min(0)] private float collisionDampening;
    [SerializeField] private LayerMask nonCollidableLayers;
    
    //If I ever decide to add sprinting, have the Event Handler send the "isSprinting" bool
    public static event EventHandler Moving;

    private bool _canMove;
    
    private void Update()
    { 
        LayerMask mask = ~nonCollidableLayers;
        
        //Gets mouse position
        Vector2 mousePos = worldCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //Calculates the distance between the mouse position and the current position
        Vector2 direction = mousePos - new Vector2(transform.position.x, transform.position.y); 
        //Calculates the degrees that the player will need to turn to face direction
        float targetAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        //Takes x,y,z coordinates and turns it into a Quaternion
        Quaternion rotation = Quaternion.Euler(0,0,targetAngle);
        //Slerps 'rotation' for smoother character rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        
        Vector2 curPosition = new Vector2(transform.position.x, transform.position.y);
        
        _canMove = !Physics2D.CircleCast(curPosition, transform.lossyScale.x / 2, transform.up,collisionDampening,mask);
        Debug.DrawRay(curPosition, transform.up * ((transform.lossyScale.x / 2) + collisionDampening), Color.green);
        
        if (Keyboard.current.wKey.isPressed && _canMove)
        {
            transform.localPosition += transform.up * (Time.deltaTime * moveSpeed);
            Moving?.Invoke(this, EventArgs.Empty);
        }
        
        /*if (Keyboard.current.sKey.isPressed && _canMove)
        {
            transform.localPosition += -transform.up * (Time.deltaTime * moveSpeed * 0.5f);
            Moving?.Invoke(this, EventArgs.Empty); 
        }*/
        
    }
    
}
