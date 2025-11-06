using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : SingleMono<CharacterSpawner>
{
	[Header("Spawn Settings")]
	[SerializeField] private Transform spawnParent; // 프리팹이 생성될 부모 오브젝트
	[SerializeField] private Vector3 spawnOffset = new Vector3(2f, 0f, 0f); // 각 캐릭터 간 간격
	[SerializeField] private Vector3 basePosition = Vector3.zero; // 기준 위치
	[SerializeField] private Vector3 spawnAnimOffset = new Vector3(0f, 3f, 0f); // 애니메이션 시작 오프셋

	// CharacterProfile과 생성된 프리팹을 연결하는 딕셔너리
	private Dictionary<CharacterProfile, GameObject> spawnedPrefabs = new Dictionary<CharacterProfile, GameObject>();

	/// <summary>
	/// 캐릭터 프리팹 생성
	/// </summary>
	public void SpawnCharacter(CharacterProfile profile)
	{
		// 이미 생성되어 있다면 무시
		if (spawnedPrefabs.ContainsKey(profile))
		{
			Debug.LogWarning($"이미 생성된 캐릭터입니다: {profile.name}");
			return;
		}

		GameObject prefab = profile.GetPrefab();
		if (prefab == null)
		{
			Debug.LogError($"프리팹이 없습니다: {profile.name}");
			return;
		}

		// 최종 위치 계산
		Vector3 finalPosition = CalculateSpawnPosition(profile);
		// 애니메이션 시작 위치 (위쪽에서 시작)
		Vector3 startPosition = finalPosition + spawnAnimOffset;

		// 프리팹 생성 (시작 위치에서 생성)
		GameObject spawnedObj = Instantiate(prefab, startPosition, Quaternion.identity, spawnParent);
		spawnedPrefabs[profile] = spawnedObj;

		// 최종 위치로 이동 애니메이션
		spawnedObj.transform.DOMove(finalPosition, 0.3f).SetEase(Ease.OutBack);

		Debug.Log($"캐릭터 생성됨: {profile.name} at {finalPosition}");
	}

	/// <summary>
	/// 캐릭터 프리팹 삭제
	/// </summary>
	public void DespawnCharacter(CharacterProfile profile)
	{
		if (!spawnedPrefabs.ContainsKey(profile))
		{
			Debug.LogWarning($"생성되지 않은 캐릭터입니다: {profile.name}");
			return;
		}

		GameObject spawnedObj = spawnedPrefabs[profile];
		if (spawnedObj != null)
		{
			Destroy(spawnedObj);
			Debug.Log($"캐릭터 삭제됨: {profile.name}");
		}

		spawnedPrefabs.Remove(profile);

		// 남은 캐릭터들의 위치 재정렬
		RepositionAllCharacters();
	}

	/// <summary>
	/// 생성 위치 계산
	/// </summary>
	private Vector3 CalculateSpawnPosition(CharacterProfile profile)
	{
		int index = PartySelector.Instance.selectedCharacters.IndexOf(profile);

		Vector3 offset = spawnOffset * index;
		Vector3 finalPosition = spawnParent != null ?
			spawnParent.position + basePosition + offset :
			basePosition + offset;

		return finalPosition;
	}

	/// <summary>
	/// 모든 캐릭터 위치 재정렬
	/// </summary>
	private void RepositionAllCharacters()
	{
		List<CharacterProfile> selectedList = PartySelector.Instance.selectedCharacters;

		for (int i = 0; i < selectedList.Count; i++)
		{
			CharacterProfile profile = selectedList[i];

			if (spawnedPrefabs.ContainsKey(profile) && spawnedPrefabs[profile] != null)
			{
				Vector3 newPosition = spawnParent != null ?
					spawnParent.position + basePosition + (spawnOffset * i) :
					basePosition + (spawnOffset * i);

				spawnedPrefabs[profile].transform.DOMove(newPosition, 0.2f).SetEase(Ease.OutQuad);
			}
		}
	}

	/// <summary>
	/// 모든 생성된 프리팹 삭제
	/// </summary>
	public void DespawnAll()
	{
		foreach (var kvp in spawnedPrefabs)
		{
			if (kvp.Value != null)
			{
				Destroy(kvp.Value);
			}
		}
		spawnedPrefabs.Clear();
		Debug.Log("모든 캐릭터 삭제됨");
	}

	/// <summary>
	/// 특정 프리팹이 생성되어 있는지 확인
	/// </summary>
	public bool IsSpawned(CharacterProfile profile)
	{
		return spawnedPrefabs.ContainsKey(profile) && spawnedPrefabs[profile] != null;
	}

	/// <summary>
	/// 생성된 프리팹 가져오기
	/// </summary>
	public GameObject GetSpawnedPrefab(CharacterProfile profile)
	{
		return spawnedPrefabs.ContainsKey(profile) ? spawnedPrefabs[profile] : null;
	}
}