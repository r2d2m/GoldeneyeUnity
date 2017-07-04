using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {

	private List<Objective> objectives;

	void Start () {
		objectives = new List<> ();
		int i = 0;
		while (PlayerPrefs.HasKey("Mission" + i.ToString))
		{
			objectives.Add(PlayerPrefs.GetString ("Mission" + i.toString));
			++i;
		}
	}

	public void UpdateProgress(int objectiveId)
	{
		foreach (Objective o in objectives)
		{
			if (o.GetId() == objectiveId)
			{
				if (o.UpdateProgress ())
				{
					Debug.Log ("Mission acomplished");
				}
			}
		}
	}

	private class Objective
	{
		private int id;
		private string description;
		private bool completed;
		private int progress;
		private int goal;

		public Objective(int id, string desctiption, int goal)
		{
			this.id = id;
			this.description = description;
			this.completed = false;
			this.goal = goal;
		}

		public int GetId()
		{
			return this.id;
		}

		public bool UpdateProgress()
		{
			++(this.progress);
			return CheckCompleted();
		}

		private void CheckCompleted()
		{
			this.completed = this.goal == this.progress;
		}
	}
}
