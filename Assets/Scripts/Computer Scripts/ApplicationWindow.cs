using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

public class ApplicationWindow : MonoBehaviour, IDraggable
{
    [SerializeField] private LayerMask borderMask;

    private Vector2 _offset;
    
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
    public void OnStartDrag(Vector2 position)
    {
        _offset = position - (Vector2)transform.position;
    }
    
    public void OnDrag(Vector2 position)
    {
        transform.position = position - _offset;
    }

    public void OnEndDrag()
    {
        print("ending drag");
    }
}
