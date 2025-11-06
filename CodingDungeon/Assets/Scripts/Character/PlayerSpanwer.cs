using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpanwer : MonoBehaviour
{
	[SerializeField] private Transform[] spawners;

	public void SpawnPlayers()
	{
		if (PartySelector.Instance == null)
		{
			Debug.LogError("PartySelector Instance가 없습니다!");
			return;
		}

		List<PartySO> list = PartySelector.Instance.selectedPartyData;

		if (list == null || list.Count == 0)
		{
			Debug.LogWarning("선택된 파티 데이터가 없습니다!");
			return;
		}

		// 기존 코드 그대로 유지
		for (int i = 0; i < list.Count; i++)
		{
			if (i >= spawners.Length)
			{
				Debug.LogWarning($"스포너 부족: {i}번째 캐릭터를 생성할 수 없습니다.");
				break;
			}

			var obj = Instantiate(list[i].InGamePrefab, spawners[i].position, Quaternion.identity);
			Character character = obj.GetComponent<Character>();

			if (character != null)
			{
				// 기존 코드 그대로 사용 가능!
				PartyManager.Instance.Characters.Add(character);
			}
			else
			{
				Debug.LogError($"Character 컴포넌트를 찾을 수 없습니다: {list[i].InGamePrefab.name}");
			}
		}

		Debug.Log($"플레이어 스폰 완료: {PartyManager.Instance.Characters.Count}명");
	}
}