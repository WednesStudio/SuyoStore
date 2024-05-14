using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TreeEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] Define.CameraMode camMode = Define.CameraMode.QuaterView;
	[SerializeField] Vector3 delta = new Vector3(0f, 4f, -4f);
	[SerializeField] GameObject target = null;

	private void Start()
	{
		SetTarget();
	}

	void LateUpdate()
    {
        if (camMode == Define.CameraMode.QuaterView)
        {
			// 장애물 무시
			LayerMask mask = LayerMask.GetMask("Floor") | LayerMask.GetMask("Wall");
			RaycastHit hit;
			if(Physics.Raycast(target.transform.position, delta, out hit, delta.magnitude, mask))
			{
				Vector3 camUp = new Vector3(0f, 2f, 0f);
				float dist = (hit.point - target.transform.position).magnitude * 0.8f;
				transform.position = target.transform.position + camUp + delta.normalized * dist;
			}
			else { 
				// 카메라 positoin 수정
				transform.position = target.transform.position + delta;
			}
			transform.LookAt(target.transform);
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