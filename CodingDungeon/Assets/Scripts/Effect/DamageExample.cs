using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageExample : MonoBehaviour
{
	public TMP_InputField Field;
	public Button Btn;
	public GameObject Prefab;

	public void Appear()
	{
		var a = Instantiate(Prefab);
		a.GetComponent<DamageText>().AppearText(Field.text);
	}
}
