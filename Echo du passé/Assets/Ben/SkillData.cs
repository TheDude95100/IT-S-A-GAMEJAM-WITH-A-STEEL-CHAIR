using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName = "...";

    [Header("General stats")]

    [SerializeField]
    [Range(0, 5)]
    private int _skillSlot = 0;
    [SerializeField]
    private string description = "...";
    [SerializeField]
    private Element _element = Element.None;
    [SerializeField]
    private Focus _focus = Focus.None;


    [Header("Damage stats")]

    [SerializeField]
    [Range(0, 100)]
    private int _damage = 0;
    [SerializeField]
    [Range(1, 10)]
    private int _accuracy;
    [SerializeField]
    private CurseData[] _curses;

    [Header("Support stats")]

    [SerializeField]
    [Range(0, 100)]
    private int _healing = 0;
    [SerializeField]
    private BuffData[] _buffs;
}
