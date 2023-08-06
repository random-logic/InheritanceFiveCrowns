using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


namespace TMPro.Examples
{

    public class TmpExampleScript01 : MonoBehaviour
    {
        public enum ObjectType { TextMeshPro = 0, TextMeshProUgui = 1 };

        public ObjectType Type;
        public bool IsStatic;

        private TMP_Text _mText;

        //private TMP_InputField m_inputfield;


        private const string KLabel = "The count is <#0080ff>{0}</color>";
        private int _count;

        void Awake()
        {
            // Get a reference to the TMP text component if one already exists otherwise add one.
            // This example show the convenience of having both TMP components derive from TMP_Text. 
            if (Type == 0)
                _mText = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
            else
                _mText = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();

            // Load a new font asset and assign it to the text object.
            _mText.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Anton SDF");

            // Load a new material preset which was created with the context menu duplicate.
            _mText.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/Anton SDF - Drop Shadow");

            // Set the size of the font.
            _mText.fontSize = 120;

            // Set the text
            _mText.text = "A <#0080ff>simple</color> line of text.";

            // Get the preferred width and height based on the supplied width and height as opposed to the actual size of the current text container.
            Vector2 size = _mText.GetPreferredValues(Mathf.Infinity, Mathf.Infinity);

            // Set the size of the RectTransform based on the new calculated values.
            _mText.rectTransform.sizeDelta = new Vector2(size.x, size.y);
        }


        void Update()
        {
            if (!IsStatic)
            {
                _mText.SetText(KLabel, _count % 1000);
                _count += 1;
            }
        }

    }
}
