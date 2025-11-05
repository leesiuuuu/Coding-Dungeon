using UnityEngine;

namespace CardSystem.Provider
{
    public class ProviderGroup : MonoBehaviour
    {
        [SerializeField] private CharacterProvider characterProvider;

        public CharacterProvider CharacterProvider => characterProvider;

        private void Awake()
        {
            if (characterProvider == null)
            {
                characterProvider = GetComponent<CharacterProvider>();
                if (characterProvider == null)
                {
                    characterProvider = gameObject.AddComponent<CharacterProvider>();
                }
            }
        }
    }
}

