using Spine.Unity;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    //Controls player animation 

    [SerializeField] private SkeletonAnimation m_skeletonAnimation;

    [SerializeField] private bool m_canFlip;

    private void Update()
    {
        SetPlayerAnimations();
    }

    void SetPlayerAnimations()
    {
        //Player input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //set move animation
        if (verticalInput != 0.0f || horizontalInput != 0.0f)
        {
            m_skeletonAnimation.AnimationName = "anim_swim";
            m_skeletonAnimation.loop = true;
        }
        else
        {
            m_skeletonAnimation.AnimationName = "anim_idle";
            m_skeletonAnimation.loop = true;
        }

        //flip the character 

        if (m_canFlip)
        {
            if (horizontalInput < 0.0f)
            {
                m_skeletonAnimation.skeleton.ScaleX = -1;
            }
            else
            {
                m_skeletonAnimation.skeleton.ScaleX = 1;
            }
        }
    }
}
