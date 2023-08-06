using UnityEngine;

namespace HeathenEngineering.UX.Samples
{
    public class ToggleSetAnimatorBoolean : MonoBehaviour
    {
        public Animator Animator;
        public string BooleanName;

        public void SetBoolean(bool value)
        {
            Animator.SetBool(BooleanName, value);
        }
    }
}
