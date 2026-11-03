using UnityEngine;
using UnityEngine.InputSystem;

public class Phone : MonoBehaviour
{
    public enum PhoneApp
    {
        Messages
    }
    
    private const string OpenPhone = "OpenPhone";
    
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerRaycast playerRaycast;
    [SerializeField] private GameObject messagesScreen;
    
    private Animator _animator;
    private bool _phoneOpen;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            _animator.SetTrigger(OpenPhone);
            if (!_phoneOpen)
            {
                playerMovement.enabled = false;
                playerRaycast.enabled = false;
                _phoneOpen = true;
            }
            else
            {
                playerMovement.enabled = true;
                playerRaycast.enabled = true;
                _phoneOpen = false;
            }
        }
    }

    private void OpenApp(PhoneApp app) 
    {
        if (app == PhoneApp.Messages)
        {
            messagesScreen.SetActive(true);
        }
    }

    private void CloseApp(PhoneApp app)
    {
        if (app == PhoneApp.Messages)
        {
            messagesScreen.SetActive(false);
        }
    }
    
    //Wrapper methods for opening apps
    public void OpenMessagesApp() => OpenApp(PhoneApp.Messages);
    
    //wrapper methods for closing apps
    public void CloseMessagesApp() => CloseApp(PhoneApp.Messages);
}
