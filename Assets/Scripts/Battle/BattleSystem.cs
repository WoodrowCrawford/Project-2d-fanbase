using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    private BattleUnit _playerUnit;

    [SerializeField]
    private BattleUnit _enemyUnit;

    [SerializeField]
    private BattleHud _playerHud;

    [SerializeField]
    private BattleHud _enemyHud;

    [SerializeField]
    private BattleDialogueBox _dialogueBox;

    private void Start()
    {
        SetUpBattle();
    }

    public void SetUpBattle()
    {
        //sets up data for the player
        _playerUnit.SetUp();
        _playerHud.SetData(_playerUnit.Pokemon);

        //sets up data for the enemy
        _enemyUnit.SetUp();
        _enemyHud.SetData(_enemyUnit.Pokemon);

        StartCoroutine(_dialogueBox.TypeDialoge($"A wild {_enemyUnit.Pokemon.Base.Name} appeared!"));
    }
}
