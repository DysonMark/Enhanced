using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject[] boxPrefabs;

    public int waitingTime;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello whatsup");
        StartCoroutine(SpawnBox());
    }

    IEnumerator SpawnBox()
    {
        while (true)
        {
            int index = Random.Range(0, boxPrefabs.Length);
            Instantiate(boxPrefabs[index], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waitingTime);   
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
