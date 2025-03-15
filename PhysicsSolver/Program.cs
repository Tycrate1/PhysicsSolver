
class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome!");
        Console.WriteLine("1. Kinematics");
        Console.Write("What physics problem are you working with? ");
        int problemType = Int32.Parse(Console.ReadLine());

        switch(problemType)
        {
            case 1:
                Console.Clear();
                Console.WriteLine("Kinematics!");
                Console.Write("Please enter in the problem statement: ");
                string problemStatement = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("Problem Type!");
                Console.WriteLine("1. 1D Kinematics");
                Console.Write("What kinematics problem are you working with? ");
                int kinematicProblemType = Int32.Parse(Console.ReadLine());

                switch(kinematicProblemType)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("One Dimensional Kinematics!");

                        // Use of abstraction, create an object that calls upon itself to solve the problem, all the details are hidden
                        OneDKinematics solver = new OneDKinematics(problemStatement);
                        if(!solver.ValidVariables())
                        {
                            Console.WriteLine("Not enough information to solve!");
                            break;
                        }
                        List<Variable> answers = solver.ReturnValues();

                        Console.Clear();
                        Console.WriteLine(problemStatement);
                        foreach (Variable v in answers)
                        {
                            Console.WriteLine(v.GetName() + ": " + v.GetValue());
                        }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                Console.WriteLine("That is not a valid option.");
                break;
        }
    }
}