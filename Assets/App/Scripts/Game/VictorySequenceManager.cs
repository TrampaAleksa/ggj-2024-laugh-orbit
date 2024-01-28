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
    public ExplosionSequence explosionSequence;

    public Transform boss;
    public Transform bossTargetPosition;

    public float delayBeforeBeginningSequence = 3f;
    public float playerMoveDuration = 1.0f;
    public float delayBeforeBossEntry = 2f;
    public float bossMoveDuration = 5.0f;
    public float delayAfterBossStops = 2f;
    public float delayAfterFinalJoke = 1f;
    

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
        TTSHandler.Speak("Oh no", BeginFinalJoke);
    }
    
    private void BeginFinalJoke()
    {
        OpenAiHandler.StartAiSpeach(10,Mode.ROAST, OnFinalJokeFinished);
    }

    private void OnFinalJokeFinished()
    {
        Invoke(nameof(BeginExplosionSequence), delayAfterFinalJoke);
    }

    private void BeginExplosionSequence()
    {
        explosionSequence.BeginSequence(BeginNarratingOutro);
    }
    
    private void BeginNarratingOutro()
    {
        TTSHandler.Speak("Whoops, your ship also exploded, my bad. Well, that's fine. Why don't you play again and hear more jokes?", OnOutroFinished);
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