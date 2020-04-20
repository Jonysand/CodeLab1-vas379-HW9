using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //declaring variables for height and width
    public int width1; 
    public int height1;
    //solution width and height
    public int width2;
    public int height2;

    //declaring GameObject
    public GameObject cube;
   

    //creating 2d array
    public GameObject[,] grid;
    public GameObject[,] solution;

    //creating a unique instance of this script
    public static GridManager instance;

    //creating a new object of type GridItem
    public GridItem selected;

    public Material grey;

    void Awake() //function called when the game starts
    {
        if(instance == null){ //if there is no GridManager filling the "instance" space
            instance = this; //make the "instance" GridManager into this particular instance
            DontDestroyOnLoad(gameObject); //keep this instance in place when changing scenes
        } else { //if there is currently a GridManager in the "instance" space
            Destroy(gameObject); //destroy that GridManager instance
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        solution = new GameObject[width2, height2]; //create solution array
        GameObject SolutionHolder = new GameObject("Solution Holder");
        for (int x = 0; x < width2; x++)
        { //checking the width
            for (int y = 0; y < height2; y++) //checking the height
            {
                //place a cube into every x,y coordinate
                solution[x, y] = Instantiate<GameObject>(cube);
                //solution[x, y].transform.position = new Vector3(x, y, 0); //No need to place this grid onto the scene

                solution[x, y].transform.parent = SolutionHolder.transform; //make each cube a child of the gridHolder object
                solution[x, y].GetComponent<GridItem>().SetPos(x, y);
            }
        }
            grid = new GameObject[width1, height1]; //creating a new grid array with a width of "width" and a height of "height"

        GameObject gridHolder = new GameObject("Grid Holder"); //create an empty GameObject to hold the 2d array

        //for loop to fill in the grid
        for (int x = 0; x < width1; x++){ //checking the width
            for (int y = 0; y < height1; y++) //checking the height
            {
             
                //place a cube into every x,y coordinate
                grid[x, y] = Instantiate<GameObject>(cube); 
                grid[x, y].transform.position = 
                    new Vector3(x, y, 0); //set the position of a cube to the x,y coordinates of this particular position in the grid

                grid[x, y].transform.parent = gridHolder.transform; //make each cube a child of the gridHolder object

                int originalGridMaterialIndex = grid[x, y].GetComponent<GridItem>().materialIndex;
                if (solution[x, y].GetComponent<GridItem>().materialIndex == originalGridMaterialIndex)
                    {
                        grid[x, y].GetComponent<GridItem>().materialIndex = Random.Range(0, 4);
                    }


                    grid[x, y].GetComponent<GridItem>().SetPos(x, y);  //get the GridItem component of a given cube and execute the SetPos function on it, passing its x,y coordinates through
            }
        }

     
        

        Camera.main.transform.position = 
            new Vector3(width1 / 2, height1 / 2, -10); //change the position of the camera to be able to see the grid
    }

    private void Update()
    {
        for (int x = 0; x < width2; x++)
        { //checking the width
            for (int y = 0; y < height2; y++) //checking the height
            {
                // getting the grid item's materialIndex
                int originalGridMaterialIndex = grid[x, y].GetComponent<GridItem>().materialIndex;
                if (solution[x, y].GetComponent<GridItem>().materialIndex == originalGridMaterialIndex)
                {
                    print("change mat");
                    // change the material of that item
                    //grid.grid[x,y].GetComponent<MeshRenderer>().material = grid.grid[x,y].GetComponent<GridItem>().materials[originalGridMaterialIndex];
                    grid[x, y].GetComponent<MeshRenderer>().material = grey;
                }
            }
        }

    }

    //creating a Swap function which passes through a new GridItem
    public void Swap(GridItem newItem)
    {
        //declaring integers whose values correspond to the x and y coordinates of the newItem
        int tempX = newItem.gridX; 
        int tempY = newItem.gridY;

        //get the position of a cube (called newItem) in the grid using the SetPos function- passing through the x and y coordinates of the selected cube instance
        newItem.SetPos(selected.gridX, selected.gridY);
        newItem.transform.position =
                    new Vector2(selected.gridX, selected.gridY); //change the position of the first cube (newItem) into the posisition of the second cube (selected)
        grid[tempX, tempY] = newItem.gameObject; //setting the location of the cube newItem within the array

        selected.SetPos(tempX, tempY); //get the coordinates of the second cube (selected)
        selected.transform.position =
                    new Vector2(tempX, tempY); //move the second cube (selected) into the position of the first cube (newItem)
        grid[tempX, tempY] = selected.gameObject; // setting the location of the second cube (selected) within the array

        selected.transform.localScale = new Vector3(.75f, .75f, .75f); //decrease the scale of a cube that is selected
        selected = null; //deselect a cube after the positions have been swapped
    }
}
