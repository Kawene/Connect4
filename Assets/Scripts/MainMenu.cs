using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private ColorPickerControl _colorPickerControl;

    [SerializeField]
    private Image _player1Color, _player2Color;

    private Player _player1, _player2;

    void Start()
    {
        _player1 = new Player("Evil", Color.red);
        _player2 = new Player("Good", Color.yellow);
    }

    private void Update()
    {
        _player1Color.color = _player1.PlayerColor;
        _player2Color.color = _player2.PlayerColor;
    }

    public void OnPlayButtonClicked()
    {
        // Logic to start the game
        Debug.Log("Play button clicked. Starting game...");
        // Load the game scene or perform any other action needed to start the game
    }

    public void ChangeColorPlayer1()
    {
        _colorPickerControl.OpenDialog(_player1);
    }

    public void ChangeColorPlayer2()
    {
        _colorPickerControl.OpenDialog(_player2);
    }


}
