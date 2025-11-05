using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBootStrapper
{
    IEnumerator BootStrap();
}

public class BootManager : MonoBehaviour
{
    private static readonly List<IBootStrapper> initBootStrappers = new();
    public static void Register(IBootStrapper bootStrapper)=>initBootStrappers.Add(bootStrapper);

    private IEnumerator Start()
    {
        foreach (var bootStrapper in initBootStrappers)
        {
            yield return bootStrapper.BootStrap();            
        }
    }
}
