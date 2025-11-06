using DG.Tweening;
using UnityEngine;

public class CanvasFader : MonoBehaviour
{
	[SerializeField] private CanvasGroup canvasGroup;
	[SerializeField] private float duration;

	public void FadeIn() => canvasGroup.DOFade(0f, duration).OnComplete(() => { gameObject.SetActive(false); });
	public void FadeOut() => canvasGroup.DOFade(1f, duration);

	public void FadeInNext(GameObject nextObj)
	{
		canvasGroup.DOFade(0f, duration).OnComplete(() => { gameObject.SetActive(false); nextObj.SetActive(true); });
	}

	private void OnEnable()
	{
		FadeOut();
	}
}
