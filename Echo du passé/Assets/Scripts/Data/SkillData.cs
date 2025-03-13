using System;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    [SerializeField]
    private string _skillName = "...";

    [SerializeField]
    [Range(0, 5)]
    private int _skillSlot = 0;
    [SerializeField]
    private string _description = "...";
    [SerializeField]
    private Element _element = Element.None;
    [SerializeField]
    private Focus _focus = Focus.None;

    [SerializeField]
    [Range(0, 100)]
    private int _damage = 0;
    [SerializeField]
    [Range(1, 10)]
    private int _accuracy;
    [SerializeField]
    private CurseData[] _curses;

    [SerializeField]
    [Range(0, 100)]
    private int _healing = 0;
    [SerializeField]
    private BuffData[] _buffs;

    public string SkillName => _skillName;
    public int SkillSlot => _skillSlot;
    public string Description => _description;
    public Element Element => _element;
    public Focus Focus => _focus;
    public int Damage => _damage;
    public int Accuracy => _accuracy;
    public CurseData[] Curses => _curses;
    public int Healing => _healing;
    public BuffData[] Buffs => _buffs;

}
