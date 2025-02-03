using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject rocketPrefab;
    [SerializeField] Transform barrel;
    [SerializeField, Range(0.5f, 5)] float spawnTime;

    float spawnTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnFire());
        //spawnTimer = Time.time + spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (Time.time >= spawnTimer)
        {
            spawnTimer = Time.time + spawnTime;
            Instantiate(rocketPrefab, barrel.position, barrel.rotation);    

        }
    }

    IEnumerator SpawnFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Instantiate(rocketPrefab, barrel.position, barrel.rotation);

        }
    }
}
