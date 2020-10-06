using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_script : MonoBehaviour
{

    Vector3 point_on_plane, normal_to_plane;

    public Vector3 normal { get { return normal_to_plane; }  }


    // Start is called before the first frame update
    void Start()
    {
        //define_plane(new Vector3(0, -2, 0), new Vector3(0.02f, 1, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void define_plane(Vector3 point, Vector3 normal)
    {
        point_on_plane = point;
        normal_to_plane = normal.normalized;
        transform.position = point_on_plane;

        transform.up = normal_to_plane;
    }

    public float distance_to(Vector3 point)
    {
        Vector3 point_on_plane_to_point = point - point_on_plane;

        return Vector3.Dot(point_on_plane_to_point, normal_to_plane);
    }
}
