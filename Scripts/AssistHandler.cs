using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> assistCharacters = new List<GameObject>();
    public void giveAssist(AssistTypes assistTypes)
    {
        if(assistTypes == AssistTypes.Thunder)
        {
            ThunderAssistor zenitsu = FindObjectOfType<ThunderAssistor>();
            if (zenitsu == null) Instantiate(assistCharacters[0]);
            else zenitsu.increaseShards(1);
        }
        else if(assistTypes == AssistTypes.Sound)
        {
            SoundAssistor uzui = FindObjectOfType<SoundAssistor>();
            if(uzui == null) Instantiate(assistCharacters[1]);
            else uzui.increaseShards(1);
        }
    }
}
