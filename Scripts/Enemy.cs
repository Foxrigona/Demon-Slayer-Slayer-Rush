using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float attackDelay = 3;
    [SerializeField] private List<GameObject> attacks = new List<GameObject>();
    [SerializeField] private List<int> rarities = new List<int>();
    private float timePassed = 0;
    private CharacterAnimator characterAnimator;

    private Spawner attackSpawner;

    private void Awake()
    {
        attackSpawner = FindObjectOfType<Spawner>();
        characterAnimator = new CharacterAnimator(GetComponent<Animator>());
    }

    private void Update()
    {
        if(timePassed > attackDelay)
        {
            this.timePassed = 0;
            attack();
        }
        else
        {
            this.timePassed += Time.deltaTime; 
        }
    }

    private void attack()
    {
        characterAnimator.triggerAttack();
        int raritySum = 0;
        foreach(int rarity in rarities)
        {
            raritySum += rarity;
        }

        int countChoice = Random.Range(1, 3);


        List<int> choices = new List<int>();
        for (int i = 0; i < countChoice; i++)
        {
            int typeSum = Random.Range(0, raritySum);
            int otherSum = 0;
            int choice = 0;
            for (int n = 1; n < rarities.Count; n++)
            {
                if (typeSum > otherSum && typeSum < otherSum + rarities[n])
                {
                    choice = n;
                    break;
                }
                else
                {
                    otherSum += rarities[n];
                }
            }


            int position = Random.Range(0, 3);
            while (choices.Contains(position))
            {
                position = Random.Range(0, 3);
            }
            choices.Add(position);

            attackSpawner.spawnObject(this.attacks[choice], position);
        }
    }
}
