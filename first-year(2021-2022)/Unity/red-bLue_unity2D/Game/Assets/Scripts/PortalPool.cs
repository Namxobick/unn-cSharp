using System;
using System.Collections.Generic;
using UnityEngine;

public class PortalPool : MonoBehaviour
{
    [SerializeField] private List<Portal> _portals;

    private int _busyPortalsCount;

    public Action OnAllPortalsAreFull;

    private void OnEnable()
    {
        foreach (var portal in _portals)
        {
            portal.OnBusyChanged += CheckPortalsFullness;
        }
    }

    private void OnDisable()
    {
        foreach (var portal in _portals)
        {
            portal.OnBusyChanged -= CheckPortalsFullness;
        }
    }

    private void CheckPortalsFullness(bool busy)
    {
        _busyPortalsCount = busy ? ++_busyPortalsCount : --_busyPortalsCount;
        
        if (_portals.Count == _busyPortalsCount)
            OnAllPortalsAreFull?.Invoke();
    }
}
