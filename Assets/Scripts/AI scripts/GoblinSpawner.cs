using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawners;
    [SerializeField] private GameObject goblin;

    private void SpawnGoblin()
    {
        int randomInt = Random.Range(1, spawners.Length);
        Instantiate(goblin, spawners[randomInt].position, spawners[randomInt].rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SpawnGoblin();
        }
    }
}
