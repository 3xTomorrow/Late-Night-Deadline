using Interfaces;
using UnityEngine;

public class MathApplication : MonoBehaviour, IScreenButton
{
    public void Clicked()
    {
        print("Opening Math Application");
    }
}
