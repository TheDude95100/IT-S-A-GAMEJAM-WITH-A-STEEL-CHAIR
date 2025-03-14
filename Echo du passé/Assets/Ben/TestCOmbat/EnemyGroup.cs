using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGroup", menuName = "Scriptable Objects/EnemyGroup")]
public class EnemyGroup : ScriptableObject
{
    [SerializeField] private Enemy[] enemyGroup;

    public Enemy[] EnemyGroupScene { get => enemyGroup; set => enemyGroup = value; }
}
