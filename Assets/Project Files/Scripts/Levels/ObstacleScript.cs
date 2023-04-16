using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_targetRigidBody;
    [SerializeField] private HingeJoint2D m_joint;

    private void OnJointBreak2D()
    {
        Debug.Log("The joint broke");
        ScoreManager.ScoreManagerInstance.CurrentScore += 1;
        m_targetRigidBody.gameObject.GetComponent<Collider2D>().enabled = false;

        StartCoroutine(DestroyFishNets(m_targetRigidBody.gameObject));
    }

    IEnumerator DestroyFishNets(GameObject gameObject)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
