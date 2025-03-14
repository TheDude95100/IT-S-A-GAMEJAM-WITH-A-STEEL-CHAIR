using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField][Range(1,4)]
    private int addCounter = 1;

    [SerializeField]
    private Enemy[] addsPool;

    public Enemy[] AddAdds(Enemy firstEnemy)
    {
        Enemy[] enemies = new Enemy[addCounter+1];
        enemies[0] = firstEnemy;
        for (int i = 1; i < addCounter+1; i++)
        {
            enemies[i] = addsPool[Random.Range(0, addsPool.Length)];
        }
        return enemies;
    }
}
