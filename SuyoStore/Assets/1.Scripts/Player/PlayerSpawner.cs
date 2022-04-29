using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    GameObject player;
    Transform arriveTransform;
    enum eDirection { UP, DOWN };
    [SerializeField] eDirection dirType;
    enum eTargetFloor { B2, B1, F1, F2, F3, Looftop };
    [SerializeField] eTargetFloor targetFloor;

    public GameObject ArrivePoint;

    float timer;
    float stayEscalatorTime;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = 0.0f;
        stayEscalatorTime = 1.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Gate On");
            GameManager.GM.ChangeToOtherScene(-1);
            player.GetComponent<Transform>().position += Vector3()

            //if (this.dirType == eDirection.UP || this.dirType == eDirection.DOWN)
            //{
            //    player.GetComponent<PlayerController>().isChangeFloor = true;
            //    player.transform.position = ArrivePoint.transform.position;
            //}
        }
    }

    //void GoUp()
    //{
    //    switch (targetFloor)
    //    {
    //        case eTargetFloor.B1:
    //            ArrivePoint = GameObject.Find("Up-arrive B1");
    //            break;
    //        case eTargetFloor.F1:
    //            ArrivePoint = GameObject.Find("Up-arrive F1");
    //            break;
    //        case eTargetFloor.F2:
    //            ArrivePoint = GameObject.Find("Up-arrive F2");
    //            break;
    //        case eTargetFloor.F3:
    //            ArrivePoint = GameObject.Find("Up-arrive F3");
    //            break;
    //        case eTargetFloor.Looftop:
    //            Debug.Log("This floor need MUST ITEM.");
    //            break;
    //        default:
    //            Debug.Log("This floor is last. Can't go Up.");
    //            break;
    //    }
    //    player.transform.position = ArrivePoint.transform.position;
    //}

    //void GoDown()
    //{
    //    switch (targetFloor)
    //    {
    //        case eTargetFloor.B2:
    //            ArrivePoint = GameObject.Find("Down-arrive B2");
    //            break;
    //        case eTargetFloor.B1:
    //            ArrivePoint = GameObject.Find("Down-arrive B1");
    //            break;
    //        case eTargetFloor.F1:
    //            ArrivePoint = GameObject.Find("Down-arrive F1");
    //            break;
    //        case eTargetFloor.F2:
    //            ArrivePoint = GameObject.Find("Down-arrive F2");
    //            break;
    //        default:
    //            Debug.Log("This floor is last. Can't go Down.");
    //            break;
    //    }
    //    player.transform.position = ArrivePoint.transform.position;
    //}

}
