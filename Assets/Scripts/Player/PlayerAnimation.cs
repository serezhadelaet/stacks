using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator = null;

        private static readonly int JumpTrigger = Animator.StringToHash("Jump");
        private static readonly int FinishedTrigger = Animator.StringToHash("Finished");
    
        public void Jump()
        {
            animator.SetTrigger(JumpTrigger);
        }

        public void Finished()
        {
            animator.SetTrigger(FinishedTrigger);
        }
    }
}