using TMPro;
using UnityEngine;

public class DropdownSample: MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _text = null;

	[SerializeField]
	private TMP_Dropdown _dropdownWithoutPlaceholder = null;

	[SerializeField]
	private TMP_Dropdown _dropdownWithPlaceholder = null;

	public void OnButtonClick()
	{
		_text.text = _dropdownWithPlaceholder.value > -1 ? "Selected values:\n" + _dropdownWithoutPlaceholder.value + " - " + _dropdownWithPlaceholder.value : "Error: Please make a selection";
	}
}
