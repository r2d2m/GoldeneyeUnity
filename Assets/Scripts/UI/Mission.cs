using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mission : MonoBehaviour {

    private Text messages;
	private List<Objective> objectives;

    private InGameMenu _inGameMenu;

	void Start () {
        // TODO: Create on menu selection
        PlayerPrefs.SetString("Mission0", "Neutralize all alarms");
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

	public void UpdateProgress(int objectiveId)
	{
		foreach (Objective o in objectives)
		{
			if (o.GetId() == objectiveId)
			{
				if (o.UpdateProgress())
				{
                    messages.text = "Objective " + char.ConvertFromUtf32(65) + " completed";
                    _inGameMenu.UpdateMissionStatus(objectiveId, MissionStatus.Completed);
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
