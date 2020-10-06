using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectManagerScript : MonoBehaviour
{

    public Plane_script[] planes;
    public Sphere_script[] spheres;
    public Sphere_script s1;
    public Sphere_script s2;
    public float R;
    public float r;
    public float d;
    public float mass1;
    public float mass2;
    public Vector3 s1_point_of_impact;
    public Vector3 s2_point_of_impact;
    public Vector3 s1Normal;
    public Vector3 s2Normal;
    public Vector3 s1Para;
    public Vector3 s1Perp;
    public Vector3 s2Para;
    public Vector3 s2Perp;
    private float Coefficient_of_Restitution = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        planes = FindObjectsOfType(typeof(Plane_script)) as Plane_script[];
        spheres = FindObjectsOfType(typeof(Sphere_script)) as Sphere_script[];

        planes[0].define_plane(new Vector3(0, -2, 0), new Vector3(-0.02f, 1, 0.02f));
        planes[1].define_plane(new Vector3(5, 0, 0), new Vector3(-1, 0, 0));
        planes[2].define_plane(new Vector3(-5, 0, 0), new Vector3(1, 0, 0));
        planes[3].define_plane(new Vector3(0, 0, 5), new Vector3(0, 0, -1));
        planes[4].define_plane(new Vector3(0, 0, -5), new Vector3(0, 0, 1));

        s1 = spheres[0];
        s2 = spheres[1];

        R = s1.Radius_of_sphere;
        r = s2.Radius_of_sphere;

        mass1 = s1.mass;
        mass2 = s2.mass;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Sphere_script sphere in spheres)
        {
            foreach (Plane_script plane in planes)
            {
                sphere.setNearestPlane(plane);
            }

            sphere.setNearestSphere();
        }

        d = Vector3.Distance(s1.transform.position, s2.transform.position);

        if(d <= R + r)
        {
            Debug.Log("Spheres Colliding!");

            //Get Normals
            s1Normal = (s2.transform.position - s1.transform.position).normalized;
            s2Normal = -s1Normal;

            //Get Para and Perp of S1 velocity at collision
            s1Para = s1.parallel_component(s1.velocity, s1Normal);
            s1Perp = s1.perpendicular_component(s1.velocity, s1Normal);

            //Get Para and Perp of S2 velocity at collision
            s2Para = s2.parallel_component(s2.velocity, s2Normal);
            s2Perp = s2.perpendicular_component(s2.velocity, s2Normal);

            //Get point of impact for both spheres
            s1_point_of_impact = s1.transform.position + (R * s1Normal);
            s2_point_of_impact = s2.transform.position - (r * s2Normal);

            //Move spheres back (problem)
            s1.transform.position -= (s1.velocity * Time.deltaTime);
            s2.transform.position -= (s2.velocity * Time.deltaTime);

            //Calculate new Para for S1 velocity after collision and aplly to the velocity
            Vector3 newV1Para = newV1Parallel(mass1, mass2, s1Para, s2Para);
            s1.velocity = s1Perp - (Coefficient_of_Restitution * newV1Para);

            //Calculate new Para for S2 velocity after collision and aplly to the velocity
            Vector3 newV2Para = newV2Parallel(mass1, mass2, s1Para, s2Para);
            s2.velocity = s2Perp - (Coefficient_of_Restitution * newV2Para);

            //Move the spheres with new velocity (Point of impact needed?)
            s1.transform.position -= (s1.velocity * Time.deltaTime);
            s2.transform.position -= (s2.velocity * Time.deltaTime);

        }

    }

    public Sphere_script[] GetSpheres()
    {
        return spheres;
    }

    public Vector3 newV1Parallel(float m1, float m2, Vector3 oldV1Para, Vector3 oldV2Para)
    {
        //Formula - (((m1-m2)/(m1+m2)*oldV1Para) + ((2*m2)/(m1+m2))*oldV2Para)

        float m1Minusm2 = m1 - m2;
        float m1Plusm2 = m1 + m2;
        float m2Times2 = m2 * 2;

        float firstDivision = m1Minusm2 / m1Plusm2;
        float secondDivision = m2Times2 / m1Plusm2;

        return (oldV1Para * firstDivision) + (oldV2Para * secondDivision);
        
    }

    public Vector3 newV2Parallel(float m1, float m2, Vector3 oldV1Para, Vector3 oldV2Para)
    {
        //Formula - (((m2-m1)/(m1+m2)*oldV2Para) + ((2*m1)/(m1+m2))*oldV1Para)

        float m2Minusm1 = m2 - m1;
        float m1Plusm2 = m1 + m2;
        float m1Times2 = m1 * 2;

        float firstDivision = m2Minusm1 / m1Plusm2;
        float secondDivision = m1Times2 / m1Plusm2;

        return (oldV2Para * firstDivision) + (oldV1Para * secondDivision);
    }
}
