using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreCharacter : MonoBehaviour
{
	[Header("Store Data")]
	[SerializeField] private StoreCharacterSO storeSO;
	[SerializeField] private TMP_Text titleText;
	[SerializeField] private Image characterImage;
	[SerializeField] private TMP_Text description;
	[SerializeField] private TMP_Text cost;
	[SerializeField] private bool isBought = false;

	[Header("Stat Data")]
	[SerializeField] private TMP_Text Attack;
	[SerializeField] private TMP_Text Defence;
	[SerializeField] private TMP_Text HP;

	private EntityAttributes characterAttributes;

	private void Start()
	{
		characterAttributes = storeSO.CharacterSO.Attributes;
		titleText.text = storeSO.CharacterSO.Name;
		characterImage.sprite = storeSO.CharacterSO.Image;
		description.text = storeSO.Description;
		cost.text = "$" + storeSO.Cost.ToString();

		Attack.text = characterAttributes.DamageModifier + "x";
		Defence.text = characterAttributes.DefenseModifier + "x";
		HP.text = characterAttributes.MaxHp.ToString();
	}
}
