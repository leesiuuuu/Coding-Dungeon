using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class TileSelector : MonoBehaviour
{
	[SerializeField] private LayerMask tilemapLayer;
	[SerializeField] private Color selectedColor = Color.red;
	[SerializeField] private Color defaultColor = Color.white;
	[SerializeField] private GameObject selectTile;

	// 타일이 선택될 때 실행할 이벤트
	[SerializeField] private UnityEvent<Tilemap> onTileSelected;

	// 현재 선택된 타일
	private Tilemap currentTilemap;
	private Vector3Int lastSelectedCell;

	void Update()
	{
		Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		int x = Mathf.RoundToInt(point.x);
		int y = Mathf.RoundToInt(point.y);
		selectTile.transform.position = new Vector3(x, y, 0);

		transform.position = point;

		// 마우스 위치 기준으로 콜라이더 찾기
		Collider2D col = Physics2D.OverlapPoint(point, tilemapLayer);

		if (Input.GetMouseButton(0))
		{
			if (col != null)
			{
				Tilemap tilemap = col.GetComponent<Tilemap>();
				if (tilemap != null)
				{
					// 마우스 위치를 셀 좌표로 변환
					Vector3Int cellPos = tilemap.WorldToCell(point);

					// 타일이 실제로 존재하는지 확인
					if (tilemap.GetTile(cellPos) != null)
					{
						//Debug.Log($"타일 선택됨! 좌표: {cellPos}");

						//if (currentTilemap != null)
						//	//onTileSelected?.Invoke(currentTilemap);

						// 새 타일 색 변경
						//tilemap.SetColor(cellPos, selectedColor);

						// 현재 선택 상태 저장
						currentTilemap = tilemap;
						lastSelectedCell = cellPos;
					}
				}
			}
		}
	}

	// 예시 함수
	// 클릭 시 타일 컬러 변경
	//public void OnTileSelected(Tilemap tile)
	//{
	//	tile.SetColor(lastSelectedCell, defaultColor);
	//}

}
