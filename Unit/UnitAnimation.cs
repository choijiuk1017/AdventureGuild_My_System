using System;
using UnityEngine;

namespace Core.Unit
{
    public enum UnitAnimationType
    {
        Move, 
    }
    
    public class UnitAnimation : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void SetAnimation(string aniName)
        {
            animator.Play(aniName);
        }

        public void Flip()
        {
            
        }
    }
}
