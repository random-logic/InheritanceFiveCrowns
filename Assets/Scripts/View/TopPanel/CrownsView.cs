using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrownsView : MonoBehaviour
{
    protected TextMeshPro textMeshPro;

    public void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    public void Set(int yourCrowns, int opponentCrowns)
    {
        textMeshPro.text = "Crowns: " + yourCrowns + ", " + opponentCrowns;
    }
}
