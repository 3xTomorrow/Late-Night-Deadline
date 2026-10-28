using UnityEngine;

public class NoPass : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ApplicationWindow aw))
        {
            aw.transform.localPosition = Vector2.zero;
        }
    }
}
