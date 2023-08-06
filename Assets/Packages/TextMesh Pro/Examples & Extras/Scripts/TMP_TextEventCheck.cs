using UnityEngine;


namespace TMPro.Examples
{
    public class TmpTextEventCheck : MonoBehaviour
    {

        public TmpTextEventHandler TextEventHandler;

        private TMP_Text _mTextComponent;

        void OnEnable()
        {
            if (TextEventHandler != null)
            {
                // Get a reference to the text component
                _mTextComponent = TextEventHandler.GetComponent<TMP_Text>();
                
                TextEventHandler.OnCharacterSelection.AddListener(OnCharacterSelection);
                TextEventHandler.OnSpriteSelection.AddListener(OnSpriteSelection);
                TextEventHandler.OnWordSelection.AddListener(OnWordSelection);
                TextEventHandler.OnLineSelection.AddListener(OnLineSelection);
                TextEventHandler.OnLinkSelection.AddListener(OnLinkSelection);
            }
        }


        void OnDisable()
        {
            if (TextEventHandler != null)
            {
                TextEventHandler.OnCharacterSelection.RemoveListener(OnCharacterSelection);
                TextEventHandler.OnSpriteSelection.RemoveListener(OnSpriteSelection);
                TextEventHandler.OnWordSelection.RemoveListener(OnWordSelection);
                TextEventHandler.OnLineSelection.RemoveListener(OnLineSelection);
                TextEventHandler.OnLinkSelection.RemoveListener(OnLinkSelection);
            }
        }


        void OnCharacterSelection(char c, int index)
        {
            Debug.Log("Character [" + c + "] at Index: " + index + " has been selected.");
        }

        void OnSpriteSelection(char c, int index)
        {
            Debug.Log("Sprite [" + c + "] at Index: " + index + " has been selected.");
        }

        void OnWordSelection(string word, int firstCharacterIndex, int length)
        {
            Debug.Log("Word [" + word + "] with first character index of " + firstCharacterIndex + " and length of " + length + " has been selected.");
        }

        void OnLineSelection(string lineText, int firstCharacterIndex, int length)
        {
            Debug.Log("Line [" + lineText + "] with first character index of " + firstCharacterIndex + " and length of " + length + " has been selected.");
        }

        void OnLinkSelection(string linkId, string linkText, int linkIndex)
        {
            if (_mTextComponent != null)
            {
                TMP_LinkInfo linkInfo = _mTextComponent.textInfo.linkInfo[linkIndex];
            }
            
            Debug.Log("Link Index: " + linkIndex + " with ID [" + linkId + "] and Text \"" + linkText + "\" has been selected.");
        }

    }
}
