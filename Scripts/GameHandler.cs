using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] int initialThrowSpeed;
    [SerializeField] Vector2 bossPosition;
    [SerializeField] List<GameObject> bosses = new List<GameObject>();
    [SerializeField] GameObject endScreen;
    private Spawner spawner;
    private int healthIncrease = 1;
    private int roundsPassed = 1;
    private float speedIncrease = 1f;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    public void newBoss()
    {
        this.roundsPassed++;
        Destroy(FindObjectOfType<Enemy>().transform.gameObject);
        EnemyHealth e = Instantiate(bosses[Random.Range(0, bosses.Count)], bossPosition, Quaternion.identity).GetComponent<EnemyHealth>();
        FindObjectOfType<CharacterAttacker>().updateBoss();
        FindObjectOfType<ThunderAssistor>()?.updateBoss();
        FindObjectOfType<SoundAssistor>()?.updateBoss();
        IncreaseDifficulty(e);

    }

    private void IncreaseDifficulty(EnemyHealth e)
    {
        e.SetMaxHealth(healthIncrease * roundsPassed);
        BackgroundMover[] movers = FindObjectsOfType<BackgroundMover>();
        foreach(BackgroundMover m in movers)
        {
            m.IncreaseMovementSpeed(speedIncrease * 4);
        }
        spawner.IncreaseThrowVelocity(speedIncrease);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void EndGame()
    {
        Instantiate(endScreen);
    }
}
