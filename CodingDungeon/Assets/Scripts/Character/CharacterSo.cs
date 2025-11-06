using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Character")]
public class CharacterSo : ScriptableObject
{
	public string Name;

	public Sprite Image;

	public EntityAttributes Attributes;
	
	public DeckSo Deck;
}