using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    //declaring a materials array and integers for the size of the grid
    public Material[] materials;
    public int gridX, gridY;
    public int materialIndex = 0;

    void Start()
    {

        materialIndex = Random.Range(0, materials.Length);
        GetComponent<MeshRenderer>().material = 
            materials[materialIndex];
    }
    private void Update() {
        // right click
        if(Input.GetMouseButtonDown(1)){
            changeColor();
        }

       // GetComponent<MeshRenderer>().material =  //accessing the mesh renderer of the GameObject this script is on
           // materials[Random.Range(0, materials.Length)]; //apply a random material from within the materials array

    }

    // creating a function that sets the position of a cube in the grid, and requires an x and y value to be passed through it
    public void SetPos(int x, int y)
    {

        gridX = x;
        gridY = y;
        name = "X: " + x + " Y: " + y;

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
            Debug.Log(materialIndex);
        } else { //if something is selected
            GridManager.instance.Swap(this); //swapping the selected instance with this particular instance
        }

        print(name);


        print(name); //prints the name of the position on the grid 

    }

     void changeColor(){
        // use ray casting to detect which block is chosen
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        if ( Physics.Raycast (ray,out hit)) {
            if(hit.transform.position==GetComponent<Transform>().position){
                IEnumerator coroutine;
                coroutine = ScaleMe(hit.transform, hit);
                StartCoroutine(coroutine);
            }
        }
    }
    IEnumerator ScaleMe(Transform objTr, RaycastHit hit) {
        objTr.localScale *= 1.2f;
        yield return new WaitForSeconds(0.3f);
        this.materialIndex++;
        if (this.materialIndex>=materials.Length){
            this.materialIndex = 0;
        }
        GetComponent<MeshRenderer>().material = 
            materials[this.materialIndex];
        objTr.localScale /= 1.2f;
    }

    public void newColor()
    {
        this.materialIndex++;
        if (this.materialIndex >= materials.Length)
        {
            this.materialIndex = 0;
        }
        GetComponent<MeshRenderer>().material =
            materials[this.materialIndex];
    }
}
