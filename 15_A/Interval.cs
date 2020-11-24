public partial class Interval
{
    public double LowerInclusiveBound;
    public double Step;
    public int Count = 0;

    public double RelativeFrequency;
    public double Percentage;

    public bool containsValue(double v) => (v >= LowerInclusiveBound && v < (LowerInclusiveBound + Step));

    public string printInterval()
    {
        return $"[{LowerInclusiveBound} - {LowerInclusiveBound + Step})";
    }
}

