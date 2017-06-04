using System.Collections;
using UnityEngine;

public class HumanInfoScript : MonoBehaviour
{
    public float brakeValeu;
    public string floorName;

    float jointValue;
    float weightValue;

    int humamValue;

    [HideInInspector]
    public bool firstBomb = true;

    //public void Init(HumanData data)
    //{
    //    jointValue = data.JointValue;
    //    weightValue = data.WeightValue;
    //}

    void Start ()
	{
        humamValue = int.Parse(gameObject.name.Substring(0, 1));

        if (humamValue == 1)
        {
            jointValue = 1;
            weightValue = 2;
        }

        if (humamValue == 2)
        {
            jointValue = 0.8f;
            weightValue = 1.9f;
        }

        if (humamValue == 3)
        {
            jointValue = 0.6f;
            weightValue = 1.8f;
        }

        if (humamValue == 4)
        {
            jointValue = 0.4f;
            weightValue = 1.7f;
        }

        if (humamValue == 5)
        {
            jointValue = 0.2f;
            weightValue = 1.6f;
        }

        for (int i = 0; i < gameObject.transform.GetChildCount(); i++)
        {
            GameObject obj = gameObject.transform.GetChild(i).gameObject;
            if (obj.GetComponent<BreakJoint>())
            {
                obj.GetComponent<BreakJoint>().velocityForBrake = brakeValeu * jointValue;
            }
        }
    }
}
