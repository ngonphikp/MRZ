using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class SpineAnimation : StateMachineBehaviour
{
    public AnimationReferenceAsset animationAsset;
    public bool isLoop = false;

    private SkeletonAnimation anim;
    private SkeletonGraphic animGraphic;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim = animator.GetComponent<SkeletonAnimation>();
        if (anim != null)
        {
            if(animationAsset != null)
            {
                string name = animationAsset.Animation.Name;
                anim.state.SetAnimation(0, name, isLoop);
            }
            else
            {
                anim.AnimationState.ClearTracks();
                anim.Skeleton.SetToSetupPose();
            }
        }
        else
        {
            animGraphic = animator.GetComponentInChildren<SkeletonGraphic>();
            if (animationAsset != null)
            {
                animGraphic.AnimationState.SetAnimation(0, animationAsset.Animation.Name, isLoop);
            }
            else
            {
                animGraphic.AnimationState.ClearTracks();
                animGraphic.Skeleton.SetToSetupPose();
            }    
        }

        //Debug.Log(name);
        //anim.AnimationState.Event += HandleAnimationStateEvent;
    }

    //private void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
    //{
    //    Debug.Log($"Sounds/{e.Data.AudioPath.Replace(".wav", "")}");
    //    //SoundManagers.Instance.PlaySound(QuickFunction.getSound($"Sounds/{e.Data.AudioPath.Replace(".wav", "")}"));
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    anim.state.TimeScale = GameManager.instance.myTimeScale;
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    anim.AnimationState.Event -= HandleAnimationStateEvent;
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
