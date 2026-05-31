using UnityEngine;

public class BallDespawner : MonoBehaviour
{
    public GameObject[] triggers;
    public GameObject holePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(GameObject trigger in triggers)
        {
            Instantiate(holePrefab, trigger.transform.position, trigger.transform.rotation, trigger.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
