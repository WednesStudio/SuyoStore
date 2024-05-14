using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TreeEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] Define.CameraMode camMode = Define.CameraMode.QuaterView;
	[SerializeField] Vector3 delta = new Vector3(0f, 6f, -4.6f);
	[SerializeField] Vector3 camUp = new Vector3(0f, 1.6f, 0f);
	[SerializeField] GameObject target = null;

	private void Start()
	{
		SetTarget();
	}

	void LateUpdate()
    {
		if (camMode == Define.CameraMode.QuaterView)
        {
			// 장애물이 타겟과 카메라 사이에 있을 때: 타겟과 카메라 사이의 간격 좁히기
			RaycastHit hit;
			if (Physics.Raycast(target.transform.position, delta, out hit, delta.magnitude, LayerMask.GetMask("Wall")))
			{
				float dist = (hit.point - target.transform.position).magnitude * 0.8f;
				transform.position = target.transform.position + camUp + delta.normalized * dist;
			}
			else if (Physics.Raycast(target.transform.position, delta, out hit, delta.magnitude, LayerMask.GetMask("Floor")))
			{
				Debug.Log("땅!!!");
				float dist = (hit.point - target.transform.position).magnitude * 0.5f;
				transform.position = target.transform.position + camUp + delta.normalized * dist;
			}
			else { 
				// 카메라 positoin 수정
				transform.position = target.transform.position + delta;
			}
			transform.LookAt(target.transform.position + camUp);
		}
	}

	private void SetTarget ()
	{
		target = transform.root.gameObject;
	}

	public void SetQuaterView(Vector3 _delta)
	{
		camMode = Define.CameraMode.QuaterView;
		delta = _delta;
	}

}