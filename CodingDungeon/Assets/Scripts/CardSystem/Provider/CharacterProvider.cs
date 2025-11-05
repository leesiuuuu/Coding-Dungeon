using UnityEngine;
using System.Collections.Generic;

namespace CardSystem.Provider
{
    public class CharacterProvider : MonoBehaviour, IProvider<List<Character>, List<Character>>
    {
        [SerializeField] private List<Character> characters = new List<Character>();

        public List<Character> Provide(List<Character> param)
        {
            return characters;
        }

        public void AddCharacter(Character character)
        {
            if (!characters.Contains(character))
            {
                characters.Add(character);
            }
        }

        public void RemoveCharacter(Character character)
        {
            characters.Remove(character);
        }
    }
}

