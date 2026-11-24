using Interfaces;
using UnityEngine;

namespace Computer_Scripts
{
    public class Application : MonoBehaviour, IScreenButton
    {
        [SerializeField] private GameObject window;

        public void Clicked()
        {
            //Doesn't do anything :D
        }

        public void ReleaseClick()
        {
            window.SetActive(true);
        }
    }
}
