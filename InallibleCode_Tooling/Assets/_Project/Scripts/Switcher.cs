using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    public List<GameObject> ChangeableObjects;
    
    private readonly List<IChangeable> _changeableObjects = new List<IChangeable>();

    private void Start()
    {
        if (ChangeableObjects == null)
        {
            Debug.LogError("Switcher has no ChangeableObjects list assigned.", this);
            return;
        }

        foreach (var changeable in ChangeableObjects)
        {
            if (changeable == null)
            {
                Debug.LogError("Switcher contains an empty ChangeableObjects entry.", this);
                continue;
            }

            var changeableComponent = changeable.GetComponent<IChangeable>();
            if (changeableComponent == null)
            {
                Debug.LogError($"{changeable.name} does not have a component implementing IChangeable.", changeable);
                continue;
            }

            _changeableObjects.Add(changeableComponent);
        }

        if (_changeableObjects.Count == 0)
        {
            Debug.LogError("Switcher has no valid IChangeable components to switch.", this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            foreach (var changeable in _changeableObjects)
            {
                changeable.Next();
            }
        }
    }
}