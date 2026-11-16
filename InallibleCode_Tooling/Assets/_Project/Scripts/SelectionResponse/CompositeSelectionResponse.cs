using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.SelectionResponse
{
    public class CompositeSelectionResponse : MonoBehaviour, ISelectionResponse, IChangeable
    {
        [SerializeField] private GameObject selectionResponseHolder;
    
        private List<ISelectionResponse> _selectionResponses;
        private int _currentIndex;
        private Transform _selection;

        private void Start()
        {
            if (selectionResponseHolder == null)
            {
                Debug.LogError("Selection response holder is not assigned.", this);
                _selectionResponses = new List<ISelectionResponse>();
                return;
            }

            _selectionResponses = selectionResponseHolder.GetComponents<ISelectionResponse>().ToList();
            if (_selectionResponses.Count == 0)
            {
                Debug.LogError("Selection response holder has no ISelectionResponse components.", selectionResponseHolder);
            }
        }

        [ContextMenu("Next")]
        public void Next()
        {
            if (_selectionResponses == null || _selectionResponses.Count == 0)
                return;

            if (_selection != null)
            {
                _selectionResponses[_currentIndex].OnDeselect(_selection);
            }

            _currentIndex = (_currentIndex + 1) % _selectionResponses.Count;

            if (_selection != null)
            {
                _selectionResponses[_currentIndex].OnSelect(_selection);
            }
        }
    
        public void OnSelect(Transform selection)
        {
            _selection = selection;
            if (HasSelection())
            {
                _selectionResponses[_currentIndex].OnSelect(selection);
            }
        }

        public void OnDeselect(Transform selection)
        {
            _selection = null;
            if (HasSelection())
            {
                _selectionResponses[_currentIndex].OnDeselect(selection);
            }
        }

        private bool HasSelection()
        {
            return _selectionResponses != null &&
                   _currentIndex > -1 &&
                   _currentIndex < _selectionResponses.Count;
        }
    }
}