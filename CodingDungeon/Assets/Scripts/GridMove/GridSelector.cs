using UnityEngine;

public class GridSelector : MonoBehaviour
{
	[SerializeField] private GridMovement gridMovement;
	private float selectRange = 0.8f;

	private void Update()
	{
		Vector3 mouseWorldPos = Input.mousePosition;
		mouseWorldPos.z = Mathf.Abs(Camera.main.transform.position.z);
		mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseWorldPos);

		// 선택된 유닛이 있을 때만 선택 타일 표시
		if (gridMovement != null)
		{
			Vector3 dir = (mouseWorldPos - gridMovement.transform.position).normalized;

			int dirX = (int)Mathf.Sign(dir.x);
			int dirY = (int)Mathf.Sign(dir.y);

			Vector3Int dir1 = new Vector3Int(dirX, dirY, 0);

			// 유닛 기준으로 방향 타일 표시
			transform.position = gridMovement.transform.position + (Vector3)dir1;
		}

		// 마우스 클릭 시
		if (Input.GetMouseButtonDown(0))
		{
			GridMovement grid = FindClosestGrid(mouseWorldPos);

			if (grid != null)
			{
				Debug.Log("선택됨");
				gridMovement = grid;
				gridMovement.Select();
			}
			else if (gridMovement != null)
			{
				Vector3 moveDir = (transform.position - gridMovement.transform.position).normalized;

				int _dirX = Mathf.RoundToInt(moveDir.x);
				int _dirY = Mathf.RoundToInt(moveDir.y);

				Vector3Int moveDir1 = new Vector3Int(_dirX, _dirY, 0);
				Debug.Log($"방향 벡터 : {moveDir}");
				Debug.Log($"최종 벡터 : {moveDir1}");

				gridMovement.Move(moveDir1);
				gridMovement.Deselect();
				gridMovement = null;
			}
		}
	}

	private GridMovement FindClosestGrid(Vector3 point)
	{
		GridMovement[] allGrids = FindObjectsOfType<GridMovement>();
		GridMovement closest = null;
		float minDist = float.MaxValue;

		foreach (GridMovement grid in allGrids)
		{
			float dist = Vector3.Distance(grid.transform.position, point);
			if (dist < minDist)
			{
				minDist = dist;
				closest = grid;
			}
		}

		// 🔸 범위 체크
		return (closest != null && minDist <= selectRange) ? closest : null;
	}
}
