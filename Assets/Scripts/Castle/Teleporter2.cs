using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Teleporter2 : MonoBehaviour
{
    [Header("Teleport targets")]
    public Transform teleportToCaveEntranceTarget;
    public Transform teleportToCaveTarget;

    [Header("Screen fader")]
    public Image fader;
    public AnimationCurve curve;

    public GameObject player;
    public bool isInCave;

    private NavMeshAgent agent;

    private void Start()
    {
        isInCave = false;
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
    }
    public void ButtonToTeleport()
    {
        StartCoroutine(startTeleporting());
    }

    public IEnumerator startTeleporting()
    {
        agent.enabled = false;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            fader.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        //Teleport to wild
        if (!isInCave)
        {
            StartCoroutine(teleportToCave());
        }

        //Teleport to cave
        else if (isInCave)
        {
            StartCoroutine(teleportToCaveEntrance());
        }



        agent.enabled = true;

        yield return new WaitForSeconds(0.5f);

        float b = 1f;

        while (b > 0f)
        {
            b -= Time.deltaTime;
            float a = curve.Evaluate(b);
            fader.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }


        yield return null;
    }

    IEnumerator teleportToCaveEntrance()
    {
        player.transform.position = teleportToCaveEntranceTarget.transform.position;
        isInCave = false;
        yield return null;

    }

    IEnumerator teleportToCave()
    {
        player.transform.position = teleportToCaveTarget.transform.position;
        isInCave = true;
        yield return null;

    }
}
