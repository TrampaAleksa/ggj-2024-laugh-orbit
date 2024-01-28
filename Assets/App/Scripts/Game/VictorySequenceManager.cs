using DG.Tweening;
using UnityEngine;

public class VictorySequenceManager : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public JokeSpawner jokeSpawner;
    public PlayerCharacter playerCharacter;

    public float delayBeforeBeginningSequence = 3f;
    [SerializeField]
    private float playerMoveDuration = 1.0f;
    
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
        
        Invoke(nameof(BeginPlayerMovement), delayBeforeBeginningSequence);
    }

    private void BeginPlayerMovement()
    {
        playerCharacter.playerMovement.enabled = false;
        playerCharacter.playerShooting.enabled = false;
        
        MoveObjectToCenter(playerCharacter.transform);
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
    
    
    
  
    public void MoveObjectToCenter(Transform playerTransform)
    {
        float centerX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0)).x;
        playerTransform.DOMoveX(centerX, playerMoveDuration).SetEase(Ease.OutQuad); 
    }
    
}