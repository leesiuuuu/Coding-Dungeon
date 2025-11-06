using System.Collections.Generic;
using UnityEngine;


public class UISoundBoard : MonoBehaviour
{
	public List<AudioClip> clips;

	public void SFXPlay(int i)
	{
		SoundManager.Instance.SFXPlay("SFX", clips[i]);
	}
}