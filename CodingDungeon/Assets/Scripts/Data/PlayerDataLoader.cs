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
		
		for(int i = 0; i < list.Count; i++)
		{
			var obj = Instantiate(CharacterProfile, CharacterContainer);
			obj.GetComponent<CharacterProfile>().UpdateSO(list[i]);
		}
	}
}

