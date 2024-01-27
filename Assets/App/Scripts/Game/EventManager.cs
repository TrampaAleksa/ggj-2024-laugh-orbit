using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnemyHitPlayerEvent(PlayerCharacter player, Enemy enemy)
    {
        // Do something when the enemy hits the player
        Debug.Log("Enemy: " + enemy.name + " hit the player: " + player.name);
        player.health.RemoveHealth(1);
    }
    
    public void BulletHitEnemyEvent(Bullet bullet, Enemy enemy)
    {
        // Do something when the bullet hits the enemy
        Debug.Log("Bullet: " + bullet.name + " hit the enemy: " + enemy.name);
        enemy.health.RemoveHealth(1);
    }
    
    public void PlayerHitPickup(PlayerCharacter player , JokePickup pickup)
    {
        // Do something when the player hits the pickup
        Debug.Log("Player: " + player.name + " hit the pickup: " + pickup.name);
        StartEnemyDeathLaughEvent();
        pickup.Deactivate();
    }
    
    public void StartEnemyDeathLaughEvent()
    {
        Debug.Log("Enemies are starting to laugh");

        foreach (var enemy in EnemyPool.Instance.GetActiveEnemies())
        {
            enemy.StartDeathLaughing();
        }
    }

    public void GameOverEvent()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("Game");
    }
}