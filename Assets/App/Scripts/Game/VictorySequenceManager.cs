using UnityEngine;

public class VictorySequenceManager : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public JokeSpawner jokeSpawner;
    public PlayerCharacter playerCharacter;
    
    public void WinGame()
    {
        PrepareForEndSequence();
    }

    private void PrepareForEndSequence()
    {
        jokeSpawner.enabled = false;
        jokeSpawner.spawnTimer.enabled = false;
        enemySpawner.enabled = false;
        enemySpawner.spawnTimer.enabled = false;
        
        var jokePickups = jokeSpawner.jokePickupPool.jokePickupPool.GetActiveObjects();

        foreach (var jokePickup in jokePickups)
            jokePickup.Deactivate();
    }

    private void BeginPlayerMovement()
    {
        
    }

    public void BeginFinalMusic()
    {
        
    }

    private void BeginBossEntry()
    {
        
    }
    
    private void BeginNarratingIntro()
    {
        
    }
    
    private void BeginFinalJoke()
    {
        
    }

    private void OnFinalJokeFinished()
    {
        
    }
    
    private void BeginNarratingOutro()
    {
        
    }
    
    private void OnOutroFinished()
    {
        
    }
    
    
}