using UnityEngine;

namespace CardSystem
{
    public abstract class AbstractCardSo<T> : ScriptableObject where T : class
    {
        [SerializeField] protected string cardName;
        [SerializeField] [TextArea(3, 5)] protected string description;

        public string Name => cardName;
        public string Description => description;

        public abstract void StartAction(object param);
    }
}

