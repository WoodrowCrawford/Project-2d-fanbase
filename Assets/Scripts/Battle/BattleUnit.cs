using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField]
    private PokemonBase _base;

    [SerializeField]
    private int _level;

    [SerializeField]
    private bool _isPlayerUnit;

    public Pokemon Pokemon { get; set; }

    //Create a pokemon from the base and level
    public void SetUp()
    {
        Pokemon = new Pokemon(_base, _level);

        if (_isPlayerUnit)
        {
            GetComponent<Image>().sprite = Pokemon.Base.Backsprite;
        }
        else
        {
            GetComponent<Image>().sprite = Pokemon.Base.FrontSprite;
        }
    }
}
