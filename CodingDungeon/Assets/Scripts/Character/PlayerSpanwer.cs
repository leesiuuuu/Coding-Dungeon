using System.Collections.Generic;
using UnityEngine;

public class PlayerSpanwer : MonoBehaviour
{
	[SerializeField] private Transform[] spawners;

	private void Start()
	{
		SpawnPlayers();
	}

	public void SpawnPlayers()
	{
		List<PartySO> list = PartySelector.Instance.selectedPartyData;

		PartyManager.Instance.Characters.Clear();

		for(int i = 0; i < list.Count; i++)
		{
			var obj = Instantiate(list[i].InGamePrefab, spawners[i].position, Quaternion.identity);
			PartyManager.Instance.Characters.Add(obj.GetComponent<Character>());
		}
	}
}
