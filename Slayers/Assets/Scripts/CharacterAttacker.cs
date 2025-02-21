using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAttacker : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float cooldown;
    private bool isOnCooldown = false;
    private float timePassed = 0;
    private bool canAttack = true;
    private bool isAttacking = false;
    private int attackShardCount;
    private CharacterAnimator animator;
    private PlayerHealth playerHealth;
    private Vector2 originalPosition;
    private Vector2 targetPosition;
    [SerializeField] private float attackRange = 3;
    [SerializeField] private float ultSpeed = 3;
    private CharacterMover characterMover;
    private float currentTime = 0;
    private Enemy enemy;
    private bool isUlting = false;
    private bool isChasing = false;

    private void Awake()
    {
        this.animator = new CharacterAnimator(GetComponent<Animator>());
        playerHealth = GetComponent<PlayerHealth>();
        enemy = FindObjectOfType<Enemy>();
        this.characterMover = GetComponent<CharacterMover>();
        this.originalPosition = new Vector2(transform.position.x, transform.position.y);
    }

    private void attack()
    {
        if (canAttack && !this.isOnCooldown)
        {
            this.isAttacking = true;
            destroyTarget();
            this.animator.triggerAttack();
            this.isOnCooldown = true;
        }
    }

    private void destroyTarget()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right, this.attackRange);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.GetComponent<Destroyable>() != null)
            {
                Debug.Log("Killed");
                hit.transform.GetComponent<Destroyable>().destroyObstacle();
            }
            else if(hit.transform.GetComponent<ShardGiver>() != null)
            {
                hit.transform.GetComponent<ShardGiver>().DestroyShard(ref this.attackShardCount);
            }
            else if(hit.transform.GetComponent<AssistGiver>() != null)
            {
                hit.transform.GetComponent<AssistGiver>().GiveAssist();
            }
        }
    }

    private void disableAttack()
    {
        this.canAttack = false;
    }

    private void enableAttack()
    {
        this.canAttack = true;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.right * this.attackRange);
        if (Input.GetKeyDown(KeyCode.J))
        {
            this.attack();
        }
        if (Input.GetKeyDown(KeyCode.K) && attackShardCount >= 3)
        {
            if(!isUlting && this.characterMover.IsOnGround && this.canAttack)
                StartUltimate();
        }

        if (this.isOnCooldown) updateCooldown();
        if (this.isAttacking) destroyTarget();

        if(isUlting && isChasing)
        {
            ChaseBoss();
            if(currentTime > ultSpeed)
            {
                isChasing = false;
                DamageBoss();
                this.currentTime = 0;
            }
        } else if(this.isUlting && !this.isChasing)
        {
            ReturnFromBoss();
            if(currentTime > ultSpeed)
            {
                this.isUlting = false;
                this.animator.endUltimate();
                this.playerHealth.disableInvincibility();
            }
        }
    }

    private void StartUltimate()
    {
        this.isUlting = true;
        this.isChasing = true;
        this.targetPosition = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
        playerHealth.enableInvincibility();
        animator.startUltimate();
        this.currentTime = 0;
        this.attackShardCount -= 3;
        GameObject.Find("Attack Shard Count").GetComponent<TextMeshProUGUI>().SetText("Attack Shards: " + this.attackShardCount);
        GameObject.Find("Attack Shard Bar").GetComponent<ShardBar>().updateBar(this.attackShardCount);
    }

    private void ChaseBoss()
    {
        transform.position = Vector2.Lerp(this.originalPosition, this.targetPosition, currentTime/ultSpeed);
        currentTime += Time.deltaTime;
    }

    private void ReturnFromBoss()
    {
        transform.position = Vector2.Lerp(this.targetPosition,this.originalPosition, currentTime/ultSpeed);
        currentTime += Time.deltaTime;
        Debug.Log(currentTime / ultSpeed);
    }

    private void DamageBoss()
    {
        FindObjectOfType<EnemyHealth>().decreaseHealth();
    }

    private void updateCooldown()
    {
        this.timePassed += Time.deltaTime;
        if (this.timePassed > cooldown)
        {
            this.timePassed = 0;
            this.isOnCooldown = false;
            this.enableAttack();
            this.isAttacking = false;
        }
    }

    public void updateBoss()
    {
        this.enemy = FindObjectOfType<Enemy>();
    }
}
