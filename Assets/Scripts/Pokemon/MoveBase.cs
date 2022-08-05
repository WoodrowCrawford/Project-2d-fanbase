using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Move", menuName = "Pokemon/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField]
    private string _name;

    [SerializeField] [TextArea]
    private string _description;

    [SerializeField]
    private PokemonBase.PokemonType _type;

    [SerializeField]
    private int _power;

    [SerializeField]
    private int _accuracy;

    [SerializeField]
    private int _pp;

    
    public string Name
    {
        get { return _name; }
    }

    public string Description
    {
        get { return _description; }
    }

    public PokemonBase.PokemonType Type
    {
        get { return _type; }
    }

    public int Power
    {
        get { return _power; }
    }

    public int Accuracy
    {
        get { return _accuracy; }
    }

    public int PP
    {
        get { return _pp; }
    }



}
