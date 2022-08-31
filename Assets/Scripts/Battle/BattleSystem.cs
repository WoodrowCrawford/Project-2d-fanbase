using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy}

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


    private BattleState _state;

    private int _currentAction;
    private int _currentMove;

    private void Start()
    {
        StartCoroutine(SetUpBattle());
    }


    private void Update()
    {
        //Handles the action selection if the battle state is player action
        if (_state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if ( _state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    public IEnumerator SetUpBattle()
    {
        //sets up data for the player
        _playerUnit.SetUp();
        _playerHud.SetData(_playerUnit.Pokemon);

        //sets up data for the enemy
        _enemyUnit.SetUp();
        _enemyHud.SetData(_enemyUnit.Pokemon);

        //Shows the dialogue 
        yield return _dialogueBox.TypeDialoge($"A wild {_enemyUnit.Pokemon.Base.Name} appeared!");

        //The player action 
        PlayerAction();

    }


    //Beginning of the player turn
    public void PlayerAction()
    {
        _state = BattleState.PlayerAction;
        StartCoroutine(_dialogueBox.TypeDialoge($"What will {_playerUnit.Pokemon.Base.Name} do?"));
        _dialogueBox.EnableActionSelector(true);
    }

    //The players turn
    public void PlayerMove()
    {
        _state = BattleState.PlayerMove;
        _dialogueBox.EnableActionSelector(false);
        _dialogueBox.EnableDialogueText(false);
        _dialogueBox.EnableMoveSelector(true);
        
        _dialogueBox.SetMoveNames(_playerUnit.Pokemon.Moves);
    }

    //Performs the player move
    IEnumerator PerformPlayerMove()
    {
        _state = BattleState.Busy;
        
        var move = _playerUnit.Pokemon.Moves[_currentMove];
        yield return _dialogueBox.TypeDialoge($"{_playerUnit.Pokemon.Base.Name} used {move.Base.Name}");
       


        var damageDetails = _enemyUnit.Pokemon.TakeDamage(move, _playerUnit.Pokemon);
        yield return _enemyHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if(damageDetails.Fainted)
        {
            yield return _dialogueBox.TypeDialoge($"{_enemyUnit.Pokemon.Base.Name} fainted");
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }


    //The enemys turn
    IEnumerator EnemyMove()
    {
        _state = BattleState.EnemyMove;

        var move = _enemyUnit.Pokemon.GetRandomMove();

        yield return _dialogueBox.TypeDialoge($"{_enemyUnit.Pokemon.Base.Name} used {move.Base.Name}");



        var damageDetails = _playerUnit.Pokemon.TakeDamage(move, _playerUnit.Pokemon);
        yield return _playerHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);


        if (damageDetails.Fainted)
        {
            yield return _dialogueBox.TypeDialoge($"{_playerUnit.Pokemon.Base.Name} fainted");
        }
        else
        {
            PlayerAction();
        }
    }

    //Shows the damage details in the dialogue box
    IEnumerator ShowDamageDetails(Pokemon.DamageDetails damageDetails)
    {
        //Shows crit text
        if (damageDetails.Critical > 1f)
        {
            yield return _dialogueBox.TypeDialoge("A critical hit!");
        }

        //Shows super effective text
        if (damageDetails.TypeEffectiveness > 1f)
        {
            yield return _dialogueBox.TypeDialoge("Its super effective!");
        }
        else if (damageDetails.TypeEffectiveness < 1f)
        {
            yield return _dialogueBox.TypeDialoge("It's not very effective...");
        }

    }

    //How the player can move to select different selections in the menu
    public void HandleActionSelection()
    {
        //Used for keyboard controls
        if(Keyboard.current.downArrowKey.IsPressed())
        {
            if(_currentAction < 1)
            {
                ++_currentAction;
            }
        }
        else if (Keyboard.current.upArrowKey.IsPressed())
        {
            if(_currentAction > 0)
            {
                --_currentAction;
            }
        }

        _dialogueBox.UpdateActionSelection(_currentAction);

        if(Keyboard.current.zKey.IsPressed())
        {
            if (_currentAction == 0)
            {
                //Fight
                PlayerMove();
                
            }
            else if (_currentAction == 1)
            {
                //Run
            }
        }
    }


    public void HandleMoveSelection()
    {
        //Used for keyboard controls
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            if (_currentMove < _playerUnit.Pokemon.Moves.Count - 1)
            {
                ++_currentMove;
            }
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            if (_currentMove > 0)
            {
                --_currentMove;
            }
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            if (_currentMove < _playerUnit.Pokemon.Moves.Count - 2)
            {
                _currentMove += 2;
            }
        }
        else if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            if (_currentMove > 1)
            {
                _currentMove -= 2;
            }
        }

        _dialogueBox.UpdateMoveSelection(_currentMove, _playerUnit.Pokemon.Moves[_currentMove]);

        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            _dialogueBox.EnableMoveSelector(false);
            _dialogueBox.EnableDialogueText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }
}
