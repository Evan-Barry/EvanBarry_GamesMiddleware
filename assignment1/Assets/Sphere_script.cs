using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere_script : MonoBehaviour
{
    public Vector3 velocity  = new Vector3(0,0,0);
    public Vector3 accelleration = new Vector3(0, -9.8f, 0);
    public float Radius_of_sphere = 0.5f;
    private float Coefficient_of_Restitution = 0.6f;
    public Plane_script plane;

    public objectManagerScript objectManager;
    public Sphere_script[] spheres;
    public Sphere_script nearestSphere;

    public Vector3 sphereNormal;

    public float mass;
    public Vector3 para;
    public Vector3 perp;
    public float distance_to_nearest_sphere;
    public float time_of_impact;

    public float distance_to_nearest_sphere1;
    public float distance_to_nearest_sphere2;
    public float distance_from_centre_to_plane;
    public float d1, d2;
    public bool active = true;

    public int hp;

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
        //setNearestSphere();
        distance_to_nearest_sphere1 = Vector3.Distance(transform.position, nearestSphere.transform.position) - Radius_of_sphere;

        velocity += accelleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        
        float distance_from_centre_to_plane = plane.distance_to(transform.position);
        float d2 = distance_from_centre_to_plane - Radius_of_sphere;
        distance_to_nearest_sphere2 = Vector3.Distance(transform.position, nearestSphere.transform.position) - Radius_of_sphere;

        if (d2 <= 0)
        {
            //Debug.Log("D2 <= 0");
            para = parallel_component(velocity, plane.normal);
            perp = perpendicular_component(velocity, plane.normal);

            if(d1-d2 == 0)//checking if the distance between the centre of the sphere to the plane from 1 frame and the next is 0
            {
                Debug.Log("d1-d2 is 0");
                time_of_impact = Time.deltaTime * (d1 / 0.01f);//if it is 0, the a division by 0 occurs, so the value is set to 0.01 to prevent this 
            }
            else//if its not 0, then the actual distance is used as the divisor
            {
                time_of_impact = Time.deltaTime * (d1 / (d1 - d2)); 
            }
            //if 0 is used as divisor, then the position of the sphere is set to infinity and causes errors

            transform.position -= velocity * (Time.deltaTime - time_of_impact);

            velocity = perp - (Coefficient_of_Restitution * para);

            transform.position += velocity * (Time.deltaTime - time_of_impact);
        }
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

    public Sphere_script getNearestSphere()
    {
        return nearestSphere;
    }

    public Vector3 getSphereNormal()
    {
        return sphereNormal;
    }

    public void setNormal(Vector3 n)
    {
        sphereNormal = n;
    }

    public float getDistance_to_nearest_sphere()
    {
        return distance_to_nearest_sphere;
    }

    public void setMass(float m)
    {
        mass = m;
    }

    public void setHP(int hp)
    {
        this.hp = hp;
    }

    public int getHP()
    {
        return hp;
    }

    public void scaleDownSphere()
    {
        Debug.Log("Shrinking");
        float newScale = Mathf.Lerp(1, 0, Time.deltaTime * 100f);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
    public bool getActive()
    {
        return active;
    }

    public void setActive(bool a)
    {
        active = a;
    }

}
