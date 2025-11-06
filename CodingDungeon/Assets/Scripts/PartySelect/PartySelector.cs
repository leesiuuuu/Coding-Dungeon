using System.Collections.Generic;
using UnityEngine;

public class PartySelector : SingleMono<PartySelector>
{
	[Header("Current Selection")]
	public List<CharacterProfile> selectedCharacters = new List<CharacterProfile>();
	public int MaxCharacter = 3;

	[Header("Data for Scene Transfer")]
	public List<PartySO> selectedPartyData = new List<PartySO>(); // 씬 전환용 데이터

	public void Enqueue(CharacterProfile profile)
	{
		profile.Select();
		selectedCharacters.Add(profile);

		// 데이터 저장 (씬 전환용)
		SavePartyData(profile);

		// CharacterSpawner에게 생성 요청
		CharacterSpawner.Instance?.SpawnCharacter(profile);
	}

	public void Dequeue() => Deselect(selectedCharacters[0]);

	public bool IsInclude(CharacterProfile profile) => selectedCharacters.Contains(profile);

	public void Deselect(CharacterProfile profile)
	{
		if (selectedCharacters.Count == 0) return;

		profile.Deselect();
		selectedCharacters.Remove(profile);

		// 데이터에서도 제거
		RemovePartyData(profile);

		// CharacterSpawner에게 삭제 요청
		CharacterSpawner.Instance?.DespawnCharacter(profile);
	}

	public bool IsFull()
	{
		return (selectedCharacters.Count >= MaxCharacter);
	}

	public bool IsEmpty()
	{
		return (selectedCharacters.Count == 0);
	}

	// 모든 캐릭터 해제 (필요시 사용)
	public void ClearAll()
	{
		while (selectedCharacters.Count > 0)
		{
			Deselect(selectedCharacters[0]);
		}
	}

	/// <summary>
	/// 선택된 캐릭터 데이터 저장 (씬 전환용)
	/// </summary>
	private void SavePartyData(CharacterProfile profile)
	{
		// CharacterProfile에서 PartySO 가져오기
		PartySO partySO = profile.GetComponent<CharacterProfile>()?.GetPartySO();

		if (partySO != null && !selectedPartyData.Contains(partySO))
		{
			selectedPartyData.Add(partySO);
			Debug.Log($"파티 데이터 저장: {partySO.character.Name}");
		}
	}

	/// <summary>
	/// 선택 해제된 캐릭터 데이터 제거
	/// </summary>
	private void RemovePartyData(CharacterProfile profile)
	{
		PartySO partySO = profile.GetComponent<CharacterProfile>()?.GetPartySO();

		if (partySO != null && selectedPartyData.Contains(partySO))
		{
			selectedPartyData.Remove(partySO);
			Debug.Log($"파티 데이터 제거: {partySO.character.Name}");
		}
	}

	/// <summary>
	/// 다음 씬에서 파티 데이터로 캐릭터 다시 생성
	/// </summary>
	public void RespawnPartyInNewScene()
	{
		if (CharacterSpawner.Instance == null)
		{
			Debug.LogWarning("CharacterSpawner가 씬에 없습니다.");
			return;
		}

		// 기존 참조 초기화
		selectedCharacters.Clear();

		// 저장된 데이터로 프리팹만 다시 생성
		foreach (PartySO partySO in selectedPartyData)
		{
			if (partySO != null && partySO.Prefab != null)
			{
				// 여기서는 CharacterProfile 없이 직접 프리팹 생성
				CharacterSpawner.Instance.SpawnCharacterFromData(partySO);
				Debug.Log($"씬 전환 후 캐릭터 재생성: {partySO.character.Name}");
			}
		}
	}

	/// <summary>
	/// 선택된 파티 데이터 가져오기
	/// </summary>
	public List<PartySO> GetSelectedPartyData()
	{
		return new List<PartySO>(selectedPartyData);
	}
}