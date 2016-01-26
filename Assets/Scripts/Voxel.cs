using UnityEngine;
using System.Collections;

public class Voxel : MonoBehaviour
{
    GameObject[] around;

    public GameObject noWallPrefab;
    public GameObject allWallPrefab;
    public GameObject oneWallPrefab;
    public GameObject cornerWallPrefab;
    public GameObject passagePrefab;
    public GameObject threeWallPrefab;


    void Start ()
    {
        around = new GameObject[4];
	}
	
	void Update ()
    {
	
	}

    public void UpdateWalls()
    {
        GameObject[] curBlocks = SelectorTest.GetObjectAround(transform.position);
        float[] notNull = new float[4];
        float[] yesNull = new float[4];

        int wallCount = 4;
        int nullCount = 0;

        for (int i = 0; i < 4; ++i)
            if (curBlocks[i] != null)
            {
                notNull[4-wallCount] = i;
                wallCount--;
            }
            else
            {
                yesNull[nullCount] = i;
                nullCount++;
            }

        switch (wallCount)
        {
            case 0:
                GetComponent<MeshFilter>().mesh = noWallPrefab.GetComponent<MeshFilter>().sharedMesh;
                break;
            case 1:
                GetComponent<MeshFilter>().mesh = oneWallPrefab.GetComponent<MeshFilter>().sharedMesh;
                transform.eulerAngles = new Vector3(0, yesNull[0]*(90f) , 0);
                break;
            case 2:
                GetComponent<MeshFilter>().mesh = cornerWallPrefab.GetComponent<MeshFilter>().sharedMesh;
                break;
            case 3:
                GetComponent<MeshFilter>().mesh = threeWallPrefab.GetComponent<MeshFilter>().sharedMesh;
                transform.eulerAngles = new Vector3(0, notNull[0] * (-90f), 0);
                break;
            case 4:
                GetComponent<MeshFilter>().mesh = allWallPrefab.GetComponent<MeshFilter>().sharedMesh;
                break;
            default:
                break;
        }
    }

    public void Init()
    {
        SelectorTest.AddToTable(gameObject);

        UpdateWalls();

        GameObject[] curBlocks = SelectorTest.GetObjectAround(transform.position);
        for (int i = 0; i < 4; ++i)
            if (curBlocks[i] != null)
                curBlocks[i].GetComponent<Voxel>().UpdateWalls();
    }
}
