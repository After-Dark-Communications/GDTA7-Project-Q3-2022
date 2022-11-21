using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    private const float Speed = 30f;
    private const float Interval = 10f;

    [SerializeField]
    private List<Transform> startPosition;

    [SerializeField]
    private List<Transform> endPosition;

    [SerializeField]
    private Transform rock;

    [SerializeField]
    private bool lookat;

    private float currentInterval = 0;
    private Vector3 currentTargetPos;

    private void Start()
    {
        MoveRockToRandomStartPos();
    }

    private void Update()
    {
        currentInterval += Time.deltaTime;

        if (currentInterval >= Interval)
        {
            currentInterval = 0;
            MoveRockToRandomStartPos();
        }

        rock.position = Vector3.MoveTowards(rock.position, currentTargetPos, Speed * Time.deltaTime);

        if (lookat)
            rock.LookAt(currentTargetPos);
    }

    private void MoveRockToRandomStartPos()
    {
        rock.gameObject.SetActive(false);
        rock.position = RandomPos(startPosition);
        currentTargetPos = RandomPos(endPosition);
        rock.gameObject.SetActive(true);
    }

    private Vector3 RandomPos(List<Transform> randomList)
    {
        Vector3 pos;

        pos = randomList[Random.Range(0, randomList.Count)].position;

        return pos;
    }
}
