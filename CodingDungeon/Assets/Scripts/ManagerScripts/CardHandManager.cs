using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandSystem : MonoBehaviour
{
    [Header("카드 설정")]
    [SerializeField] private GameObject cardPrefab;
    
    [Header("포인트 설정")]
    [SerializeField] private float pointDistance = 150f; 
    [SerializeField] private Transform handCenter; 
    
    [Header("애니메이션 설정")]
    [SerializeField] private float slideInDuration = 0.5f;
    [SerializeField] private float slideInDistance = 300f; 
    [SerializeField] private AnimationCurve slideInCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    private List<GameObject> cards = new List<GameObject>();
    private List<Vector3> targetPositions = new List<Vector3>();
    
    private Coroutine repositionCoroutine;
    private bool isAddingCard = false;
    
    void Start()
    {
        if (handCenter == null)
            handCenter = transform;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddCard();
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearAllCards();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) RemoveCardAt(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) RemoveCardAt(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) RemoveCardAt(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) RemoveCardAt(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) RemoveCardAt(4);
    }

    public void AddCard()
    {
        if (cardPrefab == null)
        {
            Debug.LogError("Card Prefab이 설정되지 않았습니다!");
            return;
        }
        
        if (isAddingCard)
        {
            StartCoroutine(WaitAndAddCard());
            return;
        }
        
        isAddingCard = true;
        
        if (repositionCoroutine != null)
        {
            StopCoroutine(repositionCoroutine);
        }
        
        GameObject newCard = Instantiate(cardPrefab, handCenter);
        newCard.SetActive(false);
        cards.Add(newCard);
        
        RecalculatePositions();
        
        int cardIndex = cards.Count - 1;
        StartCoroutine(SlideInCard(newCard, cardIndex));
    }
    
    private IEnumerator WaitAndAddCard()
    {
        yield return new WaitUntil(() => !isAddingCard);
        AddCard();
    }
    
    private void RecalculatePositions()
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
    
    private IEnumerator SlideInCard(GameObject card, int index)
    {
        if (index >= targetPositions.Count) yield break;
        
        Vector3 targetPos = targetPositions[index];
        Vector3 startPos = targetPos + new Vector3(slideInDistance, 0, 0);
        
        card.transform.position = startPos;
        card.SetActive(true);
        
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
                card.transform.position = Vector3.Lerp(startPos, targetPos, curveValue);
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
    
    private IEnumerator RepositionExistingCards()
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
    
    public void RemoveCard(GameObject card)
    {
        if (cards.Contains(card))
        {
            cards.Remove(card);
            Destroy(card);
            RecalculatePositions();
            
            if (repositionCoroutine != null)
                StopCoroutine(repositionCoroutine);
            repositionCoroutine = StartCoroutine(RepositionExistingCards());
        }
    }
    
    public void RemoveCardAt(int index)
    {
        if (index >= 0 && index < cards.Count)
        {
            GameObject card = cards[index];
            cards.RemoveAt(index);
            Destroy(card);
            
            RecalculatePositions();
            
            if (repositionCoroutine != null)
                StopCoroutine(repositionCoroutine);
            repositionCoroutine = StartCoroutine(RepositionExistingCards());
        }
        else
        {
            Debug.LogWarning($"인덱스 {index}는 유효하지 않습니다. 현재 카드 수: {cards.Count}");
        }
    }
    
    public void ClearAllCards()
    {
        StopAllCoroutines();
        
        foreach (var card in cards)
        {
            if (card != null)
                Destroy(card);
        }
        
        cards.Clear();
        targetPositions.Clear();
        isAddingCard = false;
    }
    
    public int GetCardCount()
    {
        return cards.Count;
    }
    
    public GameObject GetCardAt(int index)
    {
        if (index >= 0 && index < cards.Count)
            return cards[index];
        return null;
    }
}