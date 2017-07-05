public class Objective {

    private int id;
    private string description;
    private bool completed;
    private int progress;
    private int goal;

    public Objective(int id, string description, int goal)
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

    public string GetDescription()
    {
        return this.description;
    }

    public bool UpdateProgress()
    {
        ++(this.progress);
        return CheckCompleted();
    }

    private bool CheckCompleted()
    {
        this.completed = this.goal == this.progress;
        return this.completed;
    }
}
