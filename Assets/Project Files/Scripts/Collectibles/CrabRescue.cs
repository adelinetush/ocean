using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabRescue : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            m_animator.SetTrigger("crab_rescued");
            collision.collider.enabled = false;
            ScoreManager.ScoreManagerInstance.RescueComplete();
        }
    }
}
