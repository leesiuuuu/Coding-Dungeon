using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Store Character")]
public class StoreCharacterSO : ScriptableObject
{
	public CharacterSo CharacterSO;
	public string Description;
	public int Cost;
}
