using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField]
	private int currentHP = 100;

	[SerializeField]
	private int maxHP = 100;

	public int CurrentHP => currentHP;
	public int MaxHP => maxHP;

	public void ExamplePerform()
	{
		Debug.Log(name + "이(가) 공격했음!");
	}
	
	public void Attacked()
	{
		Debug.Log(name + "이(가) 공격당함!!");
	}

	public void SetHP(int hp)
	{
		currentHP = Mathf.Clamp(hp, 0, maxHP);
	}

	public void SetMaxHP(int max)
	{
		maxHP = Mathf.Max(1, max);
		currentHP = Mathf.Min(currentHP, maxHP);
	}
}