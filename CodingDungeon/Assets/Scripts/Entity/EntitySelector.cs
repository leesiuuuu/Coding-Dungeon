using System;
using UnityEngine;

public class EntitySelector : SceneSingleMono<EntitySelector>
{
	[SerializeField] private string tagFilter;
	[SerializeField] private LayerMask layerFilter;
	private IEntity target;
	
	public bool GetEntity(out IEntity entity)
	{
		CastRay();
		entity = target;
		return target != null;
	}
	
	private void CastRay()
	{
		target = null;

		Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f, layerFilter);


		if (hit.collider != null && hit.collider.gameObject.CompareTag(tagFilter))
		{ 
			target = hit.collider.GetComponent<IEntity>();
		}

	}
}
