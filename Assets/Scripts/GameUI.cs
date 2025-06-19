using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _player1Name;

    [SerializeField]
    private Image _player1Color;

    [SerializeField]
    private TextMeshProUGUI _player1Score;

    [SerializeField]
    private TextMeshProUGUI _player2Name;


    [SerializeField]
    private TextMeshProUGUI _titleText;

    [SerializeField]
    private Image _player2Color;

    [SerializeField]
    private TextMeshProUGUI _player2Score;

    [SerializeField]
    private Button _endGameButton;

    [SerializeField]
    private TextMeshProUGUI _endGameButtonText;

    private const string _prefixTitle = "First Player to reach ";
    private const string _suffixTitle = " points wins";

    public void Initialize(Player player1, Player player2, int maxWins)
    {
        _player1Color.color = player1.PlayerColor;
        _player2Color.color = player2.PlayerColor;

        _player1Color.enabled = true;
        _player2Color.enabled = false;
         
        _player1Name.text = player1.Name;
        _player2Name.text = player2.Name;

        _player1Score.text = player1.Score.ToString();
        _player2Score.text = player2.Score.ToString();

        _endGameButton.gameObject.SetActive(false);

        _titleText.text = _prefixTitle + maxWins + _suffixTitle;
    }

    public void SetPlayerIndication(Player player)
    {
        _player1Color.enabled = !_player1Color.enabled;
        _player2Color.enabled = !_player2Color.enabled;
    }

    public void UpdateScore(Player player)
    {
        if (player.Name == _player1Name.text)
           _player1Score.text = player.Score.ToString();
        else
            _player2Score.text = player.Score.ToString();
    }

    public void ShowButton(bool endOfSession, UnityAction action)
    {
        if (endOfSession)
        {
            _endGameButtonText.text = "Main Menu";
        }
        else
        {
            _endGameButtonText.text = "Next Game";
        }

        _endGameButton.onClick.RemoveAllListeners();
        _endGameButton.gameObject.SetActive(true);
        _endGameButton.onClick.AddListener(action);
    }
}
