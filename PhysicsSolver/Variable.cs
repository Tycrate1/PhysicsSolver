class Variable
{
    private string name;
    private bool known;
    private double value;

    public Variable(string name, bool known, double value = 0.0)
    {
        this.name = name;
        this.known = known;
        this.value = value;
    }

    public string GetName() => name;
    public bool IsKnown() => known;
    public double GetValue() => value;
}
