using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public UIManager uiManager;
    public VictorySequenceManager victorySequence;

    private JokePickupCounter jokeCounter;
    private JokeSpawner _jokeSpawner;

    private void Awake()
    {
        Instance = this;
        jokeCounter = gameObject.AddComponent<JokePickupCounter>();
        _jokeSpawner = FindObjectOfType<JokeSpawner>();
    }

    public void EnemyHitPlayerEvent(PlayerCharacter player, Enemy enemy)
    {
        // Do something when the enemy hits the player
        Debug.Log("Enemy: " + enemy.name + " hit the player: " + player.name);
        player.health.RemoveHealth(1);
        enemy.DeactivateWithExplosion();
    }
    
    public void BulletHitEnemyEvent(Bullet bullet, Enemy enemy)
    {
        // Do something when the bullet hits the enemy
        Debug.Log("Bullet: " + bullet.name + " hit the enemy: " + enemy.name);
        enemy.health.RemoveHealth(1);
        bullet.Deactivate();
    }
    
    public void PlayerHitPickup(PlayerCharacter player , JokePickup pickup)
    {
        // Do something when the player hits the pickup
        Debug.Log("Player: " + player.name + " hit the pickup: " + pickup.name);
        StartEnemyDeathLaughEvent();
        
        pickup.Deactivate();
        int jokeCount=jokeCounter.AddJokeCount(out bool isReach);
        _jokeSpawner.spawnTimer.Stop();
        var jokePickups = _jokeSpawner.jokePickupPool.jokePickupPool.GetActiveObjects();

        foreach (var jokePickup in jokePickups)
            jokePickup.Deactivate();


        if (!isReach)
        {
            OpenAiHandler.StartAiSpeach(5+ jokeCount, () => _jokeSpawner.spawnTimer.Continue());
        }
    }
    
    public void StartEnemyDeathLaughEvent()
    {
        Debug.Log("Enemies are starting to laugh");

        foreach (var enemy in EnemyPool.Instance.GetActiveEnemies())
        {
            enemy.StartDeathLaughing();
        }
    }

    public void PlayerDiedEvent()
    {
        Debug.Log("Game Over");
        uiManager.GameOver();
    }
    
    public void ReachedNeededJokeNumberEvent()
    {
        WonGameEvent();
    }
    
    public void WonGameEvent()
    {
        Debug.Log("Game Won");
        victorySequence.WinGame();
    }
}