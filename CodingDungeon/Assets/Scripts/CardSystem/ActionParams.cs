using System.Collections.Generic;

namespace CardSystem
{
    // Output Card Action Params
    public class OutputCardActionParams
    {
        public int power;
        public List<Character> targets;

        public OutputCardActionParams(int power, List<Character> targets)
        {
            this.power = power;
            this.targets = targets ?? new List<Character>();
        }
    }

    public class SimpleOutputCardParams : OutputCardActionParams
    {
        public SimpleOutputCardParams(int power, List<Character> targets) 
            : base(power, targets)
        {
        }
    }

    // Passive Card Action Params
    public class PassiveCardActionParams
    {
        // Passive 카드에 필요한 파라미터 추가
    }

    // Operation Card Action Params
    public class OperationCardActionParams
    {
        // Operation 카드에 필요한 파라미터 추가
    }
}

