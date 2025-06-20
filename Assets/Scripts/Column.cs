using System;
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

    public int PlaceToken(Player player)
    {
        if (_currentSlotIndex < _slots.Count)
        {
            _slots[_currentSlotIndex].ChangeColor(player);
            return _currentSlotIndex++;
        }
        return -1;
    }

    public void Selected()
    {
        _arrow.SetActive(true);
    }

    public void Unselected()
    {
        _arrow.SetActive(false);
    }

    public bool IsSelectedByPlayer(Player player, int row)
    {
        return _slots[row].PlayerOwner == player;
    }

    public void ClearColumn()
    {
        foreach (var slot in _slots)
        {
            slot.ClearSlot();
        }
        _currentSlotIndex = 0;
        Unselected();
    }

    public void SetWinningSlot(int row)
    {
        _slots[row].SetWinningSlot();
    }

    public void RemoveWinnigSlot(int row)
    {
        _slots[row].RemoveWinningSlot();
    }

    public void HighlightSlot(int row)
    {
        _slots[row].HighlightWinningSlot();
    }
}
