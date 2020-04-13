using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    //declaring a materials array and integers for the size of the grid
    public Material[] materials;
    public int gridX, gridY;

    void Start()
    {
        GetComponent<MeshRenderer>().material =  //accessing the mesh renderer of the GameObject this script is on
            materials[Random.Range(0, materials.Length)]; //apply a random material from within the materials array
    }

    // creating a function that sets the position of a cube in the grid, and requires an x and y value to be passed through it
    public void SetPos(int x, int y)
    {
        gridX = x; //setting value of gridX to x
        gridY = y; //setting value of gridY to y

        name = "X: " + x + " Y: " + y; //naming the grid so that it says "X: (x-value) Y: (y-value)"
    }

    //function that happens when the mouse is clicked
    void OnMouseDown()
    {
        if(GridManager.instance.selected == null){ //if nothing is selected
            GridManager.instance.selected = this; //select this particular instance
            transform.localScale = new Vector3(1, 1, 1); // changing the scale of the selected instance
        } else { //if something is selected
            GridManager.instance.Swap(this); //swapping the selected instance with this particular instance
        }

        print(name); //prints the name of the position on the grid 
    }
}
