using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShardGiver : MonoBehaviour
{
    public void DestroyShard(ref int shardCount)
    {
        shardCount += 1;
        GameObject.Find("Attack Shard Count").GetComponent<TextMeshProUGUI>().SetText("Attack Shards: " +  shardCount.ToString());
        GameObject.Find("Attack Shard Bar").GetComponent<ShardBar>().updateBar(shardCount);
        Debug.Log(shardCount);
        Destroy(gameObject);
    }
}
