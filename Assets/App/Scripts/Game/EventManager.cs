using UnityEngine;

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
    }
    
    public void BulletHitEnemyEvent(Bullet bullet, Enemy enemy)
    {
        // Do something when the bullet hits the enemy
        Debug.Log("Bullet: " + bullet.name + " hit the enemy: " + enemy.name);
    }
    
    public void PlayerHitPickup(PlayerCharacter player , JokePickup pickup)
    {
        // Do something when the player hits the pickup
        Debug.Log("Player: " + player.name + " hit the pickup: " + pickup.name);
    }
    
    public void TriggerEnemyLaughDeathEvent(Enemy enemy)
    {
        // Do something when the enemy dies
        Debug.Log("Enemy: " + enemy.name + " is dying of laughter");
        enemy.TriggerLaughDeath();
    }
}