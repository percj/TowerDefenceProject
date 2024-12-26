using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] List<Transform> teleports;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerLogic>().Teleport(teleports[Random.Range(0, teleports.Count)].position);
        }
    }

}
