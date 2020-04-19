using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    public Material[] materials;
    public int gridX, gridY;
    public int materialIndex = 0;

    void Start()
    {
        // materialIndex = Random.Range(0, materials.Length);
        GetComponent<MeshRenderer>().material = 
            materials[materialIndex];
    }
    private void Update() {
        // right click
        if(Input.GetMouseButtonDown(1)){
            changeColor();
        }
    }

    // Start is called before the first frame update
    public void SetPos(int x, int y)
    {
        gridX = x;
        gridY = y;
        name = "X: " + x + " Y: " + y;
    }

    void OnMouseDown()
    {
        if(GridManager.instance.selected == null){
            GridManager.instance.selected = this;
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            GridManager.instance.Swap(this);
        }
        print(name);
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
}
