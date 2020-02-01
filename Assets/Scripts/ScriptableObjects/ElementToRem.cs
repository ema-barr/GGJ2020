using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ElementToRem : ScriptableObject, ISerializationCallbackReceiver
{
  public GameObject go;

  public void OnBeforeSerialize()
  {

  }

  public void OnAfterDeserialize()
  {
    go = null;
  }
}