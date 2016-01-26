using UnityEngine;
using System.Collections;

public class SelectorTest : MonoBehaviour
{
    public GameObject selectorCube;
    public GameObject roomBlock;

    private static Hashtable cubeGrid;

    Camera camera = null;
    
    public static void AddToTable(GameObject addMe)
    {
        cubeGrid.Add(new Vector2(addMe.transform.position.x, addMe.transform.position.z), addMe);
    }

    public static GameObject GetObjectAt(int x, int y)
    {
        return (GameObject)cubeGrid[new Vector2(x, y)];
    }

    public static GameObject GetObjectAt(Vector3 pos)
    {
        return (GameObject)cubeGrid[new Vector2(pos.x, pos.z)];
    }

    public static GameObject[] GetObjectAround(Vector3 pos)
    {
        int[] nPos = { Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z) };

        GameObject[] returnMe = new GameObject[4];

        returnMe[0] = GetObjectAt(nPos[0], nPos[1] - 1);
        returnMe[1] = GetObjectAt(nPos[0] + 1, nPos[1]);
        returnMe[2] = GetObjectAt(nPos[0], nPos[1] + 1);
        returnMe[3] = GetObjectAt(nPos[0] - 1, nPos[1]);

        return returnMe;
    }

    void Start ()
    {
        cubeGrid = new Hashtable();
        camera = GetComponent<Camera>();   
	}
	
	void Update ()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            selectorCube.transform.position = new Vector3(Mathf.Round(hit.point.x), hit.point.y - 0.49f, Mathf.Round(hit.point.z));
        }

        if(Input.GetMouseButtonDown(0))
        {
            GameObject curBlock = GetObjectAt(selectorCube.transform.position);

            if(curBlock == null)
            {
                GameObject newBlock = GameObject.Instantiate(roomBlock);
                newBlock.transform.position = selectorCube.transform.position;
                newBlock.GetComponent<Voxel>().Init();
            }
        }
    }
}