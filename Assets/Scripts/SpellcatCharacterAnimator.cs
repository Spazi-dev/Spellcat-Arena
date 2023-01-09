using UnityEngine;

namespace EasyCharacterMovement.Examples.Animation.UnityCharacterAnimatorExample
{
    /// <summary>
    /// This example shows how to externally animate a ECM2 character based on its current movement mode and / or state.
    /// </summary>

    public sealed class SpellcatCharacterAnimator : MonoBehaviour
    {
        private static readonly int Movement = Animator.StringToHash("Movement");
        //private static readonly int Turn = Animator.StringToHash("Turn");

        [SerializeField]
        private Character _character;

        private bool _isCharacterNull;

        private void Update()
        {
            if (_isCharacterNull)
                return;

            float deltaTime = Time.deltaTime;

            // Get Character animator

            Animator animator = _character.GetAnimator();

            // Compute input move vector in local space

            Vector3 move = transform.InverseTransformDirection(_character.GetMovementDirection());

            // Update the animator parameters

            float forwardAmount = _character.useRootMotion && _character.GetRootMotionController()
                ? move.z
                : Mathf.InverseLerp(0.0f, _character.GetMaxSpeed(), _character.GetSpeed());

            animator.SetFloat(Movement, forwardAmount, 0.1f, deltaTime);
            animator.SetLayerWeight(1, animator.GetFloat(Movement)); //Set Run layer 

            //animator.SetFloat(Turn, Mathf.Atan2(move.x, move.z), 0.1f, deltaTime);

            //animator.SetBool(Ground, _character.IsGrounded());
            //animator.SetBool(Crouch, _character.IsCrouching());

            //if (_character.IsFalling())
            //    animator.SetFloat(Jump, _character.GetVelocity().y, 0.1f, deltaTime);

            // Calculate which leg is behind, so as to leave that leg trailing in the jump animation
            // (This code is reliant on the specific run cycle offset in our animations,
            // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)

            float runCycle = Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.2f, 1.0f);
            float jumpLeg = (runCycle < 0.5f ? 1.0f : -1.0f) * forwardAmount;

            //if (_character.IsGrounded())
            //    animator.SetFloat(JumpLeg, jumpLeg);
        }

        private void Start()
        {
            _isCharacterNull = _character == null;
        }
    }
}
