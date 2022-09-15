using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Part : MonoBehaviour
{

    [SerializeField]
    private Image partIcon;

    [SerializeField]
    private ConnectionPointsCollection connectionPointCollection;

    public virtual string partCategoryName => "part";
    public ConnectionPointsCollection ConnectionPointCollection => connectionPointCollection;

}
