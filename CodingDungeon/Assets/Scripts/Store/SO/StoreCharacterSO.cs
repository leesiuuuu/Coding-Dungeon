using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Store Character")]
public class StoreCharacterSO : ScriptableObject
{
	public PartySO CharacterSO;
	public string Description;
	public int Cost;
}
