using UnityEngine;

namespace _Project.Scripts.SelectionResponse
{
    public interface ISelectionResponse
    {
        void OnSelect(Transform selection);
        void OnDeselect(Transform selection);
    }
}