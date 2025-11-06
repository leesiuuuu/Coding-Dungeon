using System.Linq;
using UnityEngine;

public class BoughtCheck : MonoBehaviour
{
	[SerializeField] private StoreCharacter[] storeCharacters;
	private void OnEnable()
	{
		foreach(StoreCharacter character in storeCharacters)
		{
			if (PlayerDataManager.Instance.CurrentCharacter.Contains(character.GetSO()))
			{
				character.SetBought(true);
			}
		}
	}
}
