using CardSystem.Provider;

namespace CardSystem
{
    // Output Card
    public abstract class AbstractOutputCardSo<T> : AbstractCardSo<T> where T : OutputCardActionParams
    {
        public override void StartAction(object param)
        {
            if (param is ProviderGroup providerGroup)
            {
                StartActionInternal(providerGroup);
            }
        }

        public abstract void StartActionInternal(ProviderGroup providerGroup);
    }

    // Operation Card
    public abstract class AbstractOperationCardSo<T> : AbstractCardSo<T> where T : OperationCardActionParams
    {
        public override void StartAction(object param)
        {
            if (param is T operationParams)
            {
                StartActionInternal(operationParams);
            }
        }

        public abstract void StartActionInternal(T param);
    }

    // Passive Card
    public abstract class AbstractPassiveCardSo<T> : AbstractCardSo<T> where T : PassiveCardActionParams
    {
        public override void StartAction(object param)
        {
            if (param is T passiveParams)
            {
                StartActionInternal(passiveParams);
            }
        }

        public abstract void StartActionInternal(T param);
    }
}

