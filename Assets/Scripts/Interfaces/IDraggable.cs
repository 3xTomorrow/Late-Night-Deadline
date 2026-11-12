using UnityEngine;

namespace Interfaces
{
    public interface IDraggable
    {
        /// <summary>
        /// Called whenever the IDraggable starts being dragged.
        /// </summary>
        /// <param name="position"></param>Position of where the dragging began.
        public void OnStartDrag(Vector2 position);
        
        /// <summary>
        /// Called while the IDraggable is being dragged.
        /// </summary>
        /// <param name="position"></param>Position to be dragged to.
        public void OnDrag(Vector2 position);
        
        /// <summary>
        /// Called whenever the IDraggable stops being dragged.
        /// </summary>
        public void OnEndDrag();
    }
}