using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 3;
    private CharacterAnimator characterAnimator;
    private int maxHealth;

    private void Awake()
    {
        maxHealth = health; 
        characterAnimator = new CharacterAnimator(GetComponent<Animator>());
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.health = maxHealth;
        this.maxHealth = maxHealth;
    }

    public void decreaseHealth()
    {
        health--;
        GetComponentInChildren<Scrollbar>().size = (float)health / maxHealth;
        characterAnimator.triggerHurt();
        if (health <= 0) kill();
    }

    public void kill()
    {
        FindObjectOfType<GameHandler>().newBoss();
    }
}
