using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssistHealth : MonoBehaviour
{
    [SerializeField] private int health = 0;
    private int maxHealth;

    private void Start()
    {
        maxHealth = health;
    }

    public void IncreaseHealth()
    {
        health++;
    }

    public void DecreaseHealth()
    {
        health--;
        GetComponentInChildren<Scrollbar>().size = (float)health / maxHealth;
        if (health <= 0) kill();
    }

    public void kill()
    {
        if(GetComponent<SoundAssistor>() != null) 
            GameObject.Find("Assist Shard Count").transform.GetComponent<TextMeshProUGUI>().SetText("Sound Shards: " + 0);
        else if(GetComponent<ThunderAssistor>() != null)
            GameObject.Find("Assist Shard Count").transform.GetComponent<TextMeshProUGUI>().SetText("Thunder Shards: " + 0);
        Destroy(gameObject);
    }
}
