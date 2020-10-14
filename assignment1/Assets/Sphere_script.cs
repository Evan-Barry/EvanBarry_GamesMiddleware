using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere_script : MonoBehaviour
{
    public Vector3 velocity  = new Vector3(0,0,0);
    Vector3 accelleration = new Vector3(0, -9.8f, 0);
    public float Radius_of_sphere = 0.5f;
    private float Coefficient_of_Restitution = 0.6f;
    public Plane_script plane;

    public objectManagerScript objectManager;
    public Sphere_script[] spheres;
    public Sphere_script nearestSphere;

    public Vector3 sphereNormal;

    public float mass = 1;
    public Vector3 para;
    public Vector3 perp;

    // Start is called before the first frame update
    void Start()
    {
        plane = FindObjectOfType<Plane_script>();
        objectManager = FindObjectOfType<objectManagerScript>();
        spheres = objectManager.GetSpheres();
        foreach (Sphere_script sphere in spheres)
        {
            if(Vector3.Distance(sphere.transform.position, transform.position) > 1)
            {
                nearestSphere = sphere;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float d1 = plane.distance_to(transform.position) - Radius_of_sphere;
        float distance_to_nearest_sphere = Vector3.Distance(transform.position, nearestSphere.transform.position);

        velocity += accelleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        
        float distance_from_centre_to_plane = plane.distance_to(transform.position);
        float d2 = distance_from_centre_to_plane - Radius_of_sphere;

        if (d2 <= 0)
        {
            //Debug.Log("D2 <= 0");
            para = parallel_component(velocity, plane.normal);
            perp = perpendicular_component(velocity, plane.normal);

            float time_of_impact = Time.deltaTime * (d1 / (d1 - d2)); 
            transform.position -= velocity * (Time.deltaTime - time_of_impact); //error here at random times

            velocity = perp - (Coefficient_of_Restitution * para);

            transform.position += velocity * (Time.deltaTime - time_of_impact);//error here at random times

            // float overlap = Radius_of_sphere - distance_from_centre_to_plane;
            // transform.position += overlap * plane.normal;
        }

        // float R = Radius_of_sphere;
        // float r = nearestSphere.Radius_of_sphere;
        // float d = Vector3.Distance(transform.position, nearestSphere.transform.position);
        // sphereNormal = (nearestSphere.transform.position - transform.position).normalized;

        // if(d <= R + r)
        // {
        //     Debug.Log("Spheres Colliding");
        //     Vector3 pointOfImpact = transform.position + (R * sphereNormal);

        //     Vector3 newPara = newV1Parallel(mass, nearestSphere.mass, para, nearestSphere.para);

        //     velocity = perpendicular_component(velocity, sphereNormal) + (Coefficient_of_Restitution * newPara);

        //     transform.position += velocity * Time.deltaTime;

        // }
    }
    public Vector3 perpendicular_component(Vector3 vel, Vector3 norm)
    {
        return vel - parallel_component(vel, norm);
    }
    public Vector3 parallel_component(Vector3 vel, Vector3 norm)
    {
        return Vector3.Dot(vel, norm) * norm;
    }

    public void setNearestPlane(Plane_script p)
    {
        if(p.distance_to(transform.position) < plane.distance_to(transform.position))
        {
            plane = p;
        }
    }

    public void setNearestSphere()
    {
        foreach (Sphere_script sphere in spheres)
        {
            if(Vector3.Distance(sphere.transform.position, transform.position) > 0)
            {
                if(Vector3.Distance(sphere.transform.position, transform.position) < Vector3.Distance(nearestSphere.transform.position, transform.position))
                {
                    nearestSphere = sphere;
                }
                
            }
        }
    }

}
