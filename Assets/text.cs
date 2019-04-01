using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Net;
using UnityEngine.UI;
using UnityEngine.XR.WSA.WebCam;


public class a
{
    public int id;
    public string student;
}

public class b
{
    public int id;
    public string CourseName;
}

[DisallowMultipleComponent]
public class text : MonoBehaviour
{
    static a[] A=new a[]
    {
        new a{id=1,student ="a"}, 
        new a{id=2,student ="b"}, 
        new a{id=3,student ="c"},
    };

    private static b[] B = new b[]
    {
        new b{id=1,CourseName ="q"}, 
        new b{id=2,CourseName ="q"}, 
        new b{id=3,CourseName ="w"},        
        new b{id=1,CourseName ="w"}, 
        new b{id=2,CourseName ="e"}, 
        new b{id=3,CourseName ="e"},
    };
    void Start()
    {
        var query = from s in A join c in B on s.id equals c.id where c.CourseName == "w" select s;

        foreach (var item in query)
        {
            Debug.Log(item.student);
        }
    }

    public async void a()
    {
        WebClient wc=new WebClient();
        await wc.DownloadDataTaskAsync(new Uri("a"));
    }
    

}
