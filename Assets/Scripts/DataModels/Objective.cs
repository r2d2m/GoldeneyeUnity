public class Objective {

    private int id;
    private string description;
    private MissionStatus completed;
    private int progress;
    private int goal;

    public Objective(int id, string description, int goal)
    {
        this.id = id;
        this.description = description;
        this.completed = MissionStatus.Incomplete;
        this.goal = goal;
    }

    public int GetId()
    {
        return this.id;
    }

    public string GetDescription()
    {
        return this.description;
    }

    public MissionStatus GetStatus()
    {
        return this.completed;
    }

    public MissionStatus UpdateProgress()
    {
        ++(this.progress);
        return CheckCompleted();
    }

    public MissionStatus AbortProgress()
    {
        this.progress = -1;
        return CheckCompleted();
    }

    private MissionStatus CheckCompleted()
    {
        if (this.goal == this.progress)
        {
            this.completed = MissionStatus.Completed;
        }
        else if (this.progress == -1)
        {
            this.completed = MissionStatus.Failed;
        }
        return this.completed;
    }
}
