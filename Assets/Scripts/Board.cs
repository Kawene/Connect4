using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    Top,
    Down,
    Left,
    Right,
    LeftDiagonalTop,
    LeftDiagonalDown,
    RightDiagonalTop,
    RightDiagonalDown
}

public class Board : MonoBehaviour
{
    [SerializeField]
    private List<Column> _columns = new List<Column>(7);

    private int _selectedColumnIndex;

    public delegate void GameWonEvent(Player player);
    public static GameWonEvent OnGameWon;

    private void Start()
    {
        SelectDefaultColumn();
    }

    public bool PlaceTokenInBoard(Player player)
    {
        int row = _columns[_selectedColumnIndex].PlaceToken(player);
        if (row < 0)        
            return false;
        
        CheckWin(_selectedColumnIndex, row, player);
        return true;
    }

    public void CheckWin(int columnIndex, int rowIndex, Player player)
    {

        int t1 = CheckTopDown(player, rowIndex, columnIndex);
        int t2 = CheckLeftRight(player, rowIndex, columnIndex);
        int t3 = CheckLeftDiagonal(player, rowIndex, columnIndex);
        int t4 = CheckRightDiagonal(player, rowIndex, columnIndex);

        if (CheckTopDown(player, rowIndex, columnIndex) >= 3 ||
            CheckLeftRight(player, rowIndex, columnIndex) >= 3 ||
            CheckLeftDiagonal(player, rowIndex, columnIndex) >= 3 ||
            CheckRightDiagonal(player, rowIndex, columnIndex) >= 3)
        {
            OnGameWon(player);
        }
    }

    private int CheckTopDown(Player player, int row, int col)
    {
        return CheckDirection(Direction.Top, player, 0, row, col) 
            + CheckDirection(Direction.Down, player, 0, row, col);
    }
    private int CheckLeftRight(Player player, int row, int col)
    {
        return CheckDirection(Direction.Left, player, 0, row, col) 
            + CheckDirection(Direction.Right, player, 0, row, col);
    }
    private int CheckLeftDiagonal(Player player, int row, int col)
    {
        return CheckDirection(Direction.LeftDiagonalDown, player, 0, row, col) 
            + CheckDirection(Direction.LeftDiagonalTop, player, 0, row, col);
    }

    private int CheckRightDiagonal(Player player, int row, int col)
    {
        return CheckDirection(Direction.RightDiagonalDown, player, 0, row, col) 
            + CheckDirection(Direction.RightDiagonalTop, player, 0, row, col);
    }

    private int CheckDirection(Direction direction, Player player, int iteration, int row, int col)
    {
        if (iteration >= 3)
        {
            return iteration;
        }

        switch (direction)
            {
            case Direction.Top:
                ++row;
                break;
            case Direction.Down:
                --row;
                break;
            case Direction.Left:
                --col;
                break;
            case Direction.Right:
                ++col;
                break;
            case Direction.LeftDiagonalTop:
                ++row;
                --col;
                break;
            case Direction.LeftDiagonalDown:
                --row;
                ++col;
                break;
            case Direction.RightDiagonalTop:
                ++row;
                ++col;
                break;
            case Direction.RightDiagonalDown:
                --row;
                --col;
                break;
        }

        if (row > 5 || col >= _columns.Count || row < 0 || col < 0)
        {
            return iteration;
        }

        if (_columns[col].IsSelectedByPlayer(player, row))
        {
            return CheckDirection(direction, player, iteration + 1, row, col);
        }
        else
        {
            return iteration;
        }

    }

    public void ClearBoard()
    {
        foreach (var column in _columns)
        {
            column.ClearColumn();
        }
        SelectDefaultColumn();
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

    private void SelectDefaultColumn()
    {
        _selectedColumnIndex = 3;
        _columns[_selectedColumnIndex].Selected();
    }
}
