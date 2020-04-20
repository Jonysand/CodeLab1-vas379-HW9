using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //declaring variables for height and width
    public int width; 
    public int height;

    //declaring GameObject
    public GameObject cube;

    //creating 2d array
    public GameObject[,] grid;

    //creating a unique instance of this script
    public static GridManager instance;

    //creating a new object of type GridItem
    public GridItem selected;

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

        grid = new GameObject[width, height]; //creating a new grid array with a width of "width" and a height of "height"

        GameObject gridHolder = new GameObject("Grid Holder"); //create an empty GameObject to hold the 2d array

        //for loop to fill in the grid
        for (int x = 0; x < width; x++){ //checking the width
            for (int y = 0; y < height; y++) //checking the height
            {
             
                //place a cube into every x,y coordinate
                grid[x, y] = Instantiate<GameObject>(cube); 
                grid[x, y].transform.position = 
                    new Vector3(x, y, 0); //set the position of a cube to the x,y coordinates of this particular position in the grid

                grid[x, y].transform.parent = gridHolder.transform; //make each cube a child of the gridHolder object
             

                grid[x, y].GetComponent<GridItem>().SetPos(x, y);  //get the GridItem component of a given cube and execute the SetPos function on it, passing its x,y coordinates through
            }
        }

        Camera.main.transform.position = 
            new Vector3(width / 2, height / 2, -10); //change the position of the camera to be able to see the grid
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
