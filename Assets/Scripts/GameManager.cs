using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
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
        _selectColumnAction = _inputAction.PlayerInput.SelectColumn;

        _players.Add(new Player("Evil", Color.red));
        _players.Add(new Player("Good", Color.yellow));

        _gameUI.Initialize(_players[0], _players[1]);
    }

    private void OnEnable()
    {
        _placeTokenAction.Enable();
        _placeTokenAction.performed += ctx => PlaceToken();

        _selectColumnAction.Enable();
        _selectColumnAction.performed += ctx => SelectColumn();
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
}
