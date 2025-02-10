using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] List<GameObject> spawnables = new List<GameObject>();
    [SerializeField] private float throwVelocity = 5f;

    public void spawnObject()
    {
        if (spawnables.Count <= 0) return;
        GameObject spawnable = spawnables[Random.Range(0, spawnables.Count)];
        GameObject temp = Instantiate(spawnable, spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.identity);
        temp.GetComponent<Obstacle>().setVelocity(this.throwVelocity);
    }

    public void spawnObject(GameObject attack, int position)
    {
        GameObject temp = Instantiate(attack, spawnPoints[position].transform.position, Quaternion.identity);
        temp.GetComponent<Obstacle>().setVelocity(this.throwVelocity);
    }

    public void IncreaseThrowVelocity(float velocity)
    {
        this.throwVelocity += velocity;
    }

}
