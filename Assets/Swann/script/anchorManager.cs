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
            if (attachPoint.gameObject.activeSelf)
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
        }

        Debug.Log(distance);
        if (distance < 1)
        {
            Quaternion rotator = Quaternion.FromToRotation(closestReleased.transform.forward, -closestToAttachTo.transform.forward);

            releasedGameObject.transform.rotation = rotator;

            Vector3 movement = closestToAttachTo.transform.position - closestReleased.transform.position;
            releasedGameObject.transform.position += movement;
            Debug.Log(rotator);


            /*
            closestReleased.SetActive(false);
            closestToAttachTo.SetActive(false);
            freeAttachPoints.Remove(closestReleased);
            freeAttachPoints.Remove(closestToAttachTo);
            */
        }
        /*
        float tempY = releasedGameObject.transform.rotation.eulerAngles.y;

        releasedGameObject.transform.rotation = Quaternion.Euler(0, tempY, 0); */
    }
}
