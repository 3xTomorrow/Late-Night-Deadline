using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private float _startingSpeed;

    private void Awake()
    {
        _startingSpeed = moveSpeed;
    }
    
    private void Update()
    {
        float xDir = 0f;
        float yDir = 0f;

        if (Keyboard.current.wKey.isPressed)
            yDir = 1;
        
        if (Keyboard.current.sKey.isPressed)
            yDir = -1;
        
        if(Keyboard.current.dKey.isPressed)
            xDir = 1;
        
        if(Keyboard.current.aKey.isPressed)
            xDir = -1;
        
        Vector2 movement = new Vector2(xDir, yDir);
        movement = movement.normalized;
        transform.position += new Vector3(movement.x*moveSpeed*Time.deltaTime, movement.y*moveSpeed*Time.deltaTime,0);
    }
}
