using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Data", menuName = "ScriptableObjects/BlocksConnectionsCollection", order = 1 )]
public class BlocksConnectionCollection : ScriptableObject
{
    public List<Entry> entries;

    [Serializable]
    public class Entry {
        public GameObject block;
        public AvailableConnection[] availableConnections;
    }

    [Serializable]
    public class AvailableConnection {
        public GameObject block;
        public float yIncrement;
    }
}
