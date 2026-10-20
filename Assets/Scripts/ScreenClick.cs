using Interfaces;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenClick : MonoBehaviour
{
    [SerializeField] private Camera worldCam;
    
    private void Update()
    {
        Vector2 mousePosition = worldCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        if (Mouse.current.leftButton.isPressed)
        {
            Collider2D ray = Physics2D.OverlapPoint(mousePosition);
            if (ray != null)
            {
                if (ray.gameObject.TryGetComponent<IScreenButton>(out IScreenButton button))
                {
                    button.Clicked();
                }
            }
        }
    }
}
