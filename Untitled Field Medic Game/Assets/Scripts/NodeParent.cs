using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeParent : MonoBehaviour
{
    public Node nodePrefab;
    public List<Node> nodeList;
    [ContextMenu("Create Nodes")]
    public void MakeNodes()
    {
        for(int x = -50; x < 50; x += 5)
        {
            for(int y = -10; y < 50; y += 5)
            {
                Node n = Instantiate(nodePrefab, new Vector2(x,y-0.5f), Quaternion.identity, transform);
                nodeList.Add(n);
            }
        }
    }

    [ContextMenu("Remove Empty Nodes")]
    public void RemoveNodes()
    {
        nodeList.RemoveAll(n => n == null);

        foreach(Transform child in transform) 
            DestroyImmediate(child.gameObject);

        nodeList.Clear();
    }

    [ContextMenu("Connect Nodes")]
    public void ConnectNodes()
    {
        for(int i = 0; i < nodeList.Count; i++)
        {
            for(int j = i+1; j < nodeList.Count;j++)
            {
                if (Vector2.Distance(nodeList[i].transform.position, nodeList[j].transform.position) <= 5.5f)
                {
                    nodeList[i].connections.Add(nodeList[j]);
                    nodeList[j].connections.Add(nodeList[i]);
                }
            }
        }
    }
}
