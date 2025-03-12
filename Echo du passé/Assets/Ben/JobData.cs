using System;
using UnityEngine;

[CreateAssetMenu(fileName = "JobData", menuName = "Scriptable Objects/Job")]
public class JobData : ScriptableObject
{
    public string entityName;

    //[SerializeField] 
    //private Array<Skill> _skillList = new();
    [SerializeField] 
    private Archetype _archetype = Archetype.None;

}

