using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHud : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _nameText;

    [SerializeField]
    private TMP_Text _levelText;

    [SerializeField]
    private HPBar _hpBar;

    public void SetData(Pokemon pokemon)
    {
        _nameText.text = pokemon.Base.Name;
        _levelText.text = "Lvl " + pokemon.Level;
        _hpBar.SetHP((float)pokemon.HP / pokemon.MaxHp);
    }

}
