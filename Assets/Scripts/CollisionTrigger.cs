using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The material used when object is not touching targeted objects [:either in shapeNames list or start with \"Collide\"]")]
    Material original;
    [Tooltip("The material used when object is touching with targeted objects [:either in shapeNames list or start with \"Collide\"]")]
    [SerializeField]
    Material touching;
    [SerializeField]
    [Tooltip("When the script is used with 'non-kinematic' RigidBody and Convex components, this material is used to render the components that are colliding with a targeted object")]
    Material red;

    //[SerializeField]
    //[Tooltip("You can add a Renderer manually, otherwise the script changes the material for all Renderers in this GameObject and its children.")]
    //Renderer mainRenderer;

    [SerializeField]
    [Tooltip("Renders the colliding components with 'red' material when the script is used with 'non-kinematic' RigidBody and Convex components.")]
    bool renderCollidingComponents = false;

    List<string> shapeNames = new List<string> { "Star", "Oval", "Triangle", "Square", "Rectangle", "Trapezoid", "Hexagon", "Octagon", "Parallelogram", "Pentagon", "Quatrefoil", "Rhombus" };

    private int n_colliding = 0;

    private void Start()
    {
        Debug.Log("Collision trigger start");
        setMaterialRecursive(original, gameObject);
    }

    // TODO dirty buggy code. need to access all children recursively.
    private void setColor(Color c)
    {
        if (gameObject.GetComponent<Renderer>() != null)
        {
            gameObject.GetComponent<Renderer>().material.color = c;
        }
        else
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).GetComponent<Renderer>() != null)
                {
                    gameObject.transform.GetChild(i).GetComponent<Renderer>().material.color = c;
                }
            }
        }
    }

    // if there is a renderer specifically specified uses that,
    // else, if this object has a renderer, sets its material. If not recursively sets materials in children
    private void setMaterialRecursive(Material m, GameObject o)
    {
        //if (mainRenderer != null)
        //{
        //    mainRenderer.material = m;
        //    return;
        //}
        if (o.GetComponent<Renderer>() != null)
        {
            o.GetComponent<Renderer>().material = m;
        }
        for (int i = 0; i < o.transform.childCount; i++)
        {
            setMaterialRecursive(m, o.transform.GetChild(i).gameObject);
        }
    }

    private void renderColliders(Collision c)
    {
        foreach (ContactPoint contact in c.contacts)
        {
            Debug.Log(contact.thisCollider.name + " hit " + contact.otherCollider.name);
            // Visualize the contact point
            //Debug.DrawRay(contact.point, contact.normal, Color.white);
            if (isTargetedObject(contact.otherCollider.name)){
                contact.thisCollider.gameObject.GetComponent<Renderer>().enabled = true;
                contact.thisCollider.gameObject.GetComponent<Renderer>().material = red;
            }
        }
    }

    private bool isTargetedObject(GameObject o)
    {
        return (isTargetedObject(o.name));
    }

    private bool isTargetedObject(string gameObjectName)
    {
        Debug.Log("is interesting? " + gameObjectName + " " + gameObjectName.StartsWith("Collide"));
        if (shapeNames.Contains(gameObjectName) || gameObjectName.StartsWith("Collide"))
            return true;
        return false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name+":"+" collision with" + collision.gameObject.name);
        if (isTargetedObject(collision.gameObject.name)) {
            n_colliding += 1;
            setMaterialRecursive(touching, gameObject);
            Debug.Log("changed color to colliding in collision enter, colliding with: "+n_colliding);
            //renderColliders(collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (renderCollidingComponents)
        {
            renderColliders(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("collision exit");
        if (isTargetedObject(collision.gameObject))
        {
            n_colliding -= 1;
            Debug.Log("in collision exit, colliding with: "+ n_colliding);
            if (n_colliding > 0)
            {
                setMaterialRecursive(touching, gameObject);
            }
            else
            {
                setMaterialRecursive(original, gameObject);
                Debug.Log("changed color to normal in collision exit");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger with" + other.gameObject.name);
        setMaterialRecursive(touching, gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger end");
        setMaterialRecursive(original, gameObject);
    }
}
