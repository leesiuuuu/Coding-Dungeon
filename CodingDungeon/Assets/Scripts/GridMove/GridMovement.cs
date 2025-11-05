using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
	public void Move(Vector3 dir)
	{
		Vector3 dest = transform.position + dir;
		transform.DOJump(dest, 0.5f, 1, 0.2f).SetEase(Ease.OutQuad);
	}
}
