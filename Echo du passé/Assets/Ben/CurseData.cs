using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CurseData", menuName = "Scriptable Objects/Curse")]
public class CurseData : ScriptableObject
{
    [SerializeField]
    public string curseName = "...";
    [SerializeField]
    public string curseDescription = "...";



}

