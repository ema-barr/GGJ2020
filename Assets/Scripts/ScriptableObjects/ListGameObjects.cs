using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ListGameObjects : ScriptableObject, ISerializationCallbackReceiver
{
  [HideInInspector]
  public List<GameObject> list;



  public void OnBeforeSerialize()
  {
    list = new List<GameObject>();
  }

  public void OnAfterDeserialize()
  {
    list = new List<GameObject>();
  }

}
