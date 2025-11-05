using UnityEngine;

[CreateAssetMenu(menuName="Coding Dungeon/Deck")]
public class DeckSo : ScriptableObject
{
	public OutputCardSo[] outputCards;
	public OperationCardSo[] operationCards;
	public PassiveCardSo[] passiveCards;
}