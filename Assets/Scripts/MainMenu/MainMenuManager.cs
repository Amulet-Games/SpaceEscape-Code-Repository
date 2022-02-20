using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Rect Transform.")]
    public RectTransform _titleRect;
    public RectTransform _startGameRect;

    [Header("Button.")]
    public Button _startGameButton;

    [Header("Enlarge Tween.")]
    public LeanTweenType _enlargeEaseType;
    public Vector3 _enlargeTargetScale;
    public float _enlargeSpeed;

    [Header("Move Up Tween.")]
    public LeanTweenType _moveUpEaseType;
    public Vector3 _moveUpTargetPos;
    public float _moveUpSpeed;

    [Header("Start Game Tween.")]
    public LeanTweenType _startGameEnlargeEaseType;
    public Vector3 _startGameEnlargeScale;
    public float _startGameEnlargeSpeed;

    private void Awake()
    {
        EnLargeTitleText();
    }

    void EnLargeTitleText()
    {
        _titleRect.LeanScale(_enlargeTargetScale, _enlargeSpeed).setEase(_enlargeEaseType).setOnComplete(MoveUpTitleText);
    }

    void MoveUpTitleText()
    {
        _titleRect.LeanMove(_moveUpTargetPos, _moveUpSpeed).setEase(_moveUpEaseType).setOnComplete(EnlargetStartGameText);
    }

    void EnlargetStartGameText()
    {
        _startGameRect.LeanScale(_startGameEnlargeScale, _startGameEnlargeSpeed).setEase(_startGameEnlargeEaseType).setOnComplete(OnCompleteEnableStartGameButton);
    }

    void OnCompleteEnableStartGameButton()
    {
        _startGameButton.enabled = true;
    }
}
