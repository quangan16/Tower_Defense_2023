using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGround : MonoBehaviour
{
    public List<GameObject> aboveObjs = new List<GameObject>();
    public int x, y;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hero") /*&& !GameManager.Instance.isFighting*/)
        {
            aboveObjs.Add(other.gameObject);
            if(aboveObjs.Count > 0)
            {
                UpdatePathFinding();
                PathFinding.Instance.ChangeValueOne();
                PathFinding.Instance.UpdatePath();
            }  
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hero") /*&& !GameManager.Instance.isFighting*/)
        {
            aboveObjs.Remove(other.gameObject);
            if (aboveObjs.Count == 0)
            {
                UpdatePathFinding();
                PathFinding.Instance.ChangeValueZero();
                PathFinding.Instance.UpdatePath();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Hero") /*&& !GameManager.Instance.isFighting*/)
        {
            DeActiveUpdateTarget();
        }
    }

    public void DeActiveUpdateTarget()
    {
        for (int i = 0; i < aboveObjs.Count; i++)
        {
            if (!aboveObjs[i].activeSelf)
            {
                aboveObjs.Remove(aboveObjs[i]);
            }
        }
    }
    public void UpdatePathFinding()
    {
/*        PathFinding.Instance.xValue = (int)transform.localPosition.x;
        PathFinding.Instance.yValue = (int)transform.localPosition.z;*/

        PathFinding.Instance.xValue = x;
        PathFinding.Instance.yValue = y;
    }
}
