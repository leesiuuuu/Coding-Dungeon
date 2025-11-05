using System.Collections.Generic;
using UnityEngine;
using UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private IngameCardModel cardModel;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private CardUI cardPrefab;

    [Header("Profile UI")]
    [SerializeField] private UnityEngine.UI.Image profileImage;
    [SerializeField] private UnityEngine.UI.Image hpFillImage;
    [SerializeField] private Character character;

    private List<CardUI> cardUIList = new List<CardUI>();

    private void Start()
    {
        if (cardModel != null)
        {
            UpdateHandUI();
        }

        if (character != null)
        {
            UpdateProfileUI();
        }
    }

    private void OnEnable()
    {
        // 카드 모델 변경 시 UI 업데이트를 위한 이벤트 구독
        // 필요시 구현
    }

    private void OnDisable()
    {
        // 이벤트 구독 해제
    }

    public void UpdateHandUI()
    {
        if (cardModel == null || cardContainer == null)
        {
            return;
        }

        // 기존 카드 UI 제거
        foreach (var cardUI in cardUIList)
        {
            if (cardUI != null)
            {
                Destroy(cardUI.gameObject);
            }
        }
        cardUIList.Clear();

        // 새로운 카드 UI 생성
        foreach (var card in cardModel.Hand)
        {
            if (cardPrefab != null && card != null)
            {
                CardUI cardUI = Instantiate(cardPrefab, cardContainer);
                cardUI.SetCardData(card);
                cardUI.SetOnCardClicked(OnCardClicked);
                cardUIList.Add(cardUI);
            }
        }
    }

    private void OnCardClicked(CardUI cardUI)
    {
        if (cardUI?.CardData != null && cardModel != null)
        {
            cardModel.UseCard(cardUI.CardData);
            UpdateHandUI();
        }
    }

    public void UpdateProfileUI()
    {
        if (character == null)
        {
            return;
        }

        // HP 바 업데이트
        if (hpFillImage != null)
        {
            float hpPercentage = (float)character.CurrentHP / character.MaxHP;
            hpFillImage.fillAmount = hpPercentage;
        }

        // 프로필 이미지는 필요시 구현
        // if (profileImage != null && character.ProfileSprite != null)
        // {
        //     profileImage.sprite = character.ProfileSprite;
        // }
    }

    public void SetCardModel(IngameCardModel model)
    {
        cardModel = model;
        UpdateHandUI();
    }

    public void SetCharacter(Character targetCharacter)
    {
        character = targetCharacter;
        UpdateProfileUI();
    }
}

