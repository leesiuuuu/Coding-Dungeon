using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
	[SerializeField] private AudioSource source;
	private void Start()
	{
		FadeOut();
	}

	public void FadeIn()
	{
		SoundManager.Instance.BgFadeIn(source);
	}
	
	public void FadeOut()
	{
		SoundManager.Instance.BgFadeOut(source);
	}
}
