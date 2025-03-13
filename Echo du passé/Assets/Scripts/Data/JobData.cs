using System;
using UnityEngine;

[CreateAssetMenu(fileName = "JobData", menuName = "Dataset/Job")]
public class JobData : ScriptableObject
{
    [SerializeField]
    private string _jobName = "...";

    [SerializeField] 
    private SkillData[] _skillList;
    [SerializeField] 
    private Archetype _archetype = Archetype.None;

    public string JobName => _jobName;
    public SkillData[] SkillList => _skillList;
    public Archetype Archetype => _archetype;

}

