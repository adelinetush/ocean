using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteSpawner : MonoBehaviour
{
    //Prefab for waste 
    [SerializeField] private Collectible[] m_wasteObjects;

    //ax amount of waste to be spawned in a level
    [SerializeField] private int m_maxWasteAmount;


    //GRID STUFF
    //The size of the grid in each dimension
    private int gridSize = 10;

    // The spacing between grid cells
    private float gridSpacing = 2.0f;

    // A two-dimensional array to store occupied grid cells
    private GameObject[,] grid; 
    //GRID STUFF ENDS HERE

    private void Awake()
    {
        grid = new GameObject[gridSize, gridSize]; // Initialize the grid to be all null

        //Spawn waste when the game starts 
        for (int i = 0; i < m_maxWasteAmount; i++)
        {
            SpawnWasteGrid();
        }
    }

    private void SpawnWasteGrid()
    {
        int x, y;
        GameObject obj;

        // Keep generating random coordinates until an unoccupied cell is found
        do
        {
            // divide y by 2 so there are fewer grids than on the x since this is landscape
            x = Random.Range(0, gridSize);
            y = Random.Range(0, gridSize / 2);
            obj = grid[x, y];
        } while (obj != null); 

        // Calculate the position in world space
        Vector3 position = new Vector3(x * gridSpacing, y * gridSpacing, 0); 

        //Random prefab from the list 
        int prefabIndex = Random.Range(0, m_wasteObjects.Length);

        //so that there are objects on the negative cordinates as well
        Vector3 adjustedPosition = new Vector3(position.x - 8, position.y - 4, 0);

        // Spawn the object at the calculated position
        Collectible wasteObject = Instantiate(m_wasteObjects[prefabIndex], adjustedPosition, Quaternion.identity); 

        wasteObject.transform.SetParent(transform, false);

        //Random rotation 
        int randomRotation = Random.Range(0, 180);
        wasteObject.transform.Rotate(0,0,randomRotation);

        // Mark the grid cell as occupied
        grid[x, y] = wasteObject.gameObject; 
    }


    //the original function where spawned objects were more randomly placed and could overlap
    void SpawnWasteRandom()
    {
        //Get the screen bounds to determine where to spawn waste 
        Vector2 screenBounds = new Vector2(Screen.width, Screen.height);
        Vector2 worldBounds = Camera.main.ScreenToWorldPoint(screenBounds);

        ////Spawn waste up to the max amount 
        for (int i = 0; i < m_maxWasteAmount; i++)
        {

            float randomX = Random.Range(-worldBounds.x, worldBounds.x);
            float randomY = Random.Range(-worldBounds.y, worldBounds.y - 2);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0);

            //Random prefab from the list 
            int prefabIndex = Random.Range(0, m_wasteObjects.Length);
            Collectible wasteObject = Instantiate(m_wasteObjects[prefabIndex], randomPosition, Quaternion.identity);

            wasteObject.transform.SetParent(transform);

            //Random rotation 
            int randomRotation = Random.Range(0, 180);
            wasteObject.transform.Rotate(0, 0, randomRotation);
        }
    }
}
