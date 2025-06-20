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

    private List<(int, int)> _winningPosition = new List<(int, int)>(4);

    private void Start()
    {
        SelectDefaultColumn();
    }

    public bool PlaceTokenInBoard(Player player)
    {
        int row = _columns[_selectedColumnIndex].PlaceToken(player);
        if (row < 0)        
            return false;
        
        CheckWin(player, row, _selectedColumnIndex);
        return true;
    }

    public void CheckWin(Player player, int row, int col)
    {
        if (CheckTopDown(player, row, col) >= 3 ||
            CheckLeftRight(player, row, col) >= 3 ||
            CheckLeftDiagonal(player, row, col) >= 3 ||
            CheckRightDiagonal(player, row, col) >= 3)
        {
            _columns[col].SetWinningSlot(row);
            _winningPosition.Add((row, col));
            HighlightWinningSlots();
            OnGameWon(player);
        }
    }

    private int CheckTopDown(Player player, int row, int col)
    {
        ResetWinning();
        return CheckDirection(Direction.Top, player, row, col, 0) 
            + CheckDirection(Direction.Down, player, row, col, 0);
    }
    private int CheckLeftRight(Player player, int row, int col)
    {
        ResetWinning();
        return CheckDirection(Direction.Left, player, row, col, 0) 
            + CheckDirection(Direction.Right, player, row, col, 0);
    }
    private int CheckLeftDiagonal(Player player, int row, int col)
    {
        ResetWinning();
        return CheckDirection(Direction.LeftDiagonalDown, player, row, col, 0) 
            + CheckDirection(Direction.LeftDiagonalTop, player, row, col, 0);
    }

    private int CheckRightDiagonal(Player player, int row, int col)
    {
        ResetWinning();
        return CheckDirection(Direction.RightDiagonalDown, player, row, col, 0) 
            + CheckDirection(Direction.RightDiagonalTop, player, row, col, 0);
    }

    private int CheckDirection(Direction direction, Player player, int row, int col, int iteration)
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
            _columns[col].SetWinningSlot(row);
            _winningPosition.Add((row, col));
            return CheckDirection(direction, player, row, col, iteration + 1);
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

    private void ResetWinning()
    {
        foreach (var position in _winningPosition)
        {
            _columns[position.Item2].RemoveWinnigSlot(position.Item1);
        }
        _winningPosition.Clear();
    }

    private void HighlightWinningSlots()
    {
        foreach (var position in _winningPosition)
        {
            _columns[position.Item2].HighlightSlot(position.Item1);
        }
    }

    public void SetArrowColor(Color color)
    {
        foreach (var column in _columns)
        {
            column.SetArrowColor(color);
        }
    }
}
