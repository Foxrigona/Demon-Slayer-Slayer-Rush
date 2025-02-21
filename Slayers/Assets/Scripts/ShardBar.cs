using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShardBar : MonoBehaviour
{
    [SerializeField] int maxShardCount = 3;
    public void updateBar(int shardCount)
    {
        GetComponent<Scrollbar>().size = (float)shardCount / this.maxShardCount;
        if (shardCount >= maxShardCount) GetComponentInChildren<TextMeshProUGUI>()?.SetText("Press K");
        else GetComponentInChildren<TextMeshProUGUI>()?.SetText("");
    }
}
