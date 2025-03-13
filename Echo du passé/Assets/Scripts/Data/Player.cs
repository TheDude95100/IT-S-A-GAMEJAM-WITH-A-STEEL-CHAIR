using UnityEngine;

public class Player : Entity
{
    private int _currentXP;
    private int _nextLevel;

    private void Start()
    {
        _entityName = _entityData.Name;

        UpdateStats();

        _currentHP = _maxHP;
    }
}
