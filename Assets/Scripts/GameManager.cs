using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private int _currentPlayerIndex;

    private InputActions _inputAction;
    private InputAction _placeTokenAction, _selectColumnAction;

    [SerializeField]
    private Board _board;

    private void Awake()
    {
        _inputAction = new InputActions();
        _placeTokenAction = _inputAction.PlayerInput.PlaceToken;
        _selectColumnAction = _inputAction.PlayerInput.SelectColumn;
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
        _board.PlaceTokenInBoard();
    }
}
