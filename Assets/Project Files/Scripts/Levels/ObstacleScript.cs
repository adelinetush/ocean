using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_targetRigidBody;
    [SerializeField] private HingeJoint2D m_joint;

    //This handles the traps/nets in level 2 
    //basically applying a certain amount of force breaks the joint 
    private void OnJointBreak2D()
    {
        ScoreManager.ScoreManagerInstance.CurrentScore += 1;
        m_targetRigidBody.gameObject.GetComponent<Collider2D>().enabled = false;

        //Destroy the nets some time after they break/fall off the hinges
        StartCoroutine(DestroyFishNets(m_targetRigidBody.gameObject));
    }

    IEnumerator DestroyFishNets(GameObject gameObject)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
