using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class SpawnTimer : MonoBehaviour
{
    public UnityEvent onSpawnEvent;
    
    public float maxSpawnInterval = 2f; // Time between each spawn
    public float minSpawnInterval = 2f; // Time between each spawn
    private float randomSpawnInterval => Random.Range(minSpawnInterval, maxSpawnInterval);

    private bool IsSpawningActive;
    private float timer; // Timer to keep track of spawning\


    public void Init(UnityAction onSpawnEvent)
    {
        this.onSpawnEvent.AddListener(onSpawnEvent);
        timer = randomSpawnInterval;
        Continue();
    }
    
    public void Stop ()
    {
        IsSpawningActive = false;
    }
    
    public void Continue ()
    {
        IsSpawningActive = true;
    }
    
    private void Update()
    {
        if (!IsSpawningActive)
            return;
        
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            onSpawnEvent?.Invoke();
            timer = randomSpawnInterval;
        }
    }
}