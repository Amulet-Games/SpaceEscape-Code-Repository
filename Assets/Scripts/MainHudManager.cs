using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHudManager : MonoBehaviour
{
    [Header("Canvas Group.")]
    public CanvasGroup _mainHudCanvasGroup;
    public RectTransform _loseMessage;
    public RectTransform _winMessage;

    [Header("Slider.")]
    public Slider _oxygenSlider;
    public float sliderWidthMulti = 6f;
    public RectTransform _oxygenFillRect;

    [Header("Colors.")]
    public float _colorLerpSpeed;
    public Color _normalOxygenColor;
    public Color _lowOnOxygenColor;

    [Header("Rect Refs.")]
    public RectTransform _oxygenSliderRect;

    [Header("Canvas Refs.")]
    public Canvas _mainHudCanvas;

    [Header("Status.")]
    public bool _isColorChanged;

    [Header("Refs.")]
    public InputManager _inp;
    public StateManager _states;

    public static MainHudManager singleton;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Tick.
    public void Tick()
    {
        UpdateOxygenSliderValue();
        UpdateOxygenSliderColor();
    }

    void UpdateOxygenSliderValue()
    {
        _oxygenSlider.value = _states._curOxygenAmount;
    }

    void UpdateOxygenSliderColor()
    {
        if (_oxygenSlider.value <= _states._lowOxygenThershold)
        {
            SetIsColorChangedStatus(true);
        }
        else
        {
            SetIsColorChangedStatus(false);
        }
    }
    #endregion

    #region Refresh.
    void RefreshOxygenMaxAmount()
    {
        _oxygenSlider.maxValue = _states._totalOxygenAmonut;
        _oxygenSlider.value = _states._totalOxygenAmonut;
    }

    public void RefreshSliderWidthByType()
    {
        float _curMaxOxygenAmonut = (int)_states._totalOxygenAmonut;
        _oxygenSlider.maxValue = _curMaxOxygenAmonut;
        _oxygenSliderRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _oxygenSlider.maxValue * sliderWidthMulti);
    }
    #endregion

    void SetIsColorChangedStatus(bool _isColorChanged)
    {
        if (_isColorChanged)
        {
            if (!this._isColorChanged)
            {
                this._isColorChanged = true;
                LeanTween.color(_oxygenFillRect, _lowOnOxygenColor, _colorLerpSpeed);
            }
        }
        else
        {
            if (this._isColorChanged)
            {
                this._isColorChanged = false;
                LeanTween.color(_oxygenFillRect, _normalOxygenColor, _colorLerpSpeed);
            }
        }
    }

    #region Hide / Show MainHud
    void ShowMainHud()
    {
        EnableCanvas();
        LeanTween.alphaCanvas(_mainHudCanvasGroup, 1, 0.25f);
    }

    void HideMainHud()
    {
        LeanTween.alphaCanvas(_mainHudCanvasGroup, 0, 0.25f).setOnComplete(DisableCanvas);
    }

    void EnableCanvas()
    {
        _mainHudCanvas.enabled = true;
    }

    void DisableCanvas()
    {
        _mainHudCanvas.enabled = false;
    }

    public void ShowLoseMessage()
    {
        LeanTween.alpha(_loseMessage, 1, 1);
    }

    public void ShowWinMessage()
    {
        LeanTween.alpha(_winMessage, 1, 1);
    }
    #endregion

    #region Setup.
    #endregion

    #region Init.
    public void Init(InputManager _inp)
    {
        this._inp = _inp;

        InitMainHudCanvas();
        InitRefs();
        RefreshOxygenMaxAmount();
        RefreshSliderWidthByType();
    }

    void InitMainHudCanvas()
    {
        _mainHudCanvas = GetComponent<Canvas>();
        ShowMainHud();
    }

    void InitRefs()
    {
        _states = _inp._states;
        _oxygenSliderRect = _oxygenSlider.GetComponent<RectTransform>();
    }
    #endregion
}
