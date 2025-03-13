using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CurseData", menuName = "Dataset/Curse")]
public class CurseData : ScriptableObject
{
    [SerializeField]
    private string _curseName = "...";
    [SerializeField]
    private string _curseDescription = "...";

    public string CurseName => _curseName;
    public string CurseDescription => _curseDescription;



}

