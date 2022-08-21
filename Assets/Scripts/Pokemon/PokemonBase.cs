using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon")]
public class PokemonBase : ScriptableObject
{
    [SerializeField]
    private string name;

    [SerializeField]
    [TextArea]
    private string description;

    [SerializeField]
    Sprite frontSprite;

    [SerializeField]
    Sprite backSprite;

    [SerializeField]
    PokemonType type1;

    [SerializeField]
    PokemonType type2;


    //Base Stats
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField]
    List<LearnableMove> _learnableMoves;

    
    public PokemonType Type1
    {
        get { return type1; }
    }

    public PokemonType Type2
    {
        get { return type2; }
    }

    public Sprite Backsprite
    {
        get { return backSprite; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }


    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }


    public int SpDefense
    {
        get { return spDefense; }
    }


    public int Speed
    {
        get { return speed; }
    }


    public List<LearnableMove> LearnableMoves
    {
        get { return _learnableMoves; }
    }


    [System.Serializable]
    public class LearnableMove
    {
        [SerializeField]
        private MoveBase _moveBase;

        [SerializeField]
        private int _level;


        public MoveBase Base
        {
            get { return _moveBase; }
        }

        public int Level
        {
            get { return _level; }
        }
    }


    //Pokemon types
    public enum PokemonType
    {
        None,
        Normal,
        Fire,
        Water,
        Electric,
        Grass,
        Ice,
        Fighting,
        Poison,
        Ground,
        Flying,
        Psychic,
        Bug,
        Rock,
        Ghost,
        Dragon,
        Dark,
        Steel,
        Fairy
    }

    public class TypeChart
    {
        static float[][] chart =
        {  //                          NOR   FIR  WAT  ELE  GRA  ICE  FIG  POI  GRO  FLY  PSY  BUG  ROC  GHO  DRA  DAR  STE  FAI
          /*Nor*/        new float [] { 1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  .5f, 0f,  1f,  1f,  .5f,  1f },
          /*Fir*/        new float [] { 1f, .5f, .5f,  1f,  2f,  2f,  1f,  1f,  1f,  1f,  1f,  2f,  .5f, 1f, .5f,  1f,   2f,  1f },
          /*Wat*/        new float [] { 1f,  2f, .5f,  1f, .5f,  1f,  1f,  1f,  2f,  1f,  1f,  1f,   2f, 1f, .5f,  1f,   1f,  1f },
          /*Elec*/       new float [] { 1f,  1f,  2f, .5f, .5f,  1f,  1f,  1f,  0f,  2f,  1f,  1f,   1f, 1f, .5f,  1f,   1f,  1f },
          /*Grass*/      new float [] { 1f, .5f,  2f,  1f, .5f,  1f,  1f, .5f,  2f, .5f,  1f, .5f,   2f, 1f, .5f,  1f,  .5f,  1f },
          /*Ice*/        new float [] { 1f, .5f, .5f,  1f,  2f, .5f,  1f,  1f,  2f,  2f,  1f,  1f,   1f, 1f,  2f,  1f,  .5f,  1f },
          /*Fight*/      new float [] { 2f,  1f,  1f,  1f,  1f,  2f,  1f, .5f,  1f, .5f, .5f, .5f,   2f, 0f,  1f,  2f,   2f, .5f },
          /*Poison*/     new float [] { 1f,  1f,  1f,  1f,  2f,  1f,  1f, .5f, .5f,  1f,  1f,  1f,  .5f,.5f,  1f,  1f,   0f,  2f },
          /*Ground*/     new float [] { 1f,  1f,  1f,  2f, .5f,  1f,  1f,  2f,  1f,  0f,  1f, .5f,   2f, 1f,  1f,  1f,   2f,  1f },
          /*Flying*/     new float [] { 1f,  1f,  1f, .5f,  2f,  1f,  2f,  1f,  1f,  1f,  1f,  2f,  .5f, 1f,  1f,  1f,  .5f,  1f },
          /*Psychic*/    new float [] { 1f,  1f,  1f,  1f,  1f,  1f,  2f,  2f,  1f,  1f, .5f,  1f,   1f, 1f,  1f,  0f,  .5f,  1f },
          /*Bug*/        new float [] { 1f, .5f,  1f,  1f,  2f,  1f, .5f, .5f,  1f, .5f,  2f,  1f,   1f,.5f,  1f,  2f,  .5f, .5f },
          /*Rock*/       new float [] { 1f,  2f,  1f,  1f,  1f,  2f, .5f,  1f, .5f,  2f,  1f,  2f,   1f, 1f,  1f,  1f,  .5f,  1f },
          /*Ghost*/      new float [] { 0f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  2f,  1f,   1f, 2f,  1f, .5f,   1f,  1f },
          /*Dragon*/     new float [] { 1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,   1f, 1f,  2f,  1f,  .5f,  0f },
          /*Dark*/       new float [] { 1f,  1f,  1f,  1f,  1f,  1f, .5f,  1f,  1f,  1f, .5f,  1f,   1f, 2f,  1f, .5f,   1f, .5f },
          /*Steel*/      new float [] { 1f, .5f, .5f, .5f,  1f,  2f,  1f,  1f,  1f,  1f,  1f,  1f,   2f, 1f,  1f,  1f,  .5f,  2f },
          /*Fairy*/      new float [] { 1f, .5f,  1f,  1f,  1f,  1f,  2f, .5f,  1f,  1f,  1f,  1f,   1f, 1f,  2f,  2f,  .5f,  1f },

        };

        public static float GetEffectiveness(PokemonType attackType, PokemonType defenseType)
        {
            if (attackType == PokemonType.None || defenseType == PokemonType.None)
            {
                return 1;
            }

            int row = (int)attackType - 1;
            int col = (int)defenseType - 1;

            return chart[row][col];
        }
    }
}



