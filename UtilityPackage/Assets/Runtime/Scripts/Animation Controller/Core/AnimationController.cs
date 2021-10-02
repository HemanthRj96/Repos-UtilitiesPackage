﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace FickleFrames
{
    /// <summary>
    /// Inherit this class if manual animation update is required
    /// </summary>
    public class AnimationController : MonoBehaviour
    {

        #region EditorOnly
#if UNITY_EDITOR

        /// <summary>
        /// Method used only by editor do not call this method anywhere else
        /// </summary>
        public ref Dictionary<string, AnimationClip> GetClips()
        {
            return ref animationClipsLookup;
        }

#endif
        #endregion


        #region Internals

        // Editor fields
        [SerializeField] private string animationClipsSourcePath = "Assets/Animations/Clips/";
        [SerializeField] private string animatorControllerSavePath = "Assets/Animations/Controllers/";
        [SerializeField] private bool canEditController = false;

        [SerializeField] protected Animator animator;
        [SerializeField] private bool enableAutoUpdate = false;
        [SerializeField] private bool canUseAnimator = false;

        private Dictionary<string, AnimationClip> animationClipsLookup = new Dictionary<string, AnimationClip>();
        private string currentState;
        protected StateControllerComponent stateController;


        /// <summary>
        /// Bootstraps on start
        /// </summary>
        private void Awake()
        {
            bootStrapper();
        }


        /// <summary>
        /// Initializes values and data
        /// </summary>
        private void bootStrapper()
        {
            // Check if the animator controller exists and initialize it
            if (animator != null)
                canUseAnimator = true;

            stateController = gameObject.GetComponent<StateControllerComponent>();

            if (stateController == null)
                return;

            // Attach based on animation update
            if (enableAutoUpdate)
                stateController.AttachStateChangeEvent(autoAnimationUpdate);
            else
                stateController.AttachStateChangeEvent(manualAnimationUpdate);
        }


        /// <summary>
        /// Method to play an animation
        /// </summary>
        private void animationPlayer(string state)
        {
            if (canUseAnimator == false || currentState == state)
                return;

            animator.Play(state);
            currentState = state;
        }


        /// <summary>
        /// This function is automatically invoked if automatic animation update is set to true
        /// </summary>
        private void autoAnimationUpdate(string newState)
        {
            animationPlayer(newState);
        }


        #endregion Internals


        /// <summary>
        /// Override this method to extend functionality
        /// </summary>
        protected virtual void manualAnimationUpdate(string stateName)
        {

        }


        /// <summary>
        /// Method to play an animation
        /// </summary>
        /// <param name="newState">Target animation state to be played</param>
        public virtual void PlayAnimation(string newState)
        {
            animationPlayer(newState);
        }


        /// <summary>
        /// Plays animation if the condition is true
        /// </summary>
        /// <param name="condition">Should be true to play animation</param>
        /// <param name="newState">Target animation state to be played</param>
        public virtual void PlayAnimationIf(bool condition, string newState)
        {
            if (condition)
                animationPlayer(newState);
        }
    }
}