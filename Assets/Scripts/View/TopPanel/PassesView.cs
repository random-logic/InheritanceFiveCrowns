using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class PassesView : MonoBehaviour
    {
        protected TextMeshPro textMeshPro;

        public void Start()
        {
            textMeshPro = GetComponent<TextMeshPro>();
        }

        public void Set(int passes, int maxPasses)
        {
            textMeshPro.text = "Passes: " + passes + "/" + maxPasses;
        }
    }
}