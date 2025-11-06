using UnityEngine;
using DG.Tweening;

public class StageCameraMove : MonoBehaviour
{
	[SerializeField] private float moveDuration = 1.5f;
	[SerializeField] private float targetZoom = 3f; // 확대할 크기 (작을수록 확대)
	[SerializeField] private Ease easeType = Ease.InOutSine;
	[SerializeField] private Vector2 offset;

	private Camera mainCamera;
	private float originalZoom;

	private void Awake()
	{
		if (mainCamera == null)
			mainCamera = Camera.main;

		originalZoom = mainCamera.orthographicSize;
	}

	public void MoveCamera(GameObject target)
	{
		MoveCameraTo(target.transform.position);
	}

	private void MoveCameraTo(Vector2 dest)
	{
		// 이동 애니메이션
		Vector3 vec = dest + offset;
		mainCamera.transform.DOMove(new Vector3(vec.x, vec.y, -10f), moveDuration).SetEase(easeType);

		// 줌(확대) 애니메이션
		mainCamera.DOOrthoSize(targetZoom, moveDuration).SetEase(easeType);
	}

	public void ResetCameraZoom()
	{
		mainCamera.DOOrthoSize(originalZoom, moveDuration).SetEase(easeType);
		mainCamera.transform.DOMove(new Vector3(0f, 0f, -10f), moveDuration).SetEase(easeType);
	}
}
