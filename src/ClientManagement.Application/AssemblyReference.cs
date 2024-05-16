using System.Reflection;

namespace ClientManagement.Application;

public static class AssemblyReference
{
    public static Assembly Assembly = typeof(AssemblyReference).Assembly;
}