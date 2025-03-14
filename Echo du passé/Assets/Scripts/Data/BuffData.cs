﻿using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "Dataset/Buff")]
public class BuffData : ScriptableObject
{
    [SerializeField]
    private string _buffName = "...";
    [SerializeField]
    private string _buffDescription = "...";

    public string BuffName => _buffName;
    public string BuffDescription => _buffDescription;



}

