using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CardSystem;

namespace UI
{
    public class CardUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image cardImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Button cardButton;

        private AbstractCardSo<object> cardData;
        private System.Action<CardUI> onCardClicked;

        public AbstractCardSo<object> CardData => cardData;

        private void Awake()
        {
            if (cardButton != null)
            {
                cardButton.onClick.AddListener(OnCardClicked);
            }
        }

        public void SetCardData(AbstractCardSo<object> card)
        {
            cardData = card;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (cardData == null)
            {
                gameObject.SetActive(false);
                return;
            }

            gameObject.SetActive(true);

            if (nameText != null)
            {
                nameText.text = cardData.Name;
            }

            if (descriptionText != null)
            {
                descriptionText.text = cardData.Description;
            }

            // 카드 이미지는 카드 데이터에 따라 설정
            // if (cardImage != null && cardData.Icon != null)
            // {
            //     cardImage.sprite = cardData.Icon;
            // }
        }

        public void SetOnCardClicked(System.Action<CardUI> callback)
        {
            onCardClicked = callback;
        }

        private void OnCardClicked()
        {
            onCardClicked?.Invoke(this);
        }

        public void SetInteractable(bool interactable)
        {
            if (cardButton != null)
            {
                cardButton.interactable = interactable;
            }
        }
    }
}

