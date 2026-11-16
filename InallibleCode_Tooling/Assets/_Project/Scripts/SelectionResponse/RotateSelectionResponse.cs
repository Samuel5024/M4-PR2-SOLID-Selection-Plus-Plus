using UnityEngine;

namespace _Project.Scripts.SelectionResponse
{
    public class RotateSelectionResponse : MonoBehaviour, ISelectionResponse
    {
        [SerializeField] private float liftHeight = 0.3f;
        [SerializeField] private float bobAmplitude = 0.1f;
        [SerializeField] private float bobFrequency = 2f;
        [SerializeField] private float rotationSpeed = 180f;

        private Transform _selection;
        private Vector3 _originalPosition;
        private Quaternion _originalRotation;
        private float _elapsedTime;

        private void Update()
        {
            if (_selection == null) return;

            _elapsedTime += Time.deltaTime;
            var bobOffset = liftHeight +
                            Mathf.Sin(_elapsedTime * bobFrequency * Mathf.PI * 2f) * bobAmplitude;

            _selection.position = _originalPosition + Vector3.up * bobOffset;
            _selection.rotation =
                _originalRotation * Quaternion.Euler(0f, _elapsedTime * rotationSpeed, 0f);
        }

        public void OnSelect(Transform selection)
        {
            if (selection == null) return;
            if (_selection != null) RestoreSelection();

            _selection = selection;
            _originalPosition = selection.position;
            _originalRotation = selection.rotation;
            _elapsedTime = 0f;
        }

        public void OnDeselect(Transform selection)
        {
            if (_selection != selection) return;

            RestoreSelection();
        }

        private void OnDisable()
        {
            RestoreSelection();
        }

        private void RestoreSelection()
        {
            if (_selection != null)
            {
                _selection.position = _originalPosition;
                _selection.rotation = _originalRotation;
            }

            _selection = null;
            _elapsedTime = 0f;
        }
    }
}
