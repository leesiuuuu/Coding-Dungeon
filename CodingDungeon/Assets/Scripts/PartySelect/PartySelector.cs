using System.Collections.Generic;
using UnityEngine;

public class PartySelector : SingleMono<PartySelector>
{
	public List<CharacterProfile> selectedCharacters = new List<CharacterProfile>();
	public int MaxCharacter = 3;

	public void Enqueue(CharacterProfile profile)
	{
		profile.Select();
		selectedCharacters.Add(profile);

		// CharacterSpawner에게 생성 요청
		CharacterSpawner.Instance.SpawnCharacter(profile);
	}

	public void Dequeue() => Deselect(selectedCharacters[0]);

	public bool IsInclude(CharacterProfile profile) => selectedCharacters.Contains(profile);

	public void Deselect(CharacterProfile profile)
	{
		if (selectedCharacters.Count == 0) return;

		profile.Deselect();
		selectedCharacters.Remove(profile);

		// CharacterSpawner에게 삭제 요청
		CharacterSpawner.Instance.DespawnCharacter(profile);
	}

	public bool IsFull()
	{
		return (selectedCharacters.Count >= MaxCharacter);
	}

	public bool IsEmpty()
	{
		return (selectedCharacters.Count == 0);
	}

	// 모든 캐릭터 해제 (필요시 사용)
	public void ClearAll()
	{
		while (selectedCharacters.Count > 0)
		{
			Deselect(selectedCharacters[0]);
		}
	}
}