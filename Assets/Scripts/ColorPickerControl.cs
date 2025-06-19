using UnityEngine;
using UnityEngine.UI;


public class ColorPickerControl : MonoBehaviour
{
    private float _currentHue, _currentSat, _currentVal;

    [SerializeField]
    private RawImage _hueImage, _satValImage, _outputImage;

    [SerializeField]
    private Slider _hueSlider;

    private Texture2D _hueTexture, _svTexture, _outputTexture;

    private Player _playerToModify;

    public void OpenDialog(Player player)
    {
        _playerToModify = player;

        Color.RGBToHSV(player.PlayerColor, out _currentHue, out _currentSat, out _currentVal);

        gameObject.SetActive(true);

        CreateHueImage();
        CreateSvImage();
        CreateOutputImage();

        UpdateOutputImage();
    }


    private void CreateHueImage()
    {
        _hueTexture = new Texture2D(1, 16);
        _hueTexture.wrapMode = TextureWrapMode.Clamp;
        _hueTexture.name = "HueTexture";

        for (int i = 0; i < _hueTexture.height; i++)
        {
            _hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / _hueTexture.height , 1, 1f));
        }

        _hueTexture.Apply();

        _hueImage.texture = _hueTexture;
    }

    private void CreateSvImage()
    {
        _svTexture = new Texture2D(16, 16);
        _svTexture.wrapMode = TextureWrapMode.Clamp;
        _svTexture.name = "SatValTexture";

        for (int y = 0; y < _svTexture.height; ++y)
        {
            for (int x = 0; x < _svTexture.width; ++x)
            {
                float saturation = (float)x / _svTexture.width ;
                float value = (float)y / _svTexture.height ;
                _svTexture.SetPixel(x, y, Color.HSVToRGB(_currentHue, saturation, value));
            }
        }
        _svTexture.Apply();

        _satValImage.texture = _svTexture;
    }

    private void CreateOutputImage()
    {
        _outputTexture = new Texture2D(1, 16);
        _outputTexture.wrapMode = TextureWrapMode.Clamp;
        _outputTexture.name = "OutputTexture";

        Color currentColor = Color.HSVToRGB(_currentHue, _currentSat, _currentVal);

        for (int i = 0; i < _outputTexture.height; ++i)
        {
            _outputTexture.SetPixel(0, i, currentColor);
        }

        _outputTexture.Apply();
        _outputImage.texture = _outputTexture;
    }

    private void UpdateOutputImage()
    {
        Color currentColor = Color.HSVToRGB(_currentHue, _currentSat, _currentVal);

        for (int i = 0; i < _outputTexture.height; ++i)
        {
            _outputTexture.SetPixel(0, i, currentColor);
        }

        _outputTexture.Apply();

        _playerToModify.SetColor(currentColor);
    }

    public void SetSV(float s, float v)
    {
        _currentSat = s;
        _currentVal = v;

        UpdateOutputImage();
    }

    public void UpdateSVImage()
    {
        _currentHue = _hueSlider.value;

        for (int y = 0; y < _svTexture.height; ++y)
        {
            for (int x = 0; x < _svTexture.width; ++x)
            {
                _svTexture.SetPixel(x, y, 
                    Color.HSVToRGB(_currentHue, 
                        Mathf.InverseLerp(0, _svTexture.width, x), 
                        Mathf.InverseLerp(0, _svTexture.width, y)));
            }
        }
        _svTexture.Apply();

        UpdateOutputImage();
    }  
    
    public (float, float) GetSV()
    {
        return (_currentSat, _currentVal);
    }

    public void ChangeColor()
    {
        gameObject.SetActive(false);
        _playerToModify.SetColor(Color.HSVToRGB(_currentHue, _currentSat, _currentVal));
    }
}
