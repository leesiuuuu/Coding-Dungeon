
using System.Collections.Generic;
using UnityEngine;

public static class RandomCardProvider
{
	public static void Fill(DeckSo deck, AbstractCardSo[] hand)
    {
        // 현재 hand에 있는 output/passive 카드 수집
        HashSet<OutputCardSo> existingOutputCards = new HashSet<OutputCardSo>();
        HashSet<PassiveCardSo> existingPassiveCards = new HashSet<PassiveCardSo>();
        
        foreach (var card in hand)
        {
            if (card is OutputCardSo outputCard)
                existingOutputCards.Add(outputCard);
            else if (card is PassiveCardSo passiveCard)
                existingPassiveCards.Add(passiveCard);
        }
        
        // hand에 없는 output 카드 찾기
        List<OutputCardSo> missingOutputCards = new List<OutputCardSo>();
        foreach (var outputCard in deck.outputCards)
        {
            if (!existingOutputCards.Contains(outputCard))
                missingOutputCards.Add(outputCard);
        }
        
        // null 슬롯 채우기
        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i] == null)
            {
                // 우선: 빠진 output 카드가 있으면 채우기
                if (missingOutputCards.Count > 0)
                {
                    hand[i] = missingOutputCards[0];
                    existingOutputCards.Add(missingOutputCards[0]);
                    missingOutputCards.RemoveAt(0);
                }
                else
                {
                    // 랜덤 카드 선택 (중복 체크 포함)
                    hand[i] = ComputeRandomCard(deck, existingOutputCards, existingPassiveCards);
                }
            }
        }
    }

    private static AbstractCardSo ComputeRandomCard(DeckSo deck, 
        HashSet<OutputCardSo> existingOutputCards, 
        HashSet<PassiveCardSo> existingPassiveCards)
    {
        // 사용 가능한 카드 리스트 생성
        List<AbstractCardSo> availableCards = new List<AbstractCardSo>();
        
        // 중복되지 않은 output 카드 추가
        foreach (var card in deck.outputCards)
        {
            if (!existingOutputCards.Contains(card))
                availableCards.Add(card);
        }
        
        // operation 카드는 중복 가능하므로 모두 추가
        availableCards.AddRange(deck.operationCards);
        
        // 중복되지 않은 passive 카드 추가
        foreach (var card in deck.passiveCards)
        {
            if (!existingPassiveCards.Contains(card))
                availableCards.Add(card);
        }
        
        // 랜덤 선택
        if (availableCards.Count > 0)
        {
            int randomIndex = Random.Range(0, availableCards.Count);
            AbstractCardSo selectedCard = availableCards[randomIndex];
            
            // 선택된 카드를 사용 중 목록에 추가
            if (selectedCard is OutputCardSo outputCard)
                existingOutputCards.Add(outputCard);
            else if (selectedCard is PassiveCardSo passiveCard)
                existingPassiveCards.Add(passiveCard);
            
            return selectedCard;
        }
        
        return null;
    }
}