using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectManagerScript : MonoBehaviour
{

    public Plane_script[] planes = new Plane_script[5];
    public Sphere_script[] spheres = new Sphere_script[2];

    public Sphere_script s1;
    public Sphere_script s2;
    public float R;
    public float r;
    public float d;
    public Vector3 Point_of_impact;
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
        planes[0].define_plane(new Vector3(0, -2, 0), new Vector3(-0.02f, 1, 0.02f));
        planes[1].define_plane(new Vector3(5, 0, 0), new Vector3(-1, 0, 0));
        planes[2].define_plane(new Vector3(-5, 0, 0), new Vector3(1, 0, 0));
        planes[3].define_plane(new Vector3(0, 0, 5), new Vector3(0, 0, -1));
        planes[4].define_plane(new Vector3(0, 0, -5), new Vector3(0, 0, 1));

        s1 = spheres[0];
        s2 = spheres[1];

        R = s1.Radius_of_sphere;
        r = s2.Radius_of_sphere;
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

            s1Normal = (s2.transform.position - s1.transform.position).normalized;
            s2Normal = -s1Normal;

            Point_of_impact = s1.transform.position + (R * s1Normal);

            s1Para = s1.parallel_component(s1.velocity, s1Normal);
            s1Perp = s1.perpendicular_component(s1.velocity, s1Normal);

            s2Para = s2.parallel_component(s2.velocity, s1Normal);
            s2Perp = s2.perpendicular_component(s1.velocity, s1Normal);

            s1.transform.position -= s1.velocity * Time.deltaTime;
            s2.transform.position -= s1.velocity * Time.deltaTime;

            Vector3 newV1Para = newV1Parallel(s1Normal, s2Normal, s1Para, s2Para);
            s1.velocity = s1Perp - (Coefficient_of_Restitution * newV1Para);

            Vector3 newV2Para = newV2Parallel(s1Normal, s2Normal, s1Para, s2Para);
            s2.velocity = s2Perp - (Coefficient_of_Restitution * newV2Para);

            s1.transform.position += s1.velocity * Time.deltaTime;
            s2.transform.position += s2.velocity * Time.deltaTime;

        }

    }

    public Sphere_script[] GetSpheres()
    {
        return spheres;
    }

    public Vector3 newV1Parallel(Vector3 m1, Vector3 m2, Vector3 oldV1Para, Vector3 oldV2Para)
    {
        //Formula - (((m1-m2)/(m1+m2)*oldV1Para) + ((2*m2)/(m1+m2))*oldV2Para)

        Vector3 m1Minusm2 = m1 - m2;
        Vector3 m1Plusm2 = m1 + m2;
        Vector3 m2Times2 = m2 * 2;

        Vector3 firstDivision = new Vector3(m1Minusm2.x/m1Plusm2.x, m1Minusm2.y/m1Plusm2.y, m1Minusm2.z/m1Plusm2.z);
        Vector3 secondDivision = new Vector3(m2Times2.x/m1Plusm2.x, m2Times2.y/m1Plusm2.y, m2Times2.z/m1Plusm2.z);

        return Vector3.Scale(firstDivision, oldV1Para) + Vector3.Scale(secondDivision, oldV2Para);
        
    }

    public Vector3 newV2Parallel(Vector3 m1, Vector3 m2, Vector3 oldV1Para, Vector3 oldV2Para)
    {
        //Formula - (((m2-m1)/(m1+m2)*oldV2Para) + ((2*m1)/(m1+m2))*oldV1Para)

        Vector3 m2Minusm1 = m2 - m1;
        Vector3 m1Plusm2 = m1 + m2;
        Vector3 m1Times2 = m1 * 2;

        Vector3 firstDivision = new Vector3(m2Minusm1.x/m1Plusm2.x, m2Minusm1.y/m1Plusm2.y, m2Minusm1.z/m1Plusm2.z);
        Vector3 secondDivision = new Vector3(m1Times2.x/m1Plusm2.x, m1Times2.y/m1Plusm2.y, m1Times2.z/m1Plusm2.z);

        return Vector3.Scale(firstDivision, oldV2Para) + Vector3.Scale(secondDivision, oldV1Para);
    }
}
