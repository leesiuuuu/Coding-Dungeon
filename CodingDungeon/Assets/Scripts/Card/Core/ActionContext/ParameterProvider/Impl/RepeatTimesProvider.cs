using UnityEngine;

public class RepeatTimesProvider : MonoBehaviour
{
	private int _repeatTimes = 1;

	public int RepeatTimes
	{
		get => _repeatTimes;
		set => _repeatTimes =  Mathf.Clamp(value, 1, 20);
	}
}