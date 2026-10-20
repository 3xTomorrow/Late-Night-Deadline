using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.75f;
    [SerializeField] private float turnSpeed = 100f;
    
    //private bool _turning = false;
    //private bool _moving = false;
    private bool _isMovingBackwards = false;
    
    private void Update()
    { 
        
        if (Keyboard.current.wKey.isPressed)
        { transform.localPosition += transform.up * (Time.deltaTime * moveSpeed);
        }
            
        if (Keyboard.current.sKey.isPressed)
        {
            transform.localPosition += -transform.up * (Time.deltaTime * moveSpeed * 0.5f);
            _isMovingBackwards = true;
        }
        else
        {
            _isMovingBackwards = false;
        }

        if (!_isMovingBackwards)
        {
            if (Keyboard.current.dKey.isPressed)
            { 
                if(!_isMovingBackwards)
                    transform.localRotation *= Quaternion.Euler(0f, 0f, -turnSpeed*Time.deltaTime).normalized;
                else
                    transform.localRotation *= Quaternion.Euler(0f, 0f, turnSpeed*Time.deltaTime).normalized;
            }

            if (Keyboard.current.aKey.isPressed)
            { 
                if (!_isMovingBackwards)
                    transform.localRotation *= Quaternion.Euler(0f, 0f, turnSpeed*Time.deltaTime).normalized;
                else
                    transform.localRotation *= Quaternion.Euler(0f, 0f, -turnSpeed*Time.deltaTime).normalized;
            }
        }
    }
}
