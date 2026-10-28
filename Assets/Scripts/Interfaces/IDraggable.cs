using UnityEngine;

namespace Interfaces
{
    public interface IDraggable
    {
        public void OnStartDrag(Vector2 position);
        public void OnDrag(Vector2 position);
    }
}