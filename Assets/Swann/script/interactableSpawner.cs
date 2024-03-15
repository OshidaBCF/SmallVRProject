using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactableSpawner : MonoBehaviour
{
    public void SpawnInteractable(GameObject gameObject)
    {
        GameObject newGameObject = Instantiate(gameObject);
        newGameObject.transform.position = transform.position + new Vector3(0, 1, 0);
        newGameObject.transform.rotation = transform.rotation;
    }
}
