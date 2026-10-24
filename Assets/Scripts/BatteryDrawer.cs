using System.Collections;
using Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BatteryDrawer : MonoBehaviour, IInteractable
{
    private const string InteractTrigger = "InteractTrigger";
    
    [SerializeField] private Flashlight flashlight;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerRaycast playerRaycast;

    private Animator _anim;

    private void Awake()
    {
        _anim = gameObject.GetComponent<Animator>();
    }
    
    public void Interact()
    {
        if (!flashlight.IsFullOnBattery())
        {
            _anim.SetTrigger(InteractTrigger);
            StartCoroutine(CollectBattery());
        }
    }

    IEnumerator CollectBattery()
    {
        playerMovement.enabled = false;
        playerRaycast.enabled = false;
        yield return new WaitForSeconds(3f);
        flashlight.AddBattery(1);
        playerMovement.enabled = true;
        playerRaycast.enabled = true;
        _anim.SetTrigger(InteractTrigger);
    }
}
