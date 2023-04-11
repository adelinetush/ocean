using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float length, startPosition;
    [SerializeField] private GameObject camera;
    [SerializeField] private float parallaxEffect;

    private void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = camera.transform.position.x * (1 - parallaxEffect); 
        float dist = (camera.transform.position.x * parallaxEffect);

        transform.position = new Vector3 (startPosition + dist, transform.position.y, transform.position.z);
        if (temp > startPosition + length )
        {
            startPosition += length;
        } else if (temp < startPosition - length) { 
            startPosition -= length;
        }
    }
}
