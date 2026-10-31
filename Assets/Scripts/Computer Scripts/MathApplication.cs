using Interfaces;
using UnityEngine;

public class MathApplication : MonoBehaviour, IScreenButton
{
    [SerializeField] private GameObject mathWindow;
    
    public void Clicked()
    {
        
    }

    public void ReleaseClick()
    {
        mathWindow.SetActive(true);
    }
}
