using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
	[SerializeField] private bool isSelected = false;
	[SerializeField] private GameObject selectTile;

	private void Update()
	{
		if (isSelected)
			showRange();
	}


	// 범위 보여주기 함수
	private void showRange()
	{
		Vector3 point = Input.mousePosition;
		point.z = Mathf.Abs(Camera.main.transform.position.z);
		point = Camera.main.ScreenToWorldPoint(point);

		Vector3 dir = (point - transform.position).normalized;

		int x = Mathf.RoundToInt(dir.x);
		int y = Mathf.RoundToInt(dir.y);

		Vector3Int dir1 = new Vector3Int(x, y, 0);

		selectTile.transform.position = transform.position + dir1;

		if (Input.GetMouseButtonDown(0))
		{
			Move(dir1);
		}
	}
	
	// 오브젝트 이동 함수
	public void Move(Vector3 dir)
	{
		selectTile.SetActive(false);
		Vector3 dest = transform.position + dir;

		// 추후 개발 시 OnComplete 제외하기
		// isSelected로 selectTile 관리
		transform.DOJump(dest, 0.5f, 1, 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
		{
			selectTile.SetActive(true);
		});
	}
}
