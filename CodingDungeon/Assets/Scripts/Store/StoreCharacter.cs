using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
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
	[SerializeField] private GameObject boughtPanel;
	[SerializeField] private GameObject sureBuyPanel;

	[Header("Stat Data")]
	[SerializeField] private TMP_Text Attack;
	[SerializeField] private TMP_Text Defence;
	[SerializeField] private TMP_Text HP;

	private EntityAttributes characterAttributes;

	private void Start()
	{
		characterAttributes = storeSO.CharacterSO.character.Attributes;
		titleText.text = storeSO.CharacterSO.character.Name;
		characterImage.sprite = storeSO.CharacterSO.character.Image;
		description.text = storeSO.Description;
		cost.text = "$" + storeSO.Cost.ToString();

		Attack.text = characterAttributes.DamageModifier + "x";
		Defence.text = characterAttributes.DefenseModifier + "x";
		HP.text = characterAttributes.MaxHp.ToString();

		if (isBought)
		{
			boughtPanel.SetActive(true);
		}
	}

	private void Update()
	{
		if (isBought)
		{
			boughtPanel.SetActive(true);
		}
	}

	public void SureBuy()
	{
		if(sureBuyPanel.activeSelf)
		{
			GetComponent<RectTransform>().DOScaleX(0f, 0.125f).OnComplete(() =>
			{
				sureBuyPanel.SetActive(false);
				GetComponent<RectTransform>().DOScaleX(1f, 0.125f);
			});
		}
		else
		{
			GetComponent<RectTransform>().DOScaleX(0f, 0.125f).OnComplete(() =>
			{
				sureBuyPanel.SetActive(true);
				GetComponent<RectTransform>().DOScaleX(1f, 0.125f);
			});
		}
	}

	public void Yes()
	{
		if (PlayerDataManager.Instance.SpendCoin(storeSO.Cost))
		{
			sureBuyPanel.SetActive(false);
			isBought = true;
			PlayerDataManager.Instance.AddCharacter(storeSO.CharacterSO);
		}
		else
		{
			Debug.Log("돈이 없잖아");
		}
	}

	public void No()
	{
		GetComponent<RectTransform>().DOScaleX(0f, 0.125f).OnComplete(() =>
		{
			sureBuyPanel.SetActive(false);
			GetComponent<RectTransform>().DOScaleX(1f, 0.125f);
		});
	}

	public PartySO GetSO() => storeSO.CharacterSO;

	public void SetBought(bool value)
	{
		isBought = value;
	}
}
