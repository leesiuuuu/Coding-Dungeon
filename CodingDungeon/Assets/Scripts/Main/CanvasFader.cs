using DG.Tweening;
using UnityEngine;

public class CanvasFader : MonoBehaviour
{
	[SerializeField] private CanvasGroup canvasGroup;
	[SerializeField] private float duration;
	[SerializeField] private float delay;

	[SerializeField] private bool isDelay = false;

	public void FadeIn() => canvasGroup.DOFade(0f, duration).OnComplete(() => { gameObject.SetActive(false); });
	public void OnlyFadeIn() => canvasGroup.DOFade(0f, duration);
	public void FadeOut() => canvasGroup.DOFade(1f, duration);

	public void FadeInNext(GameObject nextObj)
	{
		canvasGroup.DOFade(0f, duration).OnComplete(() => { gameObject.SetActive(false); nextObj.SetActive(true); });
	}

	public void FadeOutDelay() => canvasGroup.DOFade(1f, duration).SetDelay(delay);

	private void OnEnable()
	{
		if (isDelay) FadeOutDelay();
		else FadeOut();
	}
}
