using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ThunderAssistor : MonoBehaviour
{
    private int thunderShards = 0;
    private CharacterAttacker attacker;
    private Spawner spawner;
    private Destroyable[] destroyables;
    private Vector2 originalPosition;
    private AssistHealth health;
    private Destroyable target = null;
    private bool isAttacking = false;
    [SerializeField] float attackTime = 1f;
    private float currentTime = 0;
    private bool isReturning = false;
    private CharacterAnimator characterAnimator;
    [SerializeField] int requiredAttackShards = 3;
    [SerializeField] float ultTime = 1f;
    private bool isUlting = false;
    private Vector2 bossPosition;
    private float ultTimer = 0;
    private Enemy enemy;

    private void Awake()
    {
        health = FindObjectOfType<AssistHealth>(); 
        attacker = FindObjectOfType<CharacterAttacker>();
        spawner = FindObjectOfType<Spawner>();
        this.originalPosition = new Vector2(this.transform.position.x,this.transform.position.y);
        this.characterAnimator = new CharacterAnimator(GetComponent<Animator>());
        this.enemy = FindObjectOfType<Enemy>();
        GameObject.Find("Assist Shard Count").transform.GetComponent<TextMeshProUGUI>().SetText("Thunder Shards: " + this.thunderShards);
        GameObject.Find("Assist Shard Bar").GetComponent<ShardBar>().updateBar(this.thunderShards);
    }


    private void attackTarget()
    {
        Vector2 targetPos = new Vector2(this.transform.position.x,target.transform.position.y);
        transform.position = Vector2.Lerp(this.transform.position, targetPos, currentTime/attackTime);
        currentTime += Time.deltaTime;
        if(this.currentTime >= attackTime)
        {
            this.target.destroyObstacle();
            this.target = null;
            this.health.DecreaseHealth();
        }
    }

    private void finishAttack()
    {
        this.isAttacking = false;
        this.isReturning = true;
        this.currentTime = 0;
    }

    private void returnToPosition()
    {
        transform.position = Vector2.Lerp(this.transform.position, this.originalPosition, currentTime / attackTime);
        currentTime += Time.deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0, this.attackTime);
        if(currentTime >= this.attackTime)
        {
            this.isReturning = false;
            this.currentTime = 0;
        }
    }

    private void destroyTarget()
    {
        target.destroyObstacle();
        health.DecreaseHealth();
    }

    public void increaseShards(int shardCount)
    {
        thunderShards += shardCount;
        GameObject.Find("Assist Shard Count").transform.GetComponent<TextMeshProUGUI>().SetText("Thunder Shards: " + this.thunderShards);
        GameObject.Find("Assist Shard Bar").GetComponent<ShardBar>().updateBar(this.thunderShards);
        if (this.thunderShards >= this.requiredAttackShards) StartUlt();
    }

    public void decreaseShards(int shardCount)
    {
        thunderShards -= shardCount;
    }

    private void StartUlt()
    {
        this.isUlting = true;
        this.ultTimer = 0;
        this.characterAnimator.startUltimate();
        bossPosition = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
        GetComponent<SpriteRenderer>().flipX = true;
    }

    private void ChaseBoss()
    {
        transform.position = Vector2.Lerp(this.originalPosition, this.bossPosition, ultTimer/ultTime);
        this.ultTimer += Time.deltaTime;
    }

    public void attackBoss()
    {
        GameObject.Find("Assist Shard Count").transform.GetComponent<TextMeshProUGUI>().SetText("Thunder Shards: " + 0);
        GameObject.Find("Assist Shard Bar").GetComponent<ShardBar>().updateBar(0);
        FindObjectOfType<EnemyHealth>().decreaseHealth();
        Destroy(gameObject);
    }

    private void Update()
    {
        destroyables = FindObjectsOfType<Destroyable>();
        if (this.target == null && !this.isUlting)
        {
            if (this.isReturning)
            {
                returnToPosition();
            }
            else
            {
                foreach (Destroyable destroyable in destroyables)
                {
                    if (destroyable.transform.position.x < attacker.transform.position.x)
                    {
                        this.currentTime = 0;
                        this.target = destroyable;
                        this.isAttacking = true;
                        this.characterAnimator.triggerAttack();
                        break;
                    }
                }
            }
            
        }
        else
        {
            if (this.isAttacking && !this.isUlting)
            {
                attackTarget();
            }
        }

        if (this.isUlting)
        {
            ChaseBoss();
            if(ultTimer > ultTime)
            {
                attackBoss();
            }
        }
    }
    public void updateBoss()
    {
        this.enemy = FindObjectOfType<Enemy>();
    }
}
