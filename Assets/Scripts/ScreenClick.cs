using Interfaces;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenClick : MonoBehaviour
{
    [SerializeField] private Camera worldCam;
    [SerializeField] private Volume computerVolume;
    
    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            var panini = computerVolume.profile.TryGet<PaniniProjection>(out PaniniProjection p) ? p : null;
            Vector2 src = panini != null && panini.active && panini.distance.value > 0
                ? MouseToSource(worldCam, Mouse.current.position.ReadValue(), panini.distance.value, panini.cropToFit.value)
                : (Vector2) Mouse.current.position.ReadValue();
        
            Vector2 mousePosNew = worldCam.ScreenToWorldPoint(src);
            
            Collider2D ray = Physics2D.OverlapPoint(mousePosNew);
            if (ray != null)
            {
                if (ray.gameObject.TryGetComponent<IScreenButton>(out IScreenButton button))
                {
                    button.Clicked();
                }
            }
        }
    }
    
    /*
     * THIS WAS BUILT BY AI (CLAUDE)
     * Prompt: I currently have Panini Projection post-processing on,
     * but I am using Physics2d.OverlapPoint(<screen to world mouse pos>)
     * which doesn't use the post-processed coordinates,
     * so where the player has to click is a little off. (Unity)
     *
     * I don't have enough experience with coding shaders/graphics to do it myself
     */
    
    // Maps a mouse position (output/post-processed space) to the matching
    // screen position in the un-distorted rendered image.
    static Vector2 PaniniGeneric(Vector2 viewPos, float d)
    {
        float viewDist = 1f + d;
        float viewHypSq = viewPos.x * viewPos.x + viewDist * viewDist;

        float isectD = viewPos.x * d;
        float isectDiscrim = viewHypSq - isectD * isectD;

        float cylDistMinusD = (-isectD * viewPos.x + viewDist * Mathf.Sqrt(isectDiscrim)) / viewHypSq;
        float cylDist = cylDistMinusD + d;

        Vector2 cylPos = viewPos * (cylDist / viewDist);
        return cylPos / (cylDist - d);
    }
    public static Vector2 MouseToSource(Camera cam, Vector2 mousePx, float distance, float cropToFit)
    {
        Vector2 res = new Vector2(Screen.width, Screen.height);
        float aspect = res.x / res.y;

        float fovY = cam.fieldOfView * Mathf.Deg2Rad;
        float tanHalf = Mathf.Tan(0.5f * fovY);
        Vector2 viewExtents = new Vector2(tanHalf * aspect, tanHalf);

        // Crop extents (same as the renderer's CalcCropExtents)
        float sinHalf = Mathf.Sin(0.5f * fovY);
        float cosHalf = Mathf.Cos(0.5f * fovY);
        float cropY = sinHalf * (distance + 1f) / (distance + cosHalf);
        Vector2 cropExtents = new Vector2(cropY * aspect, cropY);

        float scaleF = Mathf.Min(cropExtents.x / viewExtents.x, cropExtents.y / viewExtents.y);
        float paniniS = Mathf.Lerp(1f, Mathf.Clamp01(scaleF), cropToFit);

        // What the fragment shader does:
        Vector2 ndc = (mousePx / res) * 2f - Vector2.one;
        Vector2 viewPos = Vector2.Scale(ndc, viewExtents) * paniniS;
        Vector2 projPos = PaniniGeneric(viewPos, distance);
        Vector2 projNdc = new Vector2(projPos.x / viewExtents.x, projPos.y / viewExtents.y);
        Vector2 srcUV = projNdc * 0.5f + Vector2.one * 0.5f;

        return srcUV * res; // back to pixels
    }
}
