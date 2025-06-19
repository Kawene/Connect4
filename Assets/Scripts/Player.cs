using UnityEngine;

public class Player
{

    [SerializeField]
    private Color _playerColor;
    public Color PlayerColor => _playerColor;

    private string _name;

    public string Name => _name;

    public Player(string name, Color color)
    {
        _name = name;
        _playerColor = color;
    }
}
