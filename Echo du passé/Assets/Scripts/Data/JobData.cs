using System;
using UnityEngine;

[CreateAssetMenu(fileName = "JobData", menuName = "Scriptable Objects/Job")]
public class JobData : ScriptableObject
{
    [SerializeField]
    public string jobName = "...";

    [SerializeField] 
    private SkillData[] _skillList;
    [SerializeField] 
    private Archetype _archetype = Archetype.None;

    public Archetype Archetype => _archetype;

}

