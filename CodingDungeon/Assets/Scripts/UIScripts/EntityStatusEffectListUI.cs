using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class EntityStatusEffectListUI : MonoBehaviour
{
	public TextMeshProUGUI Text;

	[SerializeField]
	private GameObject _entity;

	public IEntity Entity => _entity.GetComponent<IEntity>();

	public void Update()
	{
		string list = "";
		foreach (var i in Entity.Status.CurrentEffects.ToList())
		{
			//string viewString = i.GetViewString();
			//if (!String.IsNullOrEmpty(viewString))
			//{
			//	list += "\n" + viewString;
			//}
		}
		Text.text = list;
	} 
}