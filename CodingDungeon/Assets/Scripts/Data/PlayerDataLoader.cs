using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDataLoader : MonoBehaviour
{
	private List<PartySO> list;
	[SerializeField] private GameObject CharacterProfile;
	[SerializeField] private Transform CharacterContainer;

	private void OnEnable()
	{
		list = PlayerDataManager.Instance.CurrentCharacter.ToList();

		RemoveAll();
		
		for(int i = 0; i < list.Count; i++)
		{
			var obj = Instantiate(CharacterProfile, CharacterContainer);
			obj.GetComponent<CharacterProfile>().UpdateSO(list[i]);
		}
	}
	public void RemoveAll()
	{
		if (CharacterContainer.childCount > 0)
		{
			foreach (Transform child in CharacterContainer)
			{
				Destroy(child.gameObject);
			}
			PartySelector.Instance.selectedCharacters.Clear();
		}
	}
}

