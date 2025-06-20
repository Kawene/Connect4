using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private ColorPickerControl _colorPickerControl;

    [SerializeField]
    private Image _player1Color, _player2Color;

    [SerializeField]
    private TMP_InputField _player1Name, _player2Name, _numberOfWins;


    private Player _player1, _player2;

    void Start()
    {
        _player1 = new Player("Evil", Color.red);
        _player2 = new Player("Good", Color.yellow);

        _player1Name.text = PlayerPrefs.GetString("Player1Name", _player1.Name);
        _player2Name.text = PlayerPrefs.GetString("Player2Name", _player2.Name);

        if (ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("Player1Color", "FF0000"), out Color player1Color))
        {
            _player1.SetColor(player1Color);
        }
        if (ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("Player2Color", "FFFF00"), out Color player2Color))
        {
            _player2.SetColor(player2Color);
        }

        _numberOfWins.text = PlayerPrefs.GetInt("NumberOfWins", 1).ToString();
    }

    private void Update()
    {
        _player1Color.color = _player1.PlayerColor;
        _player2Color.color = _player2.PlayerColor;

        _player1.SetName(_player1Name.text);
        _player2.SetName(_player2Name.text);
    }

    public void Play()
    {
       
        string player1Name = string.IsNullOrWhiteSpace(_player1Name.text) ? "Player 1" : _player1Name.text;
        string player2Name = string.IsNullOrWhiteSpace(_player2Name.text) ? "Player 2" : _player2Name.text;
        PlayerPrefs.SetString("Player1Name", player1Name);
        PlayerPrefs.SetString("Player2Name", player2Name);

        PlayerPrefs.SetString("Player1Color", ColorUtility.ToHtmlStringRGBA(_player1.PlayerColor));
        PlayerPrefs.SetString("Player2Color", ColorUtility.ToHtmlStringRGBA(_player2.PlayerColor));

        int wins = 1;
        if (!int.TryParse(_numberOfWins.text, out wins))
        {
            wins = 1;
        }
        PlayerPrefs.SetInt("NumberOfWins", wins);

        PlayerPrefs.Save();

        SceneManager.LoadScene("Connect4");
    }

    public void ChangeColorPlayer1()
    {
        _colorPickerControl.OpenDialog(_player1);
    }

    public void ChangeColorPlayer2()
    {
        _colorPickerControl.OpenDialog(_player2);
    }

    public void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
