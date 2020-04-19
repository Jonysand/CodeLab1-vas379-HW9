using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionScript : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject cube;
    GameObject[,] solution;

    // Start is called before the first frame update
    void Start()
    {
        solution = new GameObject[width, height];
        GameObject SolutionHolder = new GameObject("Solution Holder");
        for (int x = 0; x < width; x++)
        { //checking the width
            for (int y = 0; y < height; y++) //checking the height
            {
                //place a cube into every x,y coordinate
                solution[x, y] = Instantiate<GameObject>(cube);
                //solution[x, y].transform.position = new Vector3(x, y, 0); //No need to place this grid onto the scene

                solution[x, y].transform.parent = SolutionHolder.transform; //make each cube a child of the gridHolder object
                solution[x, y].GetComponent<GridItem>().SetPos(x, y);
            }
        }
    }
}
