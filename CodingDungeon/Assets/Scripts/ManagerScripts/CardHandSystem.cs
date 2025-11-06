using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CardHandSystem : MonoBehaviour
{
    [Header("카드 설정")]
    [SerializeField] protected GameObject cardPrefab;
    
    [Header("포인트 설정")]
    [SerializeField] protected float pointDistance = 150f; 
    [SerializeField] protected Transform handCenter; 
    
    [Header("애니메이션 설정")]
    [SerializeField] protected float slideInDuration = 0.2f;
    [SerializeField] protected float slideInDistance = 300f; 
    [SerializeField] protected AnimationCurve slideInCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("사운드")]
    [SerializeField] private AudioClip cardAdd;
    [SerializeField] private AudioClip cardRemove;
    
    protected Character character;
    
    protected List<CardInHandUI> cards = new();
    protected List<Vector3> targetPositions = new();
    
    protected Coroutine repositionCoroutine;
    protected bool isAddingCard = false;
    
    protected void Start()
    {
        if (handCenter == null)
            handCenter = transform;

        PlayerPortraitListUI.Instance.OnSelected += OnCharacterSelected;
    }

    protected virtual void OnCharacterSelected(Character selected)
    {
        Debug.Log(gameObject.name);
        if (character != null)
        {
            character.CardHolder.OnCardAdded -= AddCard;
            character.CardHolder.OnCardRemoved -= RemoveCardAt;
        }
        
        character = selected;
        UpdateCards(character.CardHolder.Hand);

        character.CardHolder.OnCardAdded += AddCard;
        character.CardHolder.OnCardRemoved += RemoveCardAt;
    }

    protected  void OnCardSelected(CardInHandUI selected)
    {
        character.CardHolder.AddCardAtActiveQueue(Array.IndexOf(cards.ToArray(), selected), selected.Card);
    }
    
    protected void OnCardDeleted(CardInHandUI deleted)
    {
        character.CardHolder.RemoveCardAtActiveQueue(Array.IndexOf(cards.ToArray(), deleted));
    }

    public void UpdateCards(List<AbstractCardSo> cardParam)
    {
        ClearAllCards();
        foreach (var card in cardParam)
        {
            AddCard(card);
        }
    }

    public void AddCard(AbstractCardSo card)
    {
        if (cardPrefab == null)
        {
            Debug.LogError("Card Prefab이 설정되지 않았습니다!");
            return;
        }
        
        if (isAddingCard)
        {
//            SoundManager.Instance.SFXPlay("CardAdd", cardAdd);
            StartCoroutine(WaitAndAddCard(card));
            return;
        }
        
        isAddingCard = true;
        
        if (repositionCoroutine != null)
        {
            StopCoroutine(repositionCoroutine);
        }
        CardInHandUI cardInHandUI = InstantiateCard(card);
        
        RecalculatePositions();
        
        int cardIndex = cards.Count - 1;
        StartCoroutine(SlideInCard(cardInHandUI.transform, cardIndex));
    }

    protected CardInHandUI InstantiateCard(AbstractCardSo card)
    {
        CardInHandUI cardInHandUI = Instantiate(cardPrefab, handCenter).GetComponent<CardInHandUI>();
        cardInHandUI.gameObject.SetActive(false);
        cardInHandUI.UpdateInformation(card);
        cards.Add(cardInHandUI);
        cardInHandUI.OnSelected += () => OnCardSelected(cardInHandUI);
        cardInHandUI.OnDeleted += () => OnCardDeleted(cardInHandUI);
        return cardInHandUI;
    }
    
    private IEnumerator WaitAndAddCard(AbstractCardSo card)
    {
        yield return new WaitUntil(() => !isAddingCard);
        AddCard(card);
    }
    
    protected virtual void RecalculatePositions()
    {
        targetPositions.Clear();
        
        int cardCount = cards.Count;
        float totalWidth = (cardCount - 1) * pointDistance;
        float startX = -totalWidth / 2f;
        
        for (int i = 0; i < cardCount; i++)
        {
            Vector3 pos = handCenter.position + new Vector3(startX + (i * pointDistance), 0, 0);
            targetPositions.Add(pos);
        }
    }
    
    protected virtual IEnumerator SlideInCard(Transform card, int index)
    {
        if (index >= targetPositions.Count) yield break;
        
        Vector3 targetPos = targetPositions[index];
        Vector3 startPos = targetPos + new Vector3(slideInDistance, 0, 0);
        
        card.position = startPos;
        card.gameObject.SetActive(true);
        
        float elapsed = 0f;
        
        List<Vector3> oldPositions = new List<Vector3>();
        for (int i = 0; i < cards.Count - 1; i++)
        {
            if (cards[i] != null)
                oldPositions.Add(cards[i].transform.position);
        }
        
        while (elapsed < slideInDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideInDuration;
            float curveValue = slideInCurve.Evaluate(t);
            
            if (index < targetPositions.Count)
            {
                targetPos = targetPositions[index];
                card.position = Vector3.Lerp(startPos, targetPos, curveValue);
            }

            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i] != null && i < targetPositions.Count && i < oldPositions.Count)
                {
                    cards[i].transform.position = Vector3.Lerp(oldPositions[i], targetPositions[i], curveValue);
                }
            }
            
            yield return null;
        }
        
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != null && i < targetPositions.Count)
            {
                cards[i].transform.position = targetPositions[i];
            }
        }
        
        isAddingCard = false;
    }
    
    protected virtual IEnumerator RepositionExistingCards()
    {
        float duration = 0.3f;
        float elapsed = 0f;
        
        List<Vector3> startPositions = new List<Vector3>();
        foreach (var card in cards)
        {
            if (card != null)
                startPositions.Add(card.transform.position);
        }
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float curveValue = slideInCurve.Evaluate(t);
            
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i] != null && i < targetPositions.Count && i < startPositions.Count)
                {
                    cards[i].transform.position = Vector3.Lerp(startPositions[i], targetPositions[i], curveValue);
                }
            }
            
            yield return null;
        }

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != null && i < targetPositions.Count)
            {
                cards[i].transform.position = targetPositions[i];
            }
        }
    }
    
    public void RemoveCardAt(int index)
    {
        if (index >= 0 && index < cards.Count)
        {
            CardInHandUI cardInHandUI = cards[index];
            DestroyCard(cardInHandUI);
            
            RecalculatePositions();
            
            if (repositionCoroutine != null)
                StopCoroutine(repositionCoroutine);
            repositionCoroutine = StartCoroutine(RepositionExistingCards());
        }
    }
    
    public void ClearAllCards()
    {
        StopAllCoroutines();

        for (int i = cards.Count - 1; i >= 0; i--)
        {
            if (cards[i] != null)
                DestroyCard(cards[i]);
        }
        
        targetPositions.Clear();
        isAddingCard = false;
    }

    protected void DestroyCard(CardInHandUI card)
    {
//		SoundManager.Instance.SFXPlay("CardRemove", cardRemove);
		Destroy(card.gameObject);
        cards.Remove(card);
    }
    
    public int GetCardCount()
    {
        return cards.Count;
    }
    
    public CardInHandUI GetCardAt(int index)
    {
        if (index >= 0 && index < cards.Count)
            return cards[index];
        return null;
    }
}