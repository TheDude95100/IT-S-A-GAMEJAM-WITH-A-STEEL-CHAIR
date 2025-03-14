using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public abstract class Entity : MonoBehaviour
{
    #region Variables
    [SerializeField]
    protected EntityData _entityData;

    protected string _entityName = "...";

    protected GameObject _entityHead;

    protected JobData _job;

    protected int _level;

    protected int _initiative;

    protected int _strength;
    protected int _dexterity;
    protected int _intelligence;
    protected int _vitality;
    protected int _luck;

    protected float _currentHP;
    protected float _maxHP;

    protected float _attack;
    protected float _defense;
    protected float _magicAttack;
    protected float _magicDefense;

    protected Dice _d20;
    #endregion

    #region Properties
    public string EntityName
    {
        get
        {
            return _entityName;
        }
        set
        {
            _entityName = value;
        }
    }

    public GameObject EntityHead => _entityHead;

    public JobData Job => _job;

    public int Level => _level;

    public int Initiative => _initiative;

    public int Strength => _strength;
    public int Dexterity => _dexterity;
    public int Intelligence => _intelligence;
    public int Vitality => _vitality;
    public int Luck => _luck;

    public float CurrentHP => _currentHP;
    public float MaxHP => _maxHP;

    public float Attack => _attack;
    public float Defense => _defense;
    public float MagicAttack => _magicAttack;
    public float MagicDefense => _magicDefense;

    public bool IsAlive { get { return _currentHP > 0; } }
    #endregion

    protected void Awake()
    {
        _d20 = new Dice(Dice.DiceType.D20);
        _level = _entityData.Level;
        _entityName = _entityData.Name;

        UpdateStats();

        _currentHP = _maxHP;
    }

    /// <summary>
    /// Roll the dices and do the math for the to hit of the character.
    /// </summary>
    /// <returns>Return the to hit value.</returns>
    public int RollAttack()
    {
        return this._d20.ThrowDice();
    }

    /// <summary>
    /// Roll the dices and do the math for the damage of the character.
    /// </summary>
    /// <returns></returns>
    public float DamageGeneration(int indexSkillUsed)
    {
        float damage = 0;
        
        if(indexSkillUsed != -1)
        {
            //Add skill damage here
        }

        damage += this.Attack;

        return damage;
    }

    /// <summary>
    /// Function to update the entity stats upon change.
    /// </summary>
    public void UpdateStats()
    {
        _strength = _entityData.RaceData.RaceStrength + _entityData.Strenth;
        _dexterity = _entityData.RaceData.RaceDexterity + _entityData.Dexterity;
        _intelligence = _entityData.RaceData.RaceIntelligence + _entityData.Intelligence;
        _vitality = _entityData.RaceData.RaceVitality + _entityData.Vitality;
        _luck = _entityData.RaceData.RaceLuck + _entityData.Luck;

        _maxHP = _vitality * _level;
        Debug.Log(_entityName + " " + _level);
    }

    /// <summary>
    /// Function to add or remove damage from an Entity.
    /// </summary>
    /// <param name="amount">Represent the amount of damage taken. Amount will be negative if healing is given.</param>
    public void TakeDamage(int amount)
    {
        if (this.CurrentHP - amount >= this.MaxHP)
        {
            _currentHP = this.MaxHP;
        }
        else if(this.CurrentHP - amount >= 0)
        { 
            _currentHP -= amount;
        }
        else
        {
            _currentHP = 0;
        }

        Debug.Log(gameObject.name + " has " + _currentHP + " left.");
    }

    /// <summary>
    /// Roll a dice to determine the initiative.
    /// </summary>
    public void RollInitiative()
    {
        _initiative = _d20.ThrowDice();
    }
}
