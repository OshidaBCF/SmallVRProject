using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anchorManager : MonoBehaviour
{
    List<GameObject> freeAttachPoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void released(GameObject releasedGameObject)
    {
        GameObject attachPoints = releasedGameObject.transform.GetChild(0).gameObject.transform.Find("AttachPoints").gameObject;

        GameObject closestReleased = null;
        GameObject closestToAttachTo = null;
        float distance = Mathf.Infinity;
        foreach (Transform attachPoint in attachPoints.transform)
        {
            Vector3 AP_Pos = attachPoint.position;
            
            if (freeAttachPoints.Count > 0)
            {
                foreach (GameObject freeAttachPoint in freeAttachPoints)
                {
                    if (!freeAttachPoint.transform.IsChildOf(attachPoints.transform))
                    {
                        if (Vector3.Distance(AP_Pos, freeAttachPoint.transform.position) < distance)
                        {
                            distance = Vector3.Distance(AP_Pos, freeAttachPoint.transform.position);
                            closestReleased = attachPoint.gameObject;
                            closestToAttachTo = freeAttachPoint;
                        }
                    }
                }
            }
            if (!freeAttachPoints.Contains(attachPoint.gameObject))
                freeAttachPoints.Add(attachPoint.gameObject);
        }
        if (distance < 0.2)
        {
            Vector3 movement = closestToAttachTo.transform.position - closestReleased.transform.position;
            releasedGameObject.transform.position += movement;
            Vector3 rotationVector = new Vector3(0, releasedGameObject.transform.rotation.y, 0);
            Debug.Log(Quaternion.Euler(rotationVector));
            releasedGameObject.transform.rotation = Quaternion.Euler(rotationVector);
            closestReleased.SetActive(false);
            closestToAttachTo.SetActive(false);
            freeAttachPoints.Remove(closestReleased);
            freeAttachPoints.Remove(closestToAttachTo);
        }
    }
}
