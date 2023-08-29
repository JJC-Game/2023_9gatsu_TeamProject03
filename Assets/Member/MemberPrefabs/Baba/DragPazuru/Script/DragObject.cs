using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    private bool buttonDown = false;
    public GameObject hitNowObject; // �q�b�g�����I�u�W�F�N�g���i�[����ϐ�
    public GameObject hitObject;
    public int numRays = 12; // Ray�̖{��
    public float maxRayLength = 100f; // Ray�̍ő咷��

    public Vector3 initialPosition; // �h���b�O�J�n���̈ʒu���L�^
    private Vector3 hitIntialPosition;

    public float time = 0.35f;
    public bool timerFlg = false;
    private void Start()
    {

    }

    public void Update()
    {
        if (buttonDown)
        {
            for (int i = 0; i < numRays; i++)
            {
                float angle = i * (360f / numRays);
                Vector3 rayDirection = Quaternion.Euler(0f, 0f, angle) * Vector3.right;
               // Debug.DrawRay(transform.position, rayDirection * maxRayLength, Color.red); // Ray������
                RaycastHit hit;
                if (Physics.Raycast(transform.position, rayDirection, out hit, maxRayLength))
                {
                    // �������g�̃I�u�W�F�N�g�łȂ����Ƃ��m�F���Ă��画��
                    if (hit.collider.gameObject != gameObject && hit.collider.gameObject.name == "1")
                    {
                        hitNowObject = hit.collider.gameObject; // �q�b�g�����I�u�W�F�N�g��ϐ��Ɋi�[
                        Debug.Log("�q�b�g�����I�u�W�F�N�g��ۑ����܂���");
                    }
                }
            }
        }
        if (timerFlg == true)
        {
            Timer();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        initialPosition = transform.position; // �����ʒu���L�^
        transform.SetAsLastSibling();
        buttonDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonDown = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        if (hitNowObject != null)
        {
            if (hitNowObject == hitObject)
            {
                timerFlg = true;
            }
            else
            {
                hitObject = hitNowObject;
                hitIntialPosition = hitNowObject.transform.position;
                hitNowObject.transform.position = initialPosition; // �q�b�g�����I�u�W�F�N�g�̈ʒu�� initialPosition �ɕύX
                initialPosition = hitIntialPosition; // �q�b�g�����I�u�W�F�N�g�̈ʒu�� initialPosition �ɑ��

            }
            if (time <= 0)
            {
                if (hitObject)
                {
                    hitObject = null;
                    time = 0.35f;
                    timerFlg = false;
                }
            }
            hitNowObject = null; // �ϐ������Z�b�g
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = initialPosition; // �h���b�O�I�����ɏ����ʒu�ɖ߂�
        hitNowObject = null;
        hitObject = null;
    }
    public void Timer()
    {
        time -= Time.deltaTime;
    }
}