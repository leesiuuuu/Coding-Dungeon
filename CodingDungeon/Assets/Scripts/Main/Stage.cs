using UnityEngine;

public class Stage : MonoBehaviour
{
	[SerializeField] private AudioClip bookSFX;
	[SerializeField] private AudioClip reverseBookSFX;
	private void OnEnable()
	{
		SoundManager.Instance.SFXPlay("BookSFX", bookSFX);
	}
	public void ReBook()
	{
		SoundManager.Instance.SFXPlay("ReBookSFX", reverseBookSFX);
	}
}
