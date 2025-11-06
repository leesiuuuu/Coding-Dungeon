using UnityEngine;

public class EnemySelector : MonoBehaviour
{
	[SerializeField] private string tagFilter;
	[SerializeField] private LayerMask enemyLayer;
	private GameObject target;

	// 적 오브젝트를 반환(비어있을 시 null)
	public GameObject GetEnemy()
	{
		CastRay();
		return target;
	}
	private void CastRay()
	{
		target = null;

		Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f, enemyLayer);


		if (hit.collider != null && hit.collider.gameObject.CompareTag(tagFilter))
		{ 
			target = hit.collider.gameObject;
		}

	}
}
