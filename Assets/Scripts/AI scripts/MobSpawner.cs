using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private Night[] nights;
    [SerializeField] private float timeUntilNight = 0;
    [SerializeField] private float nightCountdown = 0;
    [SerializeField] private TextMeshProUGUI nightCountdownText;
    [SerializeField] private TextMeshProUGUI dayCountText;
    [SerializeField] private Time time;

    [SerializeField] private Transform[] spawners;
    [SerializeField] private List<CharacterStats> mobList;

    public enum SpawnState {SPAWNING, WAITING, COUNTING};

    private SpawnState state = SpawnState.COUNTING;

    private int currentNight;

    private int dayCount;

    private void SpawnMob(GameObject mob)
    {
        int randomInt = Random.Range(1, spawners.Length);
        GameObject newMob = Instantiate(mob, spawners[randomInt].position, spawners[randomInt].rotation);
        CharacterStats newMobStats = newMob.GetComponent<CharacterStats>();

        mobList.Add(newMobStats);
    }

    private bool MobsAreDead()
    {
        int i = 0;
        foreach(CharacterStats mob in mobList)
        {
            if(mob.IsDead())
            {
                i++;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator StartNight(Night night)
    {
        state = SpawnState.SPAWNING;

        for(int i = 0; i < night.mobAmount; i++)
        {
            SpawnMob(night.mob);
            yield return new WaitForSeconds(night.countdown);
        }

        yield return new WaitForSeconds(night.countdown);

        state = SpawnState.WAITING;

        yield break;
    }

    // Start is called before the first frame update
    void Start()
    {
        nightCountdown = timeUntilNight;
        currentNight = 0;
    }

    private void Update()
    {
        dayCountText.text = "Day: " + dayCount;
        nightCountdownText.text = nightCountdown.ToString();
        if(state == SpawnState.WAITING) 
        {
            if(!MobsAreDead())
            {
                return;
            }
            else 
            {
                StartDay();
                dayCount++;
            }
        }

        if(nightCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(StartNight(nights[currentNight]));
            }
        } 
        else 
        {
            nightCountdown -= Time.deltaTime;
        }
    }

    private void StartDay()
    {
        Debug.Log("NIGHT PASSED");
        state = SpawnState.COUNTING;
        nightCountdown = timeUntilNight;

        if(currentNight + 1 > nights.Length - 1)
        {
            currentNight = 0;
            Debug.Log("GAME FINISHED");
        }
        else 
        {
            currentNight++;
        }
    }
}
