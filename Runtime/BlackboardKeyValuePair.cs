using System;
using UnityEngine;

// This is a special type to represent a key binding, with a value.
// The BlackboardKeyValuePairPropertyDrawer takes care of updating the rendered value field
// when the key type changes.
[Serializable]
public class BlackboardKeyValuePair
{
    [SerializeReference] public BlackboardKey key;

    [SerializeReference] public BlackboardKey value;

    public void WriteValue()
    {
        if (key != null && value != null) key.CopyFrom(value);
    }
}