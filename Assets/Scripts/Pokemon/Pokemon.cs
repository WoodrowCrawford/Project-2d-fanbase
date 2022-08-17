using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    public PokemonBase Base { get; set; }
    public int Level { get; set; }

public int HP { get; set; }

    public List<Move> Moves { get; set; }

    //Creates the pokemon
    public Pokemon(PokemonBase pBase, int pLevel)
    {
        Base = pBase;
        Level = pLevel;
        HP = MaxHp;

        //Gives moves to the pokemon
        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves)
        {
            //Add a move if the pokemon level is high enough
            if (move.Level <= Level)
            {
                Moves.Add(new Move(move.Base));
            }

            //If the pokemon has 4 moves then break
            if( Moves.Count >= 4)
            {
                break;
            }
        }
    }


    //formula used for attack 
    public int Attack
    {    
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
    }

    //formula used for defense
    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; }
    }

    //formula used for SpAttack
    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; }
    }

    //formula used for SpDefense
    public int SpDefense
    {
        get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5; }
    }

    //formula used for speed
    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; }
    }

    public int MaxHp
    {
        get { return Mathf.FloorToInt((Base.Speed + Level) / 100f) + 10; }
    }


    //The formula for taking damage
    public bool TakeDamage(Move move, Pokemon attacker)
    {
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a + move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            return true;
        }

        return false;
    }

    //Gets a random move for the cpu pokemon to use
    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }
}
