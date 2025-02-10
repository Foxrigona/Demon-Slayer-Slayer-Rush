using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistGiver : MonoBehaviour
{
    [SerializeField] private AssistTypes assistType;
    private AssistHandler assistHandler;

    private void Awake()
    {
        assistHandler = FindObjectOfType<AssistHandler>();
    }
    public void GiveAssist()
    {
        if (assistType == AssistTypes.Thunder)
        {
            assistHandler.giveAssist(assistType);
            Destroy(gameObject); 
        } else if(assistType == AssistTypes.Sound)
        {
            assistHandler.giveAssist(assistType);
            Destroy(gameObject);
        }
    }
}
