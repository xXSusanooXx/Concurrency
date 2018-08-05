using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Par = System.Threading.Tasks.Parallel;

namespace Parallel
{
	class Program
	{
		static void Main(string[] args)
		{
			var a = new List<A>();

			for(var i = 0;i< 100; i++)
			{
				a.Add(new A()
				{
					I = new Random().Next(1, 9),
					Incorrect = new Random().Next(0, 3) == 0 ? true : false
				});
			}

			Handle(a);

			
		}

		public static void Handle(List<A> aArray)
		{
			int incorrectCount = 0;
			object o = new object();
			Par.ForEach(aArray, async a =>
			 {
				 if (a.Incorrect)
				 {
					 lock (o)
					 {
						 incorrectCount++;
					 }
				 }
				 else
				 {
					 Thread.Sleep(a.I*1000);
					 Console.WriteLine(a.I);
				 }
			 });
			Console.WriteLine($"Total incorrect {incorrectCount}");
		}
	}

	class A
	{
		public int I;
		public bool Incorrect;
	}
}
