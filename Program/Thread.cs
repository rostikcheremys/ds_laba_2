namespace Program;

public static class CalcThread
{
    public static double E;

    public delegate double MyFunc(double x);

    public static double Calc(double x, MyFunc g)
    {
        double d;
        
        do
        {
            double temp = g(x);
            
            d = Math.Abs(temp - x);
            x = temp;
        } while (d >= E);

        return x;
    }

    public static double G1(double x)
    {
        return 0.5 * Math.Cos(x);
    }

    public static double G2(double x)
    {
        return (2 * x - Math.Log(x)) / 3;
    }
}