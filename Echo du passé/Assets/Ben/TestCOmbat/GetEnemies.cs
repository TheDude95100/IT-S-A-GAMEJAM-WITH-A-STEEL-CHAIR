using UnityEngine;

public class GetEnemies : MonoBehaviour
{
    [SerializeField] private EnemyGroup enemyGroup;
    void Start()
    {
        for (int i = 0;i<enemyGroup.EnemyGroupScene.Length;i++)
        {
            Debug.Log(enemyGroup.EnemyGroupScene[i].name);
        }
    }


}
