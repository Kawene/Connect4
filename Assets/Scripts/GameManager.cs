using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

        string player1Name = PlayerPrefs.GetString("Player1Name", "Player1");
        string player2Name = PlayerPrefs.GetString("Player2Name", "Player2");
        Color player1Color;
        Color player2Color;

        if (!ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("Player1Color", "FF0000"), out player1Color))
        {
            player1Color = Color.red;
        }

        if (!ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("Player2Color", "FFFF00"), out player2Color))
        {
            player2Color = Color.yellow; 
        }

        _maxScore = PlayerPrefs.GetInt("NumberOfWins", 1);

        _players.Add(new Player(player1Name, player1Color));
        _players.Add(new Player(player2Name, player2Color));

        _gameUI.Initialize(_players[0], _players[1], _maxScore);

        Board.OnGameWon += GameWon;
    }

    private void OnDestroy()
    {
        Board.OnGameWon -= GameWon;
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
        _gameUI.UpdateScore(winner);
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

        _gameUI.Initialize(_players[0], _players[1], _maxScore);
        _placeTokenAction.Enable();
        _selectColumnAction.Enable();
        _board.ClearBoard();
    }

    private void GameFinished()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
