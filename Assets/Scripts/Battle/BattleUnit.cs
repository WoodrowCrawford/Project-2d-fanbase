using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField]
    private PokemonBase _base;

    [SerializeField]
    private int _level;

    [SerializeField]
    private bool _isPlayerUnit;

    public Pokemon Pokemon { get; set; }

    Image image;
    Vector3 originalPos;
    Color originalColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    //Create a pokemon from the base and level
    public void SetUp()
    {
        Pokemon = new Pokemon(_base, _level);

        if (_isPlayerUnit)
        {
            image.sprite = Pokemon.Base.Backsprite;
        }
        else
        {
            image.sprite = Pokemon.Base.FrontSprite;
        }

        image.color = originalColor;
        PlayEnterAnimation();
    }

    //Plays the enter animation
    public void PlayEnterAnimation()
    {
        if (_isPlayerUnit)
        {
            image.rectTransform.localPosition = new Vector3(-1500f, originalPos.y);
        }
        else
        {
            image.rectTransform.localPosition = new Vector3(1500f, originalPos.y);
        }

        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }


    //Plays the attack animation 
    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        if (_isPlayerUnit)
        {
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f));

        }
        else
        {
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f));
        }

        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }


    //Plays the hit animation
    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.gray, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }


    public void PlayFaintAnimation()
    {
        var sequence = DOTween.Sequence();

        //Moves the image down while also fading it with the Join function
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 150f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }
}
