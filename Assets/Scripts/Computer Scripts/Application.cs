using Interfaces;
using UnityEngine;

namespace Computer_Scripts
{
    public class Application : MonoBehaviour, IScreenButton
    {
        [SerializeField] private GameObject window;

        public void Clicked()
        {

        }

        public void ReleaseClick()
        {
            window.SetActive(true);
        }
    }
}
