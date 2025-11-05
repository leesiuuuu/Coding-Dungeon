using DG.Tweening;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
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

	public void Move(Vector3 dir)
	{
		Vector3 dest = transform.position + dir;
		gameObject.transform.DOJump(dest, 0.5f, 1, 0.2f).SetEase(Ease.OutQuad);
	}
}
