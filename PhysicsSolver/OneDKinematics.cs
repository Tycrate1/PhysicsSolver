// Example of inheritence, a child of the general kinematicsClass, and PhysicsClass
class OneDKinematics : KinematicsProblem
{
    // Example of encapsulation, bundling all the data and methods into a single class
    private Variable deltaDistance;
    private List<Variable> velocities = new List<Variable>();
    private Variable deltaTime;
    private Variable acceleration;

    public OneDKinematics(string problemStatement, double precision = 3) 
        : base(problemStatement, precision) 
    {
        deltaDistance = GetVariable("distance");

        for (int i = 0; i < 2; i++)
        {
            velocities.Add(GetVariable($"{i+1}velocity"));
        }

        deltaTime = GetVariable("time");

        acceleration = GetVariable("acceleration");
    }

    public bool ValidVariables()
    {
        int totalKnown = 0;

        if (deltaDistance.IsKnown())
        {
            totalKnown++;
        }
        for (int i = 0; i < 2; i++)
        {
            if (velocities[i].IsKnown())
            {
                totalKnown++;
            }
        }
        if (deltaTime.IsKnown())
        {
            totalKnown++;
        }
        if (acceleration.IsKnown())
        {
            totalKnown++;
        }

        if (totalKnown >= 3)
        {
            return true;
        }
        return false;
    }

    public List<Variable> ReturnValues()
    {
        List<Variable> result = new List<Variable>();
        if (deltaDistance.IsKnown())
        {
            result.Add(deltaDistance);
        }
        if (velocities[0].IsKnown())
        {
            result.Add(velocities[0]);
        }
        if (velocities[1].IsKnown())
        {
            result.Add(velocities[1]);
        }
        if (deltaTime.IsKnown())
        {
            result.Add(deltaTime);
        }
        if (acceleration.IsKnown())
        {
            result.Add(acceleration);
        }

        if (!velocities[1].IsKnown() && velocities[0].IsKnown() && acceleration.IsKnown() && deltaTime.IsKnown())
        {
            // v = v_0 + at
            double vf = velocities[0].GetValue() + (acceleration.GetValue() * deltaTime.GetValue());
            Variable finalVel = new Variable("2velocity", true, vf);
            result.Add(finalVel);
        }
        if (!velocities[0].IsKnown() && velocities[1].IsKnown() && acceleration.IsKnown() && deltaTime.IsKnown())
        {
            // v_0 = v_f - at
            double vi = velocities[1].GetValue() - (acceleration.GetValue() * deltaTime.GetValue());
            Variable initVel = new Variable("1velocity", true, vi);
            result.Add(initVel);
        }
        if (!acceleration.IsKnown() && velocities[0].IsKnown() && velocities[1].IsKnown() && deltaTime.IsKnown())
        {
            // a = (v_f - v_0)/t
            double a = (velocities[1].GetValue() - velocities[0].GetValue())/deltaTime.GetValue();
            Variable acel = new Variable("acceleration", true, a);
            result.Add(acel);
        }
        if (!deltaTime.IsKnown() && velocities[0].IsKnown() && velocities[1].IsKnown() && acceleration.IsKnown())
        {
            // t = (v_f - v_0)/a
            double t = (velocities[1].GetValue() - velocities[0].GetValue()) / acceleration.GetValue();
            Variable time = new Variable("time", true, t);
            result.Add(time);
        }

        if (!deltaDistance.IsKnown() && velocities[0].IsKnown() && deltaTime.IsKnown() && acceleration.IsKnown())
        {
            // d = v_0(t) + 1/2(a)(t^2)
            double d = (velocities[0].GetValue() * deltaTime.GetValue()) + (0.5 * acceleration.GetValue() * Math.Pow(deltaTime.GetValue(), 2));
            Variable dist = new Variable("distance", true, d);
            result.Add(dist);
        }
        if (!velocities[0].IsKnown() && deltaDistance.IsKnown() && deltaTime.IsKnown() && acceleration.IsKnown())
        {
            // v_0 = (d - 1/2(a)(t^2))/t
            double vi = (deltaDistance.GetValue() - (0.5 * acceleration.GetValue() * Math.Pow(deltaTime.GetValue(), 2)))/deltaTime.GetValue();
            Variable initVel = new Variable("1velocity", true, vi);
            result.Add(initVel);
        }
        if (!deltaTime.IsKnown() && velocities[0].IsKnown() && deltaDistance.IsKnown() && acceleration.IsKnown())
        {
            // t = (-v_0 +- sqrt{v_0^2 -2ad})/a
            double t_1 = (-velocities[0].GetValue() + Math.Sqrt(Math.Pow(velocities[0].GetValue(), 2) - (2 * acceleration.GetValue() * deltaDistance.GetValue()))) / acceleration.GetValue(); 
            double t_2 = (-velocities[0].GetValue() - Math.Sqrt(Math.Pow(velocities[0].GetValue(), 2) - (2 * acceleration.GetValue() * deltaDistance.GetValue()))) / acceleration.GetValue();
            double t = (t_1 > 0) ? t_1 : (t_2 > 0) ? t_2 : double.NaN;
            Variable time = new Variable("time", true, t);
            result.Add(time);
        }
        if (!acceleration.IsKnown() && velocities[0].IsKnown() && deltaTime.IsKnown() && deltaDistance.IsKnown())
        {
            // a = (2(d - v_0t)/t^2
            double a = (2 * (deltaDistance.GetValue() - (velocities[0].GetValue() * deltaTime.GetValue()))) / Math.Pow(deltaTime.GetValue(), 2);
            Variable acel = new Variable("acceleration", true, a);
            result.Add(acel);
        }

        if (!velocities[1].IsKnown() && velocities[0].IsKnown() && deltaDistance.IsKnown() && acceleration.IsKnown())
        {
            // v = sqrt{v_0^2 + 2ad}
            double vf = Math.Sqrt(Math.Pow(velocities[0].GetValue(), 2) + (2 * acceleration.GetValue() * deltaDistance.GetValue()));
            Variable finalVel = new Variable("2velocity", true, vf);
            result.Add(finalVel);
        }
        if (!velocities[0].IsKnown() && velocities[1].IsKnown() && deltaDistance.IsKnown() && acceleration.IsKnown())
        {
            // v_0 = sqrt{v^2 - 2ad}
            double vi = Math.Sqrt(Math.Pow(velocities[1].GetValue(), 2) - (2 * acceleration.GetValue() * deltaDistance.GetValue()));
            Variable initVel = new Variable("1velocity", true, vi);
            result.Add(initVel);
        }
        if (!deltaDistance.IsKnown() && velocities[0].IsKnown() && velocities[1].IsKnown() && acceleration.IsKnown())
        {
            // d = (v^2-v_0^2)/2a
            double d = (Math.Pow(velocities[1].GetValue(), 2) - Math.Pow(velocities[0].GetValue(), 2)) / (2 * acceleration.GetValue());
            Variable dist = new Variable("distance", true, d);
            result.Add(dist);
        }
        if (!acceleration.IsKnown() && velocities[0].IsKnown() && deltaDistance.IsKnown() && velocities[1].IsKnown())
        {
            // a = (v^2-v_0^2)/2d
            double a = (Math.Pow(velocities[1].GetValue(), 2) - Math.Pow(velocities[0].GetValue(), 2))/(2 * deltaDistance.GetValue());
            Variable acel = new Variable("aceleration", true, a);
            result.Add(acel);
        }
        return result;
    }

    private Variable GetVariable(string type)
    {
        Console.WriteLine($"What is the {type}('Uknown' if value isnt present)? ");
        string value = Console.ReadLine();
        if (value == "Uknown")
        {
            Variable newVar = new Variable($"{type}", false);
            return newVar;
        }
        else
        {
            Variable newVar = new Variable($"{type}", true, Convert.ToDouble(value));
            return newVar;
        }
    }
}