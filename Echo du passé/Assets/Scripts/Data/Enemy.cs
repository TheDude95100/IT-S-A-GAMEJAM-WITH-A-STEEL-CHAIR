using UnityEngine;

public class Enemy : Entity
{
    private int _xpValue;

    public int XpValue => _xpValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _xpValue = Level;
    }
}
