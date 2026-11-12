using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Computer_Scripts
{
    public class ApplicationWindow : MonoBehaviour, IDraggable
    {
        [SerializeField] private LayerMask borderMask;
        [SerializeField] private List<ApplicationWindow> others;

        private bool _isRaisedInOrdering;
        private bool _canDrag;

        private Renderer[] _windowVisuals;
        private Canvas _windowText;
        
        private Vector2 _offset;

        private void Awake()
        {
            _canDrag = true;
        }
        
        private void Start()
        {
            _windowVisuals = GetComponentsInChildren<Renderer>();
            _windowText = GetComponentInChildren<Canvas>();
        }

        private void OnEnable()
        {
            _canDrag = true;
            
            float randomX = Random.Range(0, .1f);
            float randomY = Random.Range(0, .1f);
            transform.localPosition = new Vector2(randomX, randomY);
        }
        
        private void Update()
        {
            print(gameObject.name + " can be dragged: " + _canDrag);
            
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        public void OnStartDrag(Vector2 position)
        {
            _offset = position - (Vector2)transform.position;
            IncreaseOrderingLayer();
            foreach (ApplicationWindow window in others)
            {
                window.DecreaseOrderingLayer();
                window.DisableDrag();
            }
        }

        public void OnDrag(Vector2 position)
        {
            if(_canDrag)
                transform.position = position - _offset;
        }

        public void OnEndDrag()
        {
            foreach (ApplicationWindow window in others)
            {
                window.EnableDrag();
            }
        }

        /// <summary>
        /// Increases the ordering layer of the window
        /// </summary>
        private void IncreaseOrderingLayer()
        {
            if (!_isRaisedInOrdering)
            {
                print("Increasing ordering layer");
                foreach (Renderer rend in _windowVisuals)
                {
                    rend.sortingOrder+=3;
                }
                _windowText.sortingOrder+=3;
                _isRaisedInOrdering = true;
            }
        }
        
        /// <summary>
        /// Decreases the ordering layer of the window.
        /// Usually used to decrease the ordering layer of other windows.
        /// </summary>
        private void DecreaseOrderingLayer()
        {
            if (_isRaisedInOrdering)
            {
                print("Decreasing ordering layer");
                foreach (Renderer rend in _windowVisuals)
                {
                    rend.sortingOrder-=3;
                }
                _windowText.sortingOrder-=3;
                _isRaisedInOrdering = false;
            }
        }

        private void DisableDrag()
        {
            _canDrag = false;
        }

        private void EnableDrag()
        {
            _canDrag = true;
        }
    }
}