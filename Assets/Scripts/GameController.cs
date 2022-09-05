using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { FreeRoam, Battle }

public class GameController : MonoBehaviour
{

    [SerializeField]
     PlayerMovementBehavior playerMovement;

    [SerializeField]
    BattleSystem battleSystem;

    [SerializeField]
    Camera worldCamera;

    GameState state;


    private void Start()
    {
        playerMovement.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerMovement.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
    }

    //Starts the battle event
    void StartBattle()
    {
        state = GameState.Battle;
        playerMovement.rb.gameObject.SetActive(false);
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        battleSystem.StartBattle();

    }

    //Ends the battle event
    void EndBattle(bool won)
    {
        state = GameState.FreeRoam;
        playerMovement.rb.gameObject.SetActive(true);
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }
}
