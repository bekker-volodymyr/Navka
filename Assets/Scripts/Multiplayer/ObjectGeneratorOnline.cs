using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectGeneratorOnline : MonoBehaviour
{
    [SerializeField] private GameObject parentPrefab;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] PhotonView myPV;
    ObjList objList;


    // Start is called before the first frame update
    void Start()
    {
        //maybe clean all layer

        if (PhotonNetwork.IsMasterClient)
        {
            ObjList ObjList = GetAllChildPositions(parentPrefab);

            string objString = JsonUtility.ToJson(objList);
            myPV.RPC("RPC_SyncObjects", RpcTarget.OthersBuffered, objString);
        }
    }


    ObjList GetAllChildPositions(GameObject parent)
    {
        ObjList childPositions = new ObjList();
        foreach (Transform child in parent.transform)
        {
            childPositions.list.Add(child.position);
            // Recursively get positions of children of the child object, if you need to go deeper
            ObjList grandChildPositions = GetAllChildPositions(child.gameObject);
            childPositions.list.AddRange(grandChildPositions.list);
        }
        return childPositions;
    }


    [PunRPC]
    public void RPC_SyncObjects(string objects)
    {
        Debug.Log(objects);
        objList = JsonUtility.FromJson<ObjList>(objects);
        Debug.Log(objList.list.Count);
        for(int i=0; i<objList.list.Count; i++)
        {
            Debug.Log(objList.list[i]);
            GameObject _object = Instantiate(objectPrefab, transform);
            _object.transform.position = new Vector3(objList.list[i].x, objList.list[i].y, 0);
        }
    }
}

class ObjList
{
    public List<Vector2> list;
}