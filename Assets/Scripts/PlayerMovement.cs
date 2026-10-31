using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera worldCam;
    [SerializeField] private float moveSpeed = 2.75f;
    [SerializeField] private float rotationSpeed;
    
    private void Update()
    { 
        //Gets mouse position
        Vector2 mousePos = worldCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        //Calculates the distance between the mouse position and the current position
        Vector2 direction = mousePos - new Vector2(transform.position.x, transform.position.y); 
        
        //Calculates the degrees that the player will need to turn to face direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        //Takes x,y,z coordinates and turns it into a Quaternion
        Quaternion rotation = Quaternion.Euler(0,0,angle-90);
        
        //Slerps 'rotation' for smoother character rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        
        if (Keyboard.current.wKey.isPressed)
        {
            transform.localPosition += transform.up * (Time.deltaTime * moveSpeed);
        }
            
        if (Keyboard.current.sKey.isPressed)
        {
            transform.localPosition += -transform.up * (Time.deltaTime * moveSpeed * 0.5f);
        }
        
    }
}
