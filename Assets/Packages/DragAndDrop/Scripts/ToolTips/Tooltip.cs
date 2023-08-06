using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DragAndDrop
{
    public class Tooltip : MonoBehaviour
    {
        [Tooltip("This is the parent object to turn on and off")]
        public GameObject TooltipObject;
        [Tooltip("The text to change")]
        public Text ToolTipText;
        public IToolTip Current;
        RectTransform _toolTipTransform;

        // Use this for initialization
        void Start()
        {
            _toolTipTransform = TooltipObject.GetComponent<RectTransform>();
            TooltipObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            // look under the mouse for a tooltip source
            Current = null;
            Vector2 pos = Input.mousePosition;
            GameObject go = GetObjectUnderPos(pos);
            if (go)
            {
                // found an object...
                Current = go.GetComponent<IToolTip>();
                if (Current == null && go.transform.parent)
                    Current = go.transform.parent.GetComponent<IToolTip>();
                if (Current != null)
                {
                    string tip = Current.GetToolTipMessage();
                     
                    if (tip != "")
                    {
                        TooltipObject.SetActive(true);

                        // we need this to force a recalculation of the text size to propagate up to the parent frame
                        ToolTipText.text = "";
                        // set the text to the tooltip one
                        ToolTipText.text = tip;

                        // positon at the mouse
                        _toolTipTransform.position = pos;

                        // make it point inwards to the centre of the screen
                        bool goLeft = (pos.x > Screen.width / 2);
                        _toolTipTransform.pivot = new Vector2(goLeft ? 1 : 0, pos.y > Screen.height / 2 ? 1 : 0);
                    }
                    else
                        Current = null;
                }
            }


            // turn the tooltip on or off based on whether we have a thing under the mouse
            TooltipObject.SetActive(Current != null);
        }

        // helper function for getting the UI pbject at a point
        List<RaycastResult> _hitObjects = new List<RaycastResult>();
        GameObject GetObjectUnderPos(Vector3 position)
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = position;
            EventSystem.current.RaycastAll(pointer, _hitObjects);
            return (_hitObjects.Count <= 0) ? null : _hitObjects[0].gameObject;
        }
    }
}
