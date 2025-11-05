using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Stage")]
public class StageSO : ScriptableObject
{
	public string StageID;
	public string StageName;

	public Sprite[] AppearEnemys;
}
