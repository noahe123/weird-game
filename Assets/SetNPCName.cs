using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PixelCrushers.QuestMachine;

public class SetNPCName : MonoBehaviour
{
    public TextMeshPro _textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        _textMeshPro.text = GetComponent<QuestGiver>().displayName.ToString();
    }

}
