using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class VictorySequenceManager : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public JokeSpawner jokeSpawner;
    public PlayerCharacter playerCharacter;
    public CinematicEffectTween cinematicTween;
    public UIManager uiManager;

    public Transform boss;
    public Transform bossTargetPosition;

    public float delayBeforeBeginningSequence = 3f;
    public float playerMoveDuration = 1.0f;
    public float delayBeforeBossEntry = 2f;
    public float bossMoveDuration = 5.0f;
    public float delayAfterBossStops = 2f;


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
        BeginCinematicView();
        Invoke(nameof(BeginBossEntry), cinematicTween.duration + delayBeforeBossEntry);
    }

    public void BeginFinalMusic()
    {
        
    }
    
    public void BeginCinematicView()
    {
        cinematicTween.StartMoveAndResize();
    }

    private void BeginBossEntry()
    {
        boss.DOMove(bossTargetPosition.position, bossMoveDuration).SetEase(Ease.OutQuad); 
        Invoke(nameof(BeginNarratingIntro), bossMoveDuration + delayAfterBossStops);
    }
    
    private void BeginNarratingIntro()
    {
        BeginFinalJoke();
    }
    
    private void BeginFinalJoke()
    {
        OnFinalJokeFinished();
    }

    private void OnFinalJokeFinished()
    {
        BeginNarratingOutro();
    }
    
    private void BeginNarratingOutro()
    {
        OnOutroFinished();
    }
    
    private void OnOutroFinished()
    {
        uiManager.GameWon();
    }
    
    
    
  
    public void MoveObjectToCenter(Transform playerTransform)
    {
        float centerX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0)).x;
        playerTransform.DOMoveX(centerX, playerMoveDuration).SetEase(Ease.OutQuad); 
    }
    
}