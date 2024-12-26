using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMaterial : MonoBehaviour
{
    public MaterialType materialType;
    public BoxCollider boxCollider;
    public Rigidbody rigidbody;
    public int Size;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(GoToInventory(other.gameObject));
        }
    }

    private IEnumerator GoToInventory(GameObject player)
    {
        yield return new WaitForSeconds(1.5f); // start at time X
        float startTime = 5; // Time.time contains current frame time, so remember starting point
        Destroy(boxCollider);
        Destroy(rigidbody);
        while (startTime >=0 && Vector3.Distance(player.transform.position, transform.position) >0.3f)
        {
            startTime -= Time.deltaTime;
            transform.position = Vector3.Lerp(player.transform.position,transform.position, startTime/5);
            transform.localScale = new Vector3 (startTime/5, startTime / 5, startTime / 5);
            yield return 1; 
        }
        GameSingleton.Instance.Inventory.AddMaterial(materialType,Size);
        Destroy(gameObject);
        //Add Sound And FX
    }
}
