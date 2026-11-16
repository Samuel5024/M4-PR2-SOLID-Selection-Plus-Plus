using UnityEngine;

namespace _Project.Scripts.SelectionResponse
{
    public class ScaleSelectionResponse : MonoBehaviour, ISelectionResponse
    {
        [SerializeField] private float scaleAmplitude = 0.05f;
        [SerializeField] private float scaleFrequency = 2f;

        private Transform _selection;
        private Vector3 _originalScale;
        private float _elapsedTime;
        private void Update()
        {
            if (_selection == null) return;

            _elapsedTime += Time.deltaTime;
            var scaleOffset =
                Mathf.Sin(_elapsedTime * scaleFrequency * Mathf.PI * 2f) * scaleAmplitude;

            _selection.localScale = _originalScale * (1f + scaleOffset);
        }

        public void OnSelect(Transform selection)
        {
            if (selection == null) return;
            if (selection != null) RestoreScale();

            _selection = selection;
            _originalScale = selection.localScale;
            _elapsedTime = 0f;
        }

        public void OnDeselect(Transform selection)
        {
            if (_selection != selection) return;

            RestoreScale();
        }

        private void OnDisable()
        {
            RestoreScale();
        }

        private void RestoreScale()
        {
            if (_selection != null)
            {
                _selection.localScale = _originalScale;
            }

            _selection = null;
            _elapsedTime = 0f;
        }
    }
}
