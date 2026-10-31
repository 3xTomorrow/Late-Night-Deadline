using System;
using UnityEngine;

public class FlashlightPushback : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out TheStranger stranger))
        {
            stranger.Shine();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out TheStranger stranger))
        {
            stranger.Darken();
        }
    }
}
