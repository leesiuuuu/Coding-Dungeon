using UnityEngine;

public class Character : MonoBehaviour
{
	public void ExamplePerform()
	{
		Debug.Log(name + "이(가) 공격했음!");
	}
	
	public void Attacked()
	{
		Debug.Log(name + "이(가) 공격당함!!");
	}
}