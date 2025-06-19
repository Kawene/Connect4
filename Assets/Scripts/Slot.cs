using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _selected;

    private Player _playerOwner;

    public Player PlayerOwner => _playerOwner;

    public void ClearSlot()
    {
        _selected.enabled = false;
        _playerOwner = null;
    }

    public void ChangeColor(Player player)
    {
        _selected.enabled = true;

        _playerOwner = player;

        _selected.material.color = player.PlayerColor;
    }
}
