using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _player1Name;

    [SerializeField]
    private Image _player1Color;

    [SerializeField]
    private TextMeshProUGUI _player2Name;

    [SerializeField]
    private Image _player2Color;

    public void Initialize(Player player1, Player player2)
    {
        _player1Color.color = player1.PlayerColor;
        _player2Color.color = player2.PlayerColor;

        _player1Color.enabled = true;
        _player2Color.enabled = false;

        _player1Name.text = player1.Name;
        _player2Name.text = player2.Name;
    }

    public void SetPlayerIndication(Player player)
    {
        _player1Color.enabled = !_player1Color.enabled;
        _player2Color.enabled = !_player2Color.enabled;
    }
}
