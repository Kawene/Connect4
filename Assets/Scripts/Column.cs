using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Column : MonoBehaviour
{
    [SerializeField]
    private List<Slot> _slots;

    [SerializeField]
    private GameObject _arrow;

    private int _currentSlotIndex = 0;

    public int PlaceToken()
    {
        Assert.IsTrue(_currentSlotIndex < _slots.Count, "No more slots available in this column.");
        _slots[_currentSlotIndex++].ChangeColor();
        return _currentSlotIndex;
    }

    public void Selected()
    {
        _arrow.SetActive(true);
    }

    public void Unselected()
    {
        _arrow.SetActive(false);
    }
}
