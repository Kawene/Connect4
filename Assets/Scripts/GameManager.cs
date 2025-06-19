using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private int _maxScore = 2;

    private int _currentPlayerIndex;

    private List<Player> _players = new List<Player>(2);

    private InputActions _inputAction;
    private InputAction _placeTokenAction, _selectColumnAction;

    [SerializeField]
    private Board _board;

    [SerializeField]
    private GameUI _gameUI;

    private void Awake()
    {
        _inputAction = new InputActions();

        _placeTokenAction = _inputAction.PlayerInput.PlaceToken;
        _placeTokenAction.Enable();
        _placeTokenAction.performed += ctx => PlaceToken();

        _selectColumnAction = _inputAction.PlayerInput.SelectColumn;
        _selectColumnAction.Enable();
        _selectColumnAction.performed += ctx => SelectColumn();

        _players.Add(new Player("Evil", Color.red));
        _players.Add(new Player("Good", Color.yellow));

        _gameUI.Initialize(_players[0], _players[1]);

        Board.OnGameWon += GameWon;
    }

    private void OnDisable()
    {
        _placeTokenAction.Disable();
        _selectColumnAction.Disable();
    }

    private void SelectColumn()
    {
        float direction = _selectColumnAction.ReadValue<float>();
        if (direction < 0)
        {
            _board.SelectLeft();
        }
        else
        {
            _board.SelectRight();
        }
    }

    public void PlaceToken()
    {
        if (_board.PlaceTokenInBoard(_players[_currentPlayerIndex]))
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % 2;
            _gameUI.SetPlayerIndication(_players[_currentPlayerIndex]);
        }
    }

    private void GameWon(Player winner)
    {
        //Visual effect...
        winner.GameWon();
        _placeTokenAction.Disable();
        _selectColumnAction.Disable();

        if (winner.Score >= _maxScore)
        {
            _gameUI.ShowButton(true, GameFinished);
        }
        else
        {
            _gameUI.ShowButton(false, StartNextGame);
        }
    }

    //Action with button
    public void StartNextGame()
    {
        var temp = _players[0];
        _players[0] = _players[1];
        _players[1] = temp;
        _currentPlayerIndex = 0;

        _gameUI.Initialize(_players[0], _players[1]);
        _placeTokenAction.Enable();
        _selectColumnAction.Enable();
        _board.ClearBoard();
    }

    private void GameFinished()
    {
        Debug.Log("Main Menu ... ");

    }
}
