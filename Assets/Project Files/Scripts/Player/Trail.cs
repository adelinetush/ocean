using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Update()
    {
        spriteRenderer.enabled = transform.parent.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 0;
    }
}
