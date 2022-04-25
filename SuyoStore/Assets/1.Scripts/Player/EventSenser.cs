using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSenser : MonoBehaviour
{
    //player에게 붙어있는 스크립트
    //특정 위치/물건을 감지하면 이벤트 업데이트
    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Item")
                {
                    hit.transform.gameObject.GetComponent<ItemControl>().GetThisItem();
                }
            }
        }
    }
    
    //1층 출입구 셔터 앞
    //쉘터 컴퓨터 주변
    //쉘터 컴퓨터 클릭 시
    //쉘터 컴퓨터 클릭 한 번 더 - 카드키 찾은 7일차 전
    //쉘터 컴퓨터 클릭 한 번 더 - 카드키 찾은 이후 7일차
    //옥상으로 가는 에스컬레이터 앞 - 배터리 없음
    //옥상으로 가는 에스컬레이터 앞 - 배터리 1 < n < 10
    //옥상으로 가는 에스컬레이터 앞 - 배터리 10 개
    //지하 2층 처음 도착
    //지하 2층 처음 침낭 보유
    //지하 2층 침낭 사용 - 7일차 전
    //지하 2층 침낭 사용 - 7일차
    

}
