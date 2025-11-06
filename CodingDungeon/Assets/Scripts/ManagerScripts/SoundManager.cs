using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SingleMono<SoundManager>
{
	[SerializeField]
	private AudioMixer audioMixer;
	public void SFXPlay(string sfxName, AudioClip clip)
	{
		GameObject go = new GameObject(sfxName + "Sound");
		AudioSource source = go.AddComponent<AudioSource>();
		source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
		source.clip = clip;
		source.Play();

		StartCoroutine(DestroyAfterRealtime(go, clip.length));
	}
	public void BgFadeIn(AudioSource BgPlayer)
	{
		StartCoroutine(FadeIn(BgPlayer));
	}
	public void BgFadeInCustom(AudioSource BgPlayer, float volume, float time)
	{
		StartCoroutine(FadeIn(BgPlayer, volume, time));
	}
	public void BgFadeOut(AudioSource BgPlayer)
	{
		StartCoroutine(FadeOut(BgPlayer));
	}
	public void BgFadeOutCustom(AudioSource BgPlayer, float time)
	{
		StartCoroutine(FadeOut(BgPlayer, time));
	}
	private IEnumerator FadeIn(AudioSource BgPlayer)
	{
		float ElapsedTime = 0f;
		float Duration = 0.8f;
		float volume = BgPlayer.volume;
		while (ElapsedTime < Duration)
		{
			if (BgPlayer == null) yield break;
			ElapsedTime += Time.deltaTime;
			float t = ElapsedTime / Duration;
			BgPlayer.volume = Mathf.Lerp(volume, 0f, t);
			yield return null;
		}
	}
	private IEnumerator FadeIn(AudioSource BgPlayer, float v, float Time)
	{
		float ElapsedTime = 0f;
		float volume = BgPlayer.volume;
		while (ElapsedTime < Time)
		{
			ElapsedTime += UnityEngine.Time.unscaledDeltaTime;
			float t = ElapsedTime / Time;
			BgPlayer.volume = Mathf.Lerp(volume, v, t);
			yield return null;
		}
	}
	private IEnumerator FadeOut(AudioSource BgPlayer)
	{
		float ElapsedTime = 0f;
		float Duration = 0.8f;
		float volume = BgPlayer.volume;
		while (ElapsedTime < Duration)
		{
			ElapsedTime += Time.deltaTime;
			float t = ElapsedTime / Duration;
			BgPlayer.volume = Mathf.Lerp(volume, 1f, t);
			yield return null;
		}
	}
	private IEnumerator FadeOut(AudioSource BgPlayer, float ti)
	{
		float ElapsedTime = 0f;
		while (ElapsedTime < ti)
		{
			ElapsedTime += Time.deltaTime;
			float v = BgPlayer.volume;
			float t = ElapsedTime / ti;
			BgPlayer.volume = Mathf.Lerp(v, 1f, t);
			yield return null;
		}
	}
	private IEnumerator DestroyAfterRealtime(GameObject go, float delay)
	{
		yield return new WaitForSecondsRealtime(delay); // TimeScale 0이어도 기다림
		Destroy(go);
	}
}