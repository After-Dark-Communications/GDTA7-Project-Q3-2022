using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePulse : MonoBehaviour
{
    [SerializeField] private AnimationCurve mineEmissionPulseCurve;
    [SerializeField] private Material material;

    private float mineTime;

    // Start is called before the first frame update
    void Start()
    {
        material = gameObject.GetComponent<MeshRenderer>().materials[0];
        mineTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        mineTime += Time.deltaTime;
        material.SetColor("_EmissionColor", new Color(255,0,0) * mineEmissionPulseCurve.Evaluate(mineTime % 2));
    }
}
