public class Toggle
{
    private int m_X;
    private int m_Y;
    public bool Value;

    public int X { get => m_X; set => m_X = value; }
    public int Y { get => m_Y; set => m_Y = value; }

    public Toggle(int x, int y)
    {
        m_X = x;
        m_Y = y;
    }

    public override string ToString() => m_Y + "," + m_Y;
}
