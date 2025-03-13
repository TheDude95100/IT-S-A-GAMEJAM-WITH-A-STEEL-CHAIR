using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "Scriptable Objects/Buff")]
public class BuffData : ScriptableObject
{
    [SerializeField]
    public string buffName = "...";
    [SerializeField]
    public string buffDescription = "...";



}

