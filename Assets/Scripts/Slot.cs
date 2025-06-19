using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _selected;

    public void ChangeColor()
    {
        _selected.enabled = true;

        //Get player color then assign it
    }
}
