//This script handles waste logic
using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    //When the player triggers the waste
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            ScoreManager.ScoreManagerInstance.CurrentScore += 1;
        }
    }
}
