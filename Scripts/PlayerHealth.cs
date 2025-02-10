using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] bool isInvincible = false;
    private CharacterAnimator characterAnimator;
    private int maxHealth;

    private void Awake()
    {
        this.maxHealth = health;
        characterAnimator = new CharacterAnimator(GetComponent<Animator>());
    }

    public void decreaseHealth()
    {
        if (isInvincible) return;
        health--;
        GetComponentInChildren<Scrollbar>().size = (float) health / maxHealth;
        this.characterAnimator.triggerHurt();
        if (health <= 0) kill();
    }

    public void kill()
    {
        FindObjectOfType<GameHandler>().EndGame();
        Destroy(this.gameObject);
    }

    public void enableInvincibility()
    {
        this.isInvincible = true;
    }

    public void disableInvincibility()
    {
        this.isInvincible = false;
    }
}
