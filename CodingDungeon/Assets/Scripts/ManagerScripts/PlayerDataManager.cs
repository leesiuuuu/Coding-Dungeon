using System.IO;
using System.Linq;
using UnityEngine;

public class PlayerDataManager : SingleMono<PlayerDataManager>
{
	private PlayerData data;
	public PartySO[] CurrentCharacter; // 현재 보유 캐릭터
	public int Coin;

	[Header("Default Settings")]
	[SerializeField] private PartySO[] defaultCharacters; // C, Java, Python (Inspector에서 설정)
	[SerializeField] private int defaultCoin = 0;

	private string SaveFilePath => Application.persistentDataPath + "/PlayerData.json";

	protected override void Awake()
	{
		base.Awake();
		DontDestroyOnLoad(gameObject);
		LoadData(); // 게임 시작 시 데이터 로드
	}

	private void Start()
	{
		// 저장된 데이터가 없을 때만 초기화
		if (data.CharacterList == null || data.CharacterList.Count == 0)
		{
			InitializeData();
		}
	}

	/// <summary>
	/// 초기 데이터 설정
	/// </summary>
	private void InitializeData()
	{
		data = new PlayerData();

		// 기본값 설정
		if (defaultCharacters != null && defaultCharacters.Length > 0)
		{
			data.CharacterList = defaultCharacters.ToList();
			CurrentCharacter = defaultCharacters;
		}
		else
		{
			data.CharacterList = CurrentCharacter.ToList();
		}

		data.Coin = defaultCoin;
		Coin = defaultCoin;

		SaveData();
		Debug.Log($"초기 데이터 생성 완료 - 캐릭터: {data.CharacterList.Count}명, 코인: {Coin}");
	}

	/// <summary>
	/// 데이터 저장
	/// </summary>
	public void SaveData()
	{
		data = new PlayerData();
		data.CharacterList = CurrentCharacter.ToList();
		data.Coin = Coin;

		string json = JsonUtility.ToJson(data, true); // true = 보기 좋게 포맷

		try
		{
			File.WriteAllText(SaveFilePath, json);
			Debug.Log($"데이터 저장 완료: {SaveFilePath}");
		}
		catch (System.Exception e)
		{
			Debug.LogError($"데이터 저장 실패: {e.Message}");
		}
	}

	/// <summary>
	/// 데이터 로드
	/// </summary>
	public void LoadData()
	{
		if (File.Exists(SaveFilePath))
		{
			try
			{
				string json = File.ReadAllText(SaveFilePath);
				data = JsonUtility.FromJson<PlayerData>(json);

				// 로드한 데이터를 현재 변수에 적용
				CurrentCharacter = data.CharacterList.ToArray();
				Coin = data.Coin;

				Debug.Log($"데이터 로드 완료: 캐릭터 {CurrentCharacter.Length}명, 코인 {Coin}");
			}
			catch (System.Exception e)
			{
				Debug.LogError($"데이터 로드 실패: {e.Message}");
				InitializeData(); // 로드 실패 시 초기화
			}
		}
		else
		{
			Debug.Log("저장된 데이터가 없습니다. 초기 데이터를 생성합니다.");
			data = new PlayerData();
		}
	}

	/// <summary>
	/// 데이터 삭제 (초기화)
	/// </summary>
	public void DeleteData()
	{
		if (File.Exists(SaveFilePath))
		{
			File.Delete(SaveFilePath);
			Debug.Log("저장된 데이터 삭제 완료");
		}
		InitializeData(); // 기본값으로 초기화
	}

	/// <summary>
	/// 게임 데이터 완전 초기화 (개발/테스트용)
	/// </summary>
	public void ResetToDefault()
	{
		DeleteData();
		Debug.Log("게임 데이터를 기본값으로 초기화했습니다.");
	}

	/// <summary>
	/// 코인 추가
	/// </summary>
	public void AddCoin(int amount)
	{
		Coin += amount;
		SaveData();
		Debug.Log($"코인 추가: +{amount}, 현재: {Coin}");
	}

	/// <summary>
	/// 코인 사용
	/// </summary>
	public bool SpendCoin(int amount)
	{
		if (Coin >= amount)
		{
			Coin -= amount;
			SaveData();
			Debug.Log($"코인 사용: -{amount}, 현재: {Coin}");
			return true;
		}
		else
		{
			Debug.LogWarning("코인이 부족합니다.");
			return false;
		}
	}

	/// <summary>
	/// 캐릭터 추가
	/// </summary>
	public void AddCharacter(PartySO character)
	{
		if (!CurrentCharacter.Contains(character))
		{
			CurrentCharacter = CurrentCharacter.Append(character).ToArray();
			SaveData();
			Debug.Log($"캐릭터 추가: {character.character.Name}");
		}
	}
}