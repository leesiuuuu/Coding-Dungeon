using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;

    public void AppearText(string value)
    {
        damageText.rectTransform.DOShakeAnchorPos(0.5f, 70, 20, 100).OnComplete(() =>
        {
            damageText.DOFade(0f, 0.5f).OnComplete(() => { Destroy(gameObject); });
        });
        damageText.text = value + " DMG";
    }
}
