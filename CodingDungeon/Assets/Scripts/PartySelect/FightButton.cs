using UnityEngine;
using UnityEngine.UI;

public class FightButton : MonoBehaviour
{
	[SerializeField] private Button btn;
	private void Update()
	{
		if(PartySelector.Instance.selectedCharacters.Count >= PartySelector.Instance.MaxCharacter)
		{
			btn.interactable = true;
		}
		else
		{
			btn.interactable = false;
		}
	}
}
