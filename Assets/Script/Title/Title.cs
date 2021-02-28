using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public BaseGear ha;
    public BaseGear guruma;
    public BaseGear pa;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ha.RotateGear(Time.deltaTime, 1);
        guruma.RotateGear(Time.deltaTime, -1);
        pa.RotateGear(Time.deltaTime, 1);
    }
}
