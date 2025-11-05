using UnityEngine;

namespace CardSystem
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private string characterName;
        [SerializeField] private int maxHP = 100;
        [SerializeField] private int currentHP = 100;

        public string CharacterName => characterName;
        public int MaxHP => maxHP;
        public int CurrentHP => currentHP;

        public void SetHP(int hp)
        {
            currentHP = Mathf.Clamp(hp, 0, maxHP);
        }

        public void TakeDamage(int damage)
        {
            SetHP(currentHP - damage);
        }

        public void Heal(int amount)
        {
            SetHP(currentHP + amount);
        }
    }
}

