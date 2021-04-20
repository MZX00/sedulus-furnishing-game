using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{

    public GameObject shape;
    private ArrayList shapes;
    private int fid;

    private int price;


    void Awake(){
        shapes = new ArrayList();
    }

    public void initiateFurniture(){
        for(int i = 0; i<4; i++){
            Vector2 furnsize = new Vector2(300,300);
            float sizex = Random.value*(200 - 50)+50;
            float sizey = Random.value*(200 - 50)+50;
            float limitx = ((furnsize.x - sizex) / 2) - 10;
            float limity = ((furnsize.y - sizey) / 2) - 10;
            float posx = Random.value*(limitx * 2)-limitx;
            float posy = Random.value*(limity * 2)-limity;

            addShape(new Vector3(posx,posy,0),new Vector2(sizex,sizey));
        }
    }


    public void addShape(Vector3 pos, Vector2 size){
        GameObject temp = Instantiate(shape, pos, Quaternion.identity);
        temp.GetComponent<RectTransform>().sizeDelta = size;
        temp.transform.SetParent(transform,false);
        shapes.Add(temp);
    }

    public void removeShape(int index){
        GameObject temp = (GameObject)shapes[index];
        shapes.RemoveAt(index);
        Destroy(temp);
    }

    public void setFID(int fid){
        this.fid = fid;
    }

    public int getFID(){
        return this.fid;
    }

    public void setPrice(int price){
        this.price = price;
    }

    public int getPrice(){
        return this.price;
    }

}
