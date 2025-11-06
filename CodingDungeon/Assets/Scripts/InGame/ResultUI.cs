using TMPro;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
	[SerializeField] private TMP_Text resultText;
	[SerializeField] private CanvasFader fader;

	private void Start()
	{
		TurnManager.Instance.OnWin += OnWin;
		TurnManager.Instance.OnLose += OnLose;
	}

	private void OnWin()
	{
		enableFader();
		resultText.text = "성공!";
		resultText.color = new Color(0.4125519f, 1f, 0.345098f, 0.682353f);
	}

	private void OnLose()
	{
		enableFader();
		resultText.text = "실패...";
		resultText.color = new Color(1f, 0.2421383f, 0.2739033f, 0.8862745f);
	}

	private void enableFader()
	{
		fader.gameObject.SetActive(true);
	}
}
