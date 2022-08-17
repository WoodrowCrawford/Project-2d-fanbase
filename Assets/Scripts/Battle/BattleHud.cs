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

    Pokemon _pokemon;

    public void SetData(Pokemon pokemon)
    {
        _pokemon = pokemon;
        _nameText.text = pokemon.Base.Name;
        _levelText.text = "Lvl " + pokemon.Level;
        _hpBar.SetHP((float)pokemon.HP / pokemon.MaxHp);
    }

    public IEnumerator UpdateHP()
    {
        yield return _hpBar.SetHPSmooth((float)_pokemon.HP / _pokemon.MaxHp);
    }

}
