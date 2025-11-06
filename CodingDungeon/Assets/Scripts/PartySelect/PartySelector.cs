using System.Collections.Generic;
using UnityEngine;

public class PartySelector : SingleMono<PartySelector>
{
	public List<CharacterProfile> selectedCharacters;

	public int MaxCharacter = 3;

	public void Enqueue(CharacterProfile profile)
	{
		profile.Select();
		selectedCharacters.Add(profile);
	}

	public void Dequeue() => Deselect(selectedCharacters[0]);
	public bool IsInclude(CharacterProfile profile) => selectedCharacters.Contains(profile);

	public void Deselect(CharacterProfile profile)
	{
		if (selectedCharacters.Count == 0) return;
		profile.Deselect();
		selectedCharacters.Remove(profile);
	}

	public bool IsFull()
	{
		return (selectedCharacters.Count >= MaxCharacter);
	}
	public bool IsEmpty()
	{
		return (selectedCharacters.Count == 0);
	}
}
