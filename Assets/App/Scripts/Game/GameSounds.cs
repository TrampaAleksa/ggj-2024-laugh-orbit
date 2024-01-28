using UnityEngine;

public class GameSounds : MonoBehaviour
{
    public static GameSounds Instance;
    
    public AudioSource smallExplosionSound;
    public AudioSource pickupExplosionSound;
    public AudioSource bossExplosionSound;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void PlaySmallExplosionSound()
    {
        smallExplosionSound.Play();
    }
    
    public void PlayPickupExplosionSound()
    {
        pickupExplosionSound.Play();
    }
    
    public void PlayBossExplosionSound()
    {
        bossExplosionSound.Play();
    }
}