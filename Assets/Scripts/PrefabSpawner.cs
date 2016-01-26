using UnityEngine;
using System.Collections;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject objPrefab;
    public Vector2 dimensions;

	// Use this for initialization
	void Start ()
    {
	    for(int i = 0; i < dimensions.x; ++i)
        {
            for(int u = 0; u < dimensions.y; ++u)
            {
                GameObject nC = GameObject.Instantiate(objPrefab);
                nC.transform.position = new Vector3(i, transform.position.y, u);
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
