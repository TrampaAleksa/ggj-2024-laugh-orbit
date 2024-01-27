using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [HideInInspector]
    public Health health;
    [HideInInspector]
    public PlayerMovement playerMovement;
    [HideInInspector]
    public PlayerShooting playerShooting;

    private void Awake()
    {
        health = GetComponent<Health>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<PlayerShooting>();
    }
    private void Start()
    {
        health.OnZeroHealth.AddListener(EventManager.Instance.PlayerDiedEvent);
    }
}