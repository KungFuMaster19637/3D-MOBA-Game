using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Teleporter : MonoBehaviour
{
    [Header ("Teleport targets")]
    public Transform teleportToLocationA;
    public Transform teleportToLocationB;

    [Header ("Screen fader")]    
    public Image fader;
    public AnimationCurve curve;

    public GameObject player;
    public bool isInCastle;

    private NavMeshAgent agent;

    [SerializeField] private GameObject hideCastle;
    [SerializeField] private GameObject hideWild;

    private void Start()
    {
        isInCastle = true;
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
        if (isInCastle)
        {
            StartCoroutine(teleportToWild());
        }

        //Teleport to city
        else if (!isInCastle)
        {
            StartCoroutine(teleportToCastle());
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

    IEnumerator teleportToCastle()
    {
        hideWild.SetActive(false);
        hideCastle.SetActive(true);
        player.transform.position = teleportToLocationA.transform.position;
        isInCastle = true;
        yield return null;

    }

    IEnumerator teleportToWild()
    {
        hideWild.SetActive(true);
        hideCastle.SetActive(false);
        player.transform.position = teleportToLocationB.transform.position;
        isInCastle = false;
        yield return null;

    }
}
