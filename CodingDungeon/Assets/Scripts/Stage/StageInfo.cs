using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageInfo : MonoBehaviour
{
	[SerializeField] private TMP_Text stageID;
	[SerializeField] private TMP_Text stageTitle;
	[SerializeField] private Transform enemyContainer;
	[SerializeField] private GameObject imageObj;
	public void EnableStageInfo(StageSO stageSO)
	{
		gameObject.transform.parent.gameObject.SetActive(true);
		stageID.text = stageSO.StageID;
		stageTitle.text = stageSO.StageName;

		RemoveAll();

		for (int i = 0; i < stageSO.AppearEnemys.Length; i++)
		{
			var obj = Instantiate(imageObj, enemyContainer);
			obj.GetComponent<Image>().sprite = stageSO.AppearEnemys[i];
		}
	}

	public void RemoveAll()
	{
		if (enemyContainer.childCount > 0)
		{
			foreach (Transform child in enemyContainer)
			{
				Destroy(child.gameObject);
			}
		}
	}

}
