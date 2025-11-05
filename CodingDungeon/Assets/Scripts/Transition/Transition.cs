using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
	[SerializeField] private Image panel;
	[SerializeField] private float fadeOutDuration = 0.5f;

	private void Start()
	{
		FadeOut(fadeOutDuration);
	}

	// 화면이 다시 밝게 변하는 함수
	public void FadeOut(float duration, float delay = 0f)
	{
		panel.DOFade(0f, duration).SetDelay(delay).OnComplete(() =>
		{
			panel.raycastTarget = false;
		});
	}

	// fadeOutDuration과 똑같은 시간으로 FadeIn을 하는 함수(UI OnClick에서 사용 가능)
	public void FadeIn(string sceneName)
	{
		FadeIn(sceneName, fadeOutDuration);
	}

	// 화면이 어둡게 변한 후 씬이 변경되는 함수
	public void FadeIn(string sceneName, float duration, float delay = 0f)
	{
		panel.raycastTarget = true;
		panel.DOFade(1f, duration).SetDelay(delay).OnComplete(() =>
		{
			SceneManager.LoadScene(sceneName);
		});
	}
}
