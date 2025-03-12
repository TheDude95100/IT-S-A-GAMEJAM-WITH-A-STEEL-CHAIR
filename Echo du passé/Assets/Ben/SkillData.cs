using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill")]
public class SkillData : ScriptableObject
{
    public string entityName;

    [SerializeField] private int dexterity;
    [SerializeField] private int intelligence;
    [SerializeField] private int vitality;
    [SerializeField] private int luck;

    [SerializeField] private JobData job;
}
