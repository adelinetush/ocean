using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabRescue : MonoBehaviour
{
    //In level 2, player needs to rescue a crab in order to survive 
    //Will be renamed in future to take into account other sea life 
    [SerializeField] private Animator m_animator;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Crap is "rescued" on collision with player
        if (collision.collider.CompareTag("Player"))
        {
            m_animator.SetTrigger("crab_rescued");
            collision.collider.enabled = false;
            ScoreManager.ScoreManagerInstance.RescueComplete();
        }
    }
}
