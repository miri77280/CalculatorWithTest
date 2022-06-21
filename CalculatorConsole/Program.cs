// See https://aka.ms/new-console-template for more information
using MyCalculator;
using Ninject;
using System.Reflection;

Console.WriteLine("Hello, World!");
var kernel = new StandardKernel();
kernel.Load(Assembly.GetExecutingAssembly());
var dataSaver = kernel.Get<IDataSaver>();
var calculator = new Calculator(dataSaver);
calculator.Add(2);
Console.Write(calculator.GetValue());
Console.Write(calculator.GetValue());



