using LightCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public ButtonEx btn;
    Student stu;
    public GameObject cube0;
    public GameObject cube1;
    private GameObject root;
	// Use this for initialization
	void Start () {
        stu = new Student();
        stu.age = 5;
        btn.DataContex = stu;
        cube1.transform.SetParent(cube0.transform,false);
        btn.PointerClick = (o, e) => {
            Debug.Log("Click>>>>>>>>>>");
        };
        btn.PointerEnter = (o, e) => {
            Debug.Log("Enter>>>>>>>>>>>");
        };
        btn.PointerExit = (o, e) => {
            Debug.Log("Exit>>>>>>>>>>>");
        };
        root = GameObject.Find("Canvas");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
public class Student
{
    public int age;
}
