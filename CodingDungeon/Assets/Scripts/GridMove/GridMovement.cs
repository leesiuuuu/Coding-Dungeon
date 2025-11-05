using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
	[SerializeField] private bool isSelected = false;
	
	// 오브젝트 이동 함수
	public void Move(Vector3 dir)
	{
		if (!isSelected) return;

		Vector3 dest = transform.position + dir;
		transform.DOJump(dest, 0.5f, 1, 0.2f).SetEase(Ease.OutQuad);
	}
	public void Select()
	{
		isSelected = true;
	}
	public void Deselect()
	{
		isSelected = false;
	}
}
