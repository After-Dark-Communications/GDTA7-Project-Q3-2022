using Parts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private List<Part> lstParts = new List<Part>();

    public List<Part> LstParts { get => lstParts; set => lstParts = value; }
}
