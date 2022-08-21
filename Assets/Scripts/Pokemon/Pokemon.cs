using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PokemonBase;

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
    public DamageDetails TakeDamage(Move move, Pokemon attacker)
    {
        //Determinds the crit hit chance for attacks
        float critical = 1f;
        if (Random.value * 100f <= 6.25f)
        {
            critical = 2f;
        }

        //Gets the effectiveness for the move used
        float type = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1) * TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2);

        var damageDetails = new DamageDetails()
        {
            TypeEffectiveness = type,
            Critical = critical,
            Fainted = false

        };

        //Modifiers used for the damage values
        float modifiers = Random.Range(0.85f, 1f) * type * critical;
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a + move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            damageDetails.Fainted = true;
        }

        return damageDetails;
    }

    //Gets a random move for the cpu pokemon to use
    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }


    public class DamageDetails
    {
        public bool Fainted { get; set; }

        public float Critical { get; set; }

        public float TypeEffectiveness { get; set; }
    }
}
