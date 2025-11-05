using DG.Tweening;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
	// 플레이어 이동 범위
	[SerializeField] private int range = 1;
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			Move(Vector3.up);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			Move(Vector3.down);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Move(Vector3.left);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Move(Vector3.right);
		}
	}

	/// <summary>
	/// 플레이어 / 적을 특정 방향으로 이동시키는 함수
	/// </summary>
	/// <param name="dir">방향 벡터</param>
	public void Move(Vector3 dir)
	{
		Vector3 dest = transform.position + dir;
		gameObject.transform.DOJump(dest, 0.5f, 1, 0.2f).SetEase(Ease.OutQuad);
	}
}
