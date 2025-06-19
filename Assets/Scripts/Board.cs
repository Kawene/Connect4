using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private List<Column> _columns = new List<Column>(7);

    private int _selectedColumnIndex;

    private void Start()
    {
        _selectedColumnIndex = 3;
        _columns[_selectedColumnIndex].Selected();
    }

    public void CheckWin(int columnIndex, int rowIndex)
    {
        //Check horizontal, vertical, and diagonal connections for a win condition ...
        Debug.Log($"Checking win condition for column {columnIndex}, row {rowIndex}");
    }

    public void PlaceTokenInBoard()
    {
        int row = _columns[_selectedColumnIndex].PlaceToken();
        CheckWin(_selectedColumnIndex, row);
    }

    public void SelectRight()
    {
        _columns[_selectedColumnIndex].Unselected();
        _selectedColumnIndex++;
        if (_selectedColumnIndex >= _columns.Count)
        {
            _selectedColumnIndex = 0;
        }
        _columns[_selectedColumnIndex].Selected();
    }

    public void SelectLeft()
    {
        _columns[_selectedColumnIndex].Unselected();
        _selectedColumnIndex--;
        if (_selectedColumnIndex < 0)
        {
            _selectedColumnIndex = _columns.Count - 1;
        }
        _columns[_selectedColumnIndex].Selected();
    }
}
