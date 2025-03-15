# Physics Solver
Physics problem solver that demonstrates uses of OOP.

____________________________________________________________________________________________________________________________________________
Getting Started
Visual Studio is recommended on a Windows operating system, other development environments are currently untested.

1. Downloading the repository:
Start with cloning the repository with git clone https://github.com/Tycrate1/PhysicsSolver

2. Running Project:
The default Visual Studio setup should allow you to run this project.

3. Exploring Project:
The project is currently only able to solve 1D Kinematic problems, but the design is set up to allow for easy integration of other physics problems.
  Abstraction: There are many examples of abstraction in this project. A notable example is the OneDKinematics class; this class hides the functionality required to solve the physics problem and presents an easy function to do so.
  Encapsulation: Again, there are many examples of encapsulation in this project, but a great example is the Variable class. This class(more like a structure) holds all the data needed for each variable, allowing the user to work directly with the variable structure rather than each piece individually.
  Inheritance: The file structure is set up so that each physics problem has its own class; these classes each derive from their parent problem type class and the general PhysicsProblem class.
  Polymorphism: At the current state of this project, polymorphism isn't present, but the project is setup to allow use for polymorphism when more problem types are added.
