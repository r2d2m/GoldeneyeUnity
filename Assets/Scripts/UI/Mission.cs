using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mission : MonoBehaviour {

    private Text messages;
	private List<Objective> objectives;

    private InGameMenu _inGameMenu;

	void Start () {
        _inGameMenu = GameObject.Find("Main Camera").GetComponent<InGameMenu>();

        // TODO: Create on menu selection
        PlayerPrefs.SetString("Mission0", "Neutralize all alarms");
        PlayerPrefs.SetString("Mission1", "Install covert modem");
        PlayerPrefs.SetString("Mission2", "Bungee jump from platform");
        //
        messages = GameObject.Find("Messages").GetComponent<Text>();
		objectives = new List<Objective> ();
		int i = 0;
		while (PlayerPrefs.HasKey("Mission" + i.ToString()))
		{
            string description = PlayerPrefs.GetString("Mission" + i.ToString());
			objectives.Add(new Objective(i, description, 1));
			++i;
		}
	}

	public void UpdateProgress(int objectiveId, bool abort)
	{
		foreach (Objective o in objectives)
		{
			if (o.GetId() == objectiveId)
			{
                MissionStatus status = abort ? o.AbortProgress() : o.UpdateProgress();
				if (status == MissionStatus.Completed)
				{
                    messages.text = "Objective " + char.ConvertFromUtf32(65 + o.GetId()) + " completed";
                    _inGameMenu.UpdateMissionStatus(objectiveId, status);
                    StartCoroutine(RemoveText());
                }
                else if (status == MissionStatus.Failed)
                {
                    messages.text = "Objective " + char.ConvertFromUtf32(65 + o.GetId()) + " failed";
                    _inGameMenu.UpdateMissionStatus(objectiveId, status);
                    StartCoroutine(RemoveText());
                }
			}
		}
	}

    public List<Objective> GetObjectives()
    {
        return this.objectives;
    }

    private IEnumerator RemoveText()
    {
        yield return new WaitForSeconds(5);
        messages.text = "";
    }
}
