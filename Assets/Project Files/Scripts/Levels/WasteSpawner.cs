using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteSpawner : MonoBehaviour
{
    //Prefab for waste 
    [SerializeField] private Collectible[] m_wasteObjects;

    //ax amount of waste to be spawned in a level
    [SerializeField] private int m_maxWasteAmount;

    private void Awake()
    {

        //Spawn waste when the game starts 
        SpawnWaste();
    }

    private void SpawnWaste()
    {
        //Get the screen bounds to determine where to spawn waste 
        Vector2 screenBounds = new Vector2(Screen.width, Screen.height);
        Vector2 worldBounds = Camera.main.ScreenToWorldPoint(screenBounds);

        //Spawn waste up to the max amount 
        for (int i = 0; i < m_maxWasteAmount; i++)
        {

            float randomX = Random.Range(-worldBounds.x, worldBounds.x);
            float randomY = Random.Range(-worldBounds.y, worldBounds.y -2);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0);

            //Random prefab from the list 
            int prefabIndex = Random.Range(0, m_wasteObjects.Length);
            Collectible wasteObject = Instantiate(m_wasteObjects[prefabIndex], randomPosition, Quaternion.identity);

            wasteObject.transform.SetParent(transform);

            //Random rotation 
            int randomRotation = Random.Range(0, 180);
            wasteObject.transform.Rotate(0,0,randomRotation);
        }
    }
}
