using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateFloor : MonoBehaviour
{
    public BlocksConnectionCollection collection;
    float xSpacing = 5f;

    void Start()
    {
        Random.InitState( System.DateTime.Now.Millisecond );
        StartCoroutine( GenerateFloorPlan() );
    }

    private IEnumerator GenerateFloorPlan() {

        Vector3 position = Vector3.zero;

        GameObject g = Instantiate( collection.entries[0].block, position, Quaternion.identity, transform );
        int nextBlockAvailableBlockIndex = Random.Range( 0, collection.entries[0].availableConnections.Length - 1 );
        int index = GetIndexOfNextBlock( nextBlockAvailableBlockIndex, collection.entries[0].availableConnections );
        position.x += xSpacing;
        position.y += collection.entries[0].availableConnections[nextBlockAvailableBlockIndex].yIncrement;
        yield return new WaitForEndOfFrame();

        for( int i = 0; i < 10; i++ ) {
            g = Instantiate( collection.entries[index].block, position, Quaternion.identity, transform );
            nextBlockAvailableBlockIndex = Random.Range( 0, collection.entries[index].availableConnections.Length - 1 );
            position.x += xSpacing;
            position.y += collection.entries[index].availableConnections[nextBlockAvailableBlockIndex].yIncrement;
            index = GetIndexOfNextBlock( nextBlockAvailableBlockIndex, collection.entries[index].availableConnections );
            yield return new WaitForEndOfFrame();
        }
        //I got lazy generating end piece
        g = Instantiate( collection.entries[index].block, position, Quaternion.identity, transform );
        index = GetEndPieceIndex(g);
        position.x += xSpacing;
        //position.y += collection.entries[index].availableConnections[nextBlockAvailableBlockIndex].yIncrement;
        //index = GetIndexOfNextBlock( nextBlockAvailableBlockIndex, collection.entries[index].availableConnections );
        g = Instantiate( collection.entries[index].block, position, Quaternion.identity, transform );
        yield return new WaitForSeconds( 0f );
    }

    int GetIndexOfNextBlock(int index, BlocksConnectionCollection.AvailableConnection[] connections ) {
        for( int i = 0; i < collection.entries.Count; i++ ) {
            if( connections[index].block.name == collection.entries[i].block.name ) {
                return i;
            }
        }

        return 0;
    }

    //Fix this stupid code
    int GetEndPieceIndex(GameObject g) {
        if( g.name.Contains( "1x2" ) ) {
            return 11;
        }
        return 10;
    }

}
