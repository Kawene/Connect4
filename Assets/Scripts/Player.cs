using UnityEngine;

public class Player
{

    [SerializeField]
    private Color _playerColor;
    public Color PlayerColor => _playerColor;

    private string _name;

    public string Name => _name;

    private int _score = 0;
    public int Score => _score;

    public Player(string name, Color color)
    {
        _name = name;
        _playerColor = color;
        _score = 0;
    }

    public void GameWon()
    {
        ++_score;
        Debug.Log($"{_name} won!");
    }
}
