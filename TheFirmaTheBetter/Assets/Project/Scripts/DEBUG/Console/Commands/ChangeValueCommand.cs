using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//[CreateAssetMenu(fileName = "New Change Value Command", menuName = "Console Commands/Change Value Command")]
    public class ChangeValueCommand : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            if (args != null && args.Length == 3)//object name, value to change, new value
            {
                args[2] = args[2].Replace("\"", "");
                try
                {
                    GameObject obj = GameObject.Find(args[0]);
                    DEBUG_ShipMovement d_ShipMovement = obj.GetComponentInChildren<DEBUG_ShipMovement>();
                    Type type = d_ShipMovement.GetType().GetField(args[1]).FieldType;
                    d_ShipMovement.GetType().GetField(args[1]).SetValue(d_ShipMovement, Convert.ChangeType(args[2], type));//Tries to set the variable in args[2] to the type of args[1]
                    return true;
                }
                catch (Exception e)
                {
                    //Debug.LogError("Caught Exception whilst trying to change value: " + e.Message);
                }
            }
            return false;
        }
    }
