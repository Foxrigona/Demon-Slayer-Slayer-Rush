using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float velocity = 3;
    [SerializeField] private bool destroyOnImpact = true;
    [SerializeField] private bool doesDamage = true;
    public void setVelocity(float velocity)
    {
        this.velocity = velocity;
    }

    private void Update()
    {
        this.transform.Translate(Vector2.left * this.velocity * Time.deltaTime);
        if (this.transform.position.x< -60) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth health = collision.transform.GetComponent<PlayerHealth>();
        if (health == null) return;
        if(doesDamage) health.decreaseHealth();
        if(destroyOnImpact)Destroy(gameObject);
    }
}
