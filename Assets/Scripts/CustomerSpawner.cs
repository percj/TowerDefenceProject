using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{

    public List<StationController> stations;
    float spawnTimer;
    [Range(0,5)][SerializeField] float spawnElapsed;
    [SerializeField] List<GameObject> customers;
    [SerializeField] Transform startPos;
    [SerializeField] Transform exitPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCustomer();
    }

    private void SpawnCustomer()
    {
        if (spawnTimer >= spawnElapsed)
        {
            var selectedStation = stations.Where(x => x.currCustomer == null && x.unlockedManager.isUnlocked == true).OrderBy(x => Guid.NewGuid()).Take(1).ToList();
            if(selectedStation.Count > 0)
            {
                spawnTimer = 0;
                var spawnedCustomer = customers[UnityEngine.Random.Range(0, customers.Count)];
                var x = Instantiate(spawnedCustomer, transform);
                var customerController = x.GetComponent<CustomerController>();

                x.transform.parent = transform;
                x.transform.position = startPos.position;
                customerController.selectedStation = selectedStation[0];
                customerController.exitPos = exitPos;
                selectedStation[0].currCustomer = customerController;

            }
        }
        else
            spawnTimer += Time.deltaTime;
    }
}
