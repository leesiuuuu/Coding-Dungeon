using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
	[SerializeField] AudioClip _audioClip;
	public void Move(Vector3 dir)
	{
		Vector3 dest = transform.position + dir;
		SoundManager.Instance.SFXPlay("SFX", _audioClip);
		transform.DOJump(dest, 0.5f, 1, 0.2f).SetEase(Ease.OutQuad);
	}
}
