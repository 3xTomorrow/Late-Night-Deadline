using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

public class ApplicationWindow : MonoBehaviour, IDraggable
{
    private Vector2 _offset;
    
    public void OnStartDrag(Vector2 position)
    {
        _offset = position - (Vector2)transform.position; //Don't know if this works yet
    }
    
    public void OnDrag(Vector2 position)
    {
        print("being dragged");
        
        //When being dragged, the object moves towards <position> plus the offset
    }
    
}
