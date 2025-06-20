using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private GameObject _selectedObject;

    private MeshRenderer _selected;

    private Player _playerOwner;

    public Player PlayerOwner => _playerOwner;

    private bool _winningSlot;

    private float _highlightTime = 0f;

    private Vector3 _originalScale;

    private Color _baseColor;

    private void Start()
    {
        _selected = _selectedObject.GetComponent<MeshRenderer>();
        enabled = false;
        _originalScale = _selectedObject.transform.localScale;
    }

    private void Update()
    {
        if (_winningSlot)
        {
            _highlightTime += Time.deltaTime * 4f;

            float scale = (1f + Mathf.Abs(Mathf.Sin(_highlightTime))) * 0.5f;
            _selectedObject.transform.localScale = _originalScale * scale;

            Color.RGBToHSV(_baseColor, out float h, out float s, out float v);
            float highlightV = Mathf.Clamp01(v + 0.5f * Mathf.Sin(_highlightTime) * 0.5f);
            Color highlightColor = Color.HSVToRGB(h, s, highlightV);
            _selected.material.color = highlightColor;
        }
    }

    public void ClearSlot()
    {
        _selected.enabled = false;
        _playerOwner = null;
        enabled = false;
        _selectedObject.transform.localScale = _originalScale;
        _highlightTime = 0f;
    }

    public void ChangeColor(Player player)
    {
        _selected.enabled = true;

        _playerOwner = player;

        _selected.material.color = player.PlayerColor;

        _baseColor = player.PlayerColor;
    }

    public void SetWinningSlot()
    {
        _winningSlot = true;
    }

    public void RemoveWinningSlot()
    {
        _winningSlot = false;
    }

    public void HighlightWinningSlot()
    {
        enabled = true;
    }
}
