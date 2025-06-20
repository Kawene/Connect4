using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SVImageControl : MonoBehaviour, IDragHandler, IPointerClickHandler
{

    [SerializeField]
    private Image _pickerImage;

    [SerializeField]
    private ColorPickerControl _cpc;

    private RectTransform _rectTransform, _pickerTransform;

    private void Awake()
    {
        _cpc = GetComponentInParent<ColorPickerControl>();
        _rectTransform = GetComponent<RectTransform>();

        _pickerTransform = _pickerImage.GetComponent<RectTransform>();
        _pickerTransform.position = new Vector2(-(_rectTransform.sizeDelta.x * 0.5f), -(_rectTransform.sizeDelta.y * 0.5f));
    }

    void OnEnable()
    {
        var pos = _cpc.GetSV();
        float x = (pos.Item1 - 0.5f) * _rectTransform.sizeDelta.x;
        float y = (pos.Item2 - 0.5f) * _rectTransform.sizeDelta.y;
        _pickerTransform.localPosition = new Vector3(x, y, 0);
        _pickerImage.color = Color.HSVToRGB(0, 0, 1 - y);
    }


    void UpdateColor(PointerEventData eventData)
    {
        Vector3 pos = _rectTransform.InverseTransformPoint(eventData.position);

        float deltaX = _rectTransform.sizeDelta.x * 0.5f;
        float deltaY = _rectTransform.sizeDelta.y * 0.5f;

        if (pos.x < -deltaX)
        {
            pos.x = -deltaX;
        }
        else if (pos.x > deltaX)
        {
            pos.x = deltaX;
        }

        if (pos.y < -deltaY)
        {
            pos.y = -deltaY;

        }
        else if (pos.y > deltaY)
        {
            pos.y = deltaY;
        }

        float x  = (pos.x + deltaX) / _rectTransform.sizeDelta.x;
        float y = (pos.y + deltaY) / _rectTransform.sizeDelta.y;

        _pickerTransform.localPosition = pos;
        _pickerImage.color = Color.HSVToRGB(0, 0, 1 - y);

        _cpc.SetSV(x, y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateColor(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateColor(eventData);
    }
}

