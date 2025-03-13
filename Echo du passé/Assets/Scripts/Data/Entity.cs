using Unity.VisualScripting;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected EntityData _entityData;

    protected string _entityName = "...";

    protected Sprite _sprite;

    protected JobData _job;
    protected RaceData _raceData;

    protected int _level;

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

    public bool IsAlive { get { return _currentHP > 0; } }

    /// <summary>
    /// Function to update the entity stats upon change.
    /// </summary>
    public void UpdateStats()
    {
        _strength = _raceData.RaceStrength + _entityData.Strenth;
        _dexterity = _raceData.RaceDexterity + _entityData.Dexterity;
        _intelligence = _raceData.RaceIntelligence + _entityData.Intelligence;
        _vitality = _raceData.RaceVitality + _entityData.Vitality;
        _luck = _raceData.RaceLuck + _entityData.Luck;

        _maxHP = _vitality * _level;
    }

    /// <summary>
    /// Function to add or remove damage from an Entity.
    /// </summary>
    /// <param name="amount">Represent the amount of damage taken. Amount will be negative if healing is given.</param>
    public void TakeDamage(int amount)
    {
        _currentHP -= amount;
        Debug.Log(gameObject.name + " has " + _currentHP + " left.");
    }
}
