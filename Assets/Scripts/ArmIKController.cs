using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))] 

public class ArmIKController : MonoBehaviour
{   
    protected Animator animator;
    
    public bool ikActive = false;
    public Transform rightHandTarget = null;
    public Transform leftHandTarget;

    void Start () 
    {
        animator = GetComponent<Animator>();
    }
    
    //a callback for calculating IK
    void OnAnimatorIK()
    {
    	Debug.Log("IK Called");

        if(animator) {
            
            //if the IK is active, set the position and rotation directly to the goal. 
            if(ikActive) {
                if(leftHandTarget != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);  
                    animator.SetIKPosition(AvatarIKGoal.LeftHand,leftHandTarget.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand,leftHandTarget.rotation);
                }        
                
            }
            
            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {          
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,0); 
                animator.SetLookAtWeight(0);
            }
        }
    }    
}
