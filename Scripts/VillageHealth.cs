using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageHealth : MonoBehaviour
{
    [SerializeField] private int health;
    private int maxHealth;
    private void Start()
    {
        maxHealth = health;
    }
    public void TakeDamage()
    {
        health--;
        GameObject.Find("Game Canvas").transform.GetComponentInChildren<Scrollbar>().size = (float)health / maxHealth;
        if(health <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        FindObjectOfType<GameHandler>().EndGame();
    }
}
