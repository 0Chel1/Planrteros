using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LegProceduralAnimation2 : MonoBehaviour
{
    public List<GameObject> Joints;
    public List<Transform> Joints2;
    public List<float> distances;
    public Transform BodyJoint;
    public Transform PointToStep;
    public Transform StepPoint;
    public LegProceduralAnimation2 neighbourLeg;
    public bool grounded;
    public float LegMovingSpeed = 10;
    public float duration = 1f;
    private bool loop = true;
    public int LegNumber;

    public float DistanceBtwSteps = 1.3f;
    void Start()
    {
        for (int i = 0; i < Joints.Count; i++)
        {
            distances[i] = Vector2.Distance(Joints[i].transform.position, Joints2[i].position);
        }
        StartCoroutine(UpdatePos());
    }

    void Update()
    {
        float distBtwSteps = Vector2.Distance(StepPoint.position, PointToStep.position);
        if (distBtwSteps > DistanceBtwSteps && grounded)
        {
            if(Joints.Count == 1)
                PointToStep.position = Vector2.Lerp(PointToStep.position, StepPoint.position, LegMovingSpeed);
            else if(Joints.Count == 2)
                PointToStep.transform.DOMove(StepPoint.position, LegMovingSpeed);
            grounded = false;
        }
        else if (neighbourLeg.grounded == false && !grounded)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator UpdatePos()
    {
        while (true)
        {
            if(Joints.Count == 1)
            {
                Joints[0].transform.position = BodyJoint.position;
                Vector3 direction = Joints[0].transform.position - PointToStep.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.Euler(new Vector3(0, 0, angle));
                Joints[0].transform.rotation = targetRot;
                float distBtwJoints = Vector2.Distance(Joints[0].transform.position, PointToStep.position);
                Joints[0].transform.localScale = new Vector2(distBtwJoints, 1f);
                yield return new WaitForSeconds(0.01f);

            }
            else if (Joints.Count == 2)
            {
                for (int i = 0; i < Joints.Count; i++)
                {
                    distances[i] = Vector2.Distance(Joints[i].transform.position, Joints2[i].position);
                }
                Joints[0].transform.position = BodyJoint.position;
                Joints[1].transform.position = Joints2[0].position;
                Vector3 direction = Joints[1].transform.position - PointToStep.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.Euler(new Vector3(0, 0, angle));
                Joints[1].transform.rotation = targetRot;
                float distBtwJoints = Vector2.Distance(Joints[1].transform.position, PointToStep.position);
                float distanceToMove = distBtwJoints - distances[1];
                Vector3 newPos = Joints[1].transform.position - direction * distanceToMove;
                Joints[1].transform.position = newPos;
                float BodyDist = Vector2.Distance(BodyJoint.position, PointToStep.position);
                if (BodyDist > 5)
                {
                    StartCoroutine(ScaleOverTime(new Vector2(1.5f, 1f), 1));
                }
                else if(BodyDist < 5 && BodyDist > 3)
                {
                    StartCoroutine(ScaleOverTime(new Vector2(1f, 1f), 1));
                }
                else if (BodyDist < 3)
                {
                    StartCoroutine(ScaleOverTime(new Vector2(0.5f, 1f), 1));
                }
                Vector3 direction2 = Joints[0].transform.position - Joints[1].transform.position;
                float angle2 = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;
                if (LegNumber == 1 || LegNumber == 4)
                {
                    Quaternion targetRot2 = Quaternion.Euler(new Vector3(0, 0, angle2 - 10f));
                    Joints[0].transform.rotation = targetRot2;
                }
                else if(LegNumber == 2 || LegNumber == 3)
                {
                    Quaternion targetRot2 = Quaternion.Euler(new Vector3(0, 0, angle2 + 10f));
                    Joints[0].transform.rotation = targetRot2;
                }
                Joints[1].transform.position = Joints2[0].position;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    IEnumerator Wait()
    {
        if (Joints.Count > 1)
        {
            grounded = false;
            yield return new WaitForSeconds(0.1f);
            grounded = true;
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            grounded = true;
        }
    }

    private IEnumerator ScaleOverTime(Vector3 endScale, float duration)
    {
        Vector3 startScale = Joints[1].transform.localScale; // Начальный размер
        float elapsedTime = 0f; // Время, прошедшее с начала интерполяции

        while (loop)
        {
            // Увеличение размера
            while (elapsedTime < duration)
            {
                Joints[1].transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
                elapsedTime += Time.deltaTime; // Увеличиваем время
                yield return null; // Ждем следующего кадра
            }

            // Обратное уменьшение размера
            elapsedTime = 0f; // Сброс времени
            while (elapsedTime < duration)
            {
                Joints[1].transform.localScale = Vector3.Lerp(endScale, startScale, elapsedTime / duration);
                elapsedTime += Time.deltaTime; // Увеличиваем время
                yield return null; // Ждем следующего кадра
            }

            // Сброс времени для следующего цикла
            elapsedTime = 0f;
        }

        // Убедитесь, что размер установлен точно в конечный размер
        Joints[1].transform.localScale = startScale;
    }
}
