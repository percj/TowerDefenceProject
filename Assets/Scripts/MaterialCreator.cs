using FIMSpace.Jiggling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MaterialType
{
    Gold,
    Bronze,
    Wood
}

public class MaterialCreator : MonoBehaviour 
{
    [SerializeField] GameObject collectableMaterial;
    [SerializeField] Transform Pos;
    FJiggling_Simple jiggling;
    [SerializeField] int maxCount;
    [SerializeField] int minCount;
    [SerializeField] int maxSize;
    [SerializeField] int minSize;
    [SerializeField] float Speed;
    [SerializeField] int hitCounter;
    [SerializeField] GameObject beforeLevel;
    [SerializeField] MaterialType materialType;
    
    int currHit;

    private void Start()
    {
        TryGetComponent(out jiggling);
    }
    private void OnEnable()
    {
        currHit = 0;
    }
    private void OnDisable()
    {
        currHit = 0;
    }
    public void createMaterial()
    {
        StartCoroutine(GiveMaterial());
    }
    IEnumerator GiveMaterial()
    {
        currHit++;
        jiggling?.StartJiggle();
        if (materialType == MaterialType.Wood) GameSingleton.Instance.Sounds.PlayOneShot(GameSingleton.Instance.Sounds.PickAxe);
        if (materialType == MaterialType.Gold || materialType == MaterialType.Gold) GameSingleton.Instance.Sounds.PlayOneShot(GameSingleton.Instance.Sounds.Axe);
        var count = Random.Range(minCount, maxCount);
        for(int i =0;i < count; i++)
        {
            var size = Random.Range(minSize, maxSize);
            var x = Instantiate(collectableMaterial, Pos);
            x.transform.position = Pos.position;
            var right = Vector3.left * Random.Range(5, 2) + Vector3.right * Random.Range(5, 2);
            var forward = Vector3.forward * Random.Range(5, 2)+ Vector3.back * Random.Range(5, 2);
            x.GetComponent<Rigidbody>().AddForce((Vector3.up + right + forward)*Speed,ForceMode.Impulse);
            x.GetComponent<CollectableMaterial>().Size = size;
            yield return new WaitForSeconds(0.1f);
        }
        if (hitCounter <= currHit)
        {
            beforeLevel.SetActive(true);
            gameObject.SetActive(false);
        }
        yield return null;
    }

}
