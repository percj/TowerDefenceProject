using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWithPick : MonoBehaviour
{
    [SerializeField] PlayerLogic playerLogic;
    [SerializeField] AudioSource Audio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mine" || other.tag == "Tree")
        {
            MaterialCreator materialCreator;
            if (other.gameObject.transform.TryGetComponent(out materialCreator))
            {
                 playerLogic.currMaterialCreator = materialCreator;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Mine" || other.tag == "Tree")
        {
            playerLogic.currMaterialCreator = null;
        }
    }

}
