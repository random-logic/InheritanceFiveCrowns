using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragAndDrop
{ 
    public class ToolTipSimple : MonoBehaviour, IToolTip
    {
        [TextArea]
        public string Tip;

        public string GetToolTipMessage()
        {
            return Tip;
        }
    }
}
