using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    private NetworkService _network;
    public ManagerStatus status { get; private set; }

    public int health { get; private set; }
    public int maxHealth { get; private set; }

    public void Startup(NetworkService service)
    {
        Debug.Log("Player manager starting...");

        UpdateDate(50, 100);

        _network = service;

        status = ManagerStatus.Started;
    }

    public void UpdateDate(int health, int maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
    }
    public void ChangeHealth(int value)
    {
        health += value;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }

        if (health == 0)
        {
            Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        }

        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
    }

    public void Respawn()
    {
        UpdateDate(50, 100);
    }
}
