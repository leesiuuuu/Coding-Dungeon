using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterProfile : MonoBehaviour
{
	[Header("Profile UI")]
	[SerializeField] private PartySO partySO;
	[SerializeField] private TMP_Text title;
	[SerializeField] private Image profile;

	[Header("Stat UI")]
	[SerializeField] private TMP_Text Attack;
	[SerializeField] private TMP_Text Defence;
	[SerializeField] private TMP_Text HP;
	[SerializeField] private GameObject selectedObj;

	private CharacterSo character;
	private CharacterAttributes characterAttributes;
	private GameObject prefab;
	private bool isSelected = false;

	void Start()
	{
		character = partySO.character;
		prefab = partySO.Prefab;
		characterAttributes = character.Attributes;
		title.text = character.Name;
		profile.sprite = character.Image;
		Attack.text = characterAttributes.DamageModifier + "x";
		Defence.text = characterAttributes.DefenseModifier + "x";
		HP.text = characterAttributes.MaxHp.ToString();
	}

	public void OnClicked()
	{
		if (PartySelector.Instance.IsInclude(this))
		{
			PartySelector.Instance.Deselect(this);
			return;
		}

		if (PartySelector.Instance.IsFull())
		{
			PartySelector.Instance.Dequeue();
		}

		PartySelector.Instance.Enqueue(this);
	}

	public void Select()
	{
		isSelected = true;
		selectedObj.SetActive(isSelected);
	}

	public void Deselect()
	{
		isSelected = false;
		selectedObj.SetActive(isSelected);
	}

	public GameObject GetPrefab() => prefab;
}