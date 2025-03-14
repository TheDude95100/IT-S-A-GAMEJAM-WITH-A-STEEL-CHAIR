using UnityEngine;
using UnityEngine.SceneManagement;

public class Ennemy : InteractionSystem
{
    [SerializeField] private Enemy principalEnemy;
    [SerializeField] private EnemyGroup enemyGroupScene;
    [SerializeField] private EnemiesManager enemiesManager;

    protected override void Interact()
    {
        
        enemyGroupScene.EnemyGroupScene = enemiesManager.AddAdds(principalEnemy);
        SceneManager.LoadScene(5);
    }
}
