using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInHandUI : MonoBehaviour
{
	public AbstractCardSo Card { get; set; }
	
	[SerializeField]
	private TextMeshProUGUI _name;
	
	[SerializeField]
	private TextMeshProUGUI _description;

	[SerializeField]
	private TextMeshProUGUI _cost;

	[SerializeField]
	private Image _image;

	public event Action OnSelected;

	/// <summary>
	/// Called By UGUI Button
	/// </summary>
	public void OnClicked()
	{
		OnSelected?.Invoke();
	}
	
	public void UpdateInformation(AbstractCardSo card)
	{
		_name.text = card.Name;
		_description.text = card.Description;
		_cost.text = card.Cost.ToString();
		_image.sprite = card.Image;
	}
	
}