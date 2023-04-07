//Handle camera follow player 
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] protected Transform trackingTarget;
    [SerializeField] private float xOffset;

    [SerializeField] private float yOffset;

    void Update()
    {
        //check if tracking object has been assigned
        if (trackingTarget != null)
        {
            //only tracking X axis 
            transform.position = new Vector3(trackingTarget.position.x + xOffset, transform.position.y, transform.position.z);
        }
    }


}
