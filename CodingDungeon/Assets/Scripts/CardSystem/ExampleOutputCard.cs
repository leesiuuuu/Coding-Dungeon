using UnityEngine;
using CardSystem.Provider;

namespace CardSystem
{
    [CreateAssetMenu(fileName = "ExampleOutputCard", menuName = "Card System/Output Card/Example")]
    public class ExampleOutputCard : AbstractOutputCardSo<SimpleOutputCardParams>
    {
        [SerializeField] private float power = 1f;

        public float Power => power;

        public override void StartActionInternal(ProviderGroup providerGroup)
        {
            if (providerGroup == null || providerGroup.CharacterProvider == null)
            {
                Debug.LogWarning("ProviderGroup 또는 CharacterProvider가 없습니다.");
                return;
            }

            // CharacterProvider를 통해 타겟 캐릭터들을 가져옴
            var characters = providerGroup.CharacterProvider.Provide(null);
            var param = new SimpleOutputCardParams((int)power, characters);

            Debug.Log($"ExampleOutputCard 실행: Power={power}, Targets={param.targets.Count}");
            
            // 실제 데미지 처리 로직
            foreach (var target in param.targets)
            {
                if (target != null)
                {
                    target.TakeDamage(param.power);
                }
            }
        }
    }
}

