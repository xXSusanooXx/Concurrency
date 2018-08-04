using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait1
{
	class Program
	{
		static void Main(string[] args)
		{
			OneByOne();
			OneOfTwo();
			Console.Read();
		}

		static async void OneByOne()
		{
			var t1 = DelayAndReturnAsync(4);
			var t2 = DelayAndReturnAsync(2);
			var t3 = DelayAndReturnAsync(7);

			var tasks = new List<Task<int>>() { t1, t2, t3 };
			
			var processingTasks = tasks.Select(async t =>
			{
				var result = await t;
				Console.WriteLine(result);
			}).ToArray();

			await Task.WhenAll(processingTasks);
		}

		static async void OneOfTwo()
		{
			var t3 = DelayAndReturnAsync(7);
			var t2 = DelayAndReturnAsync(3);

			var fastest = await Task.WhenAny(t2, t3);
			Console.WriteLine(await fastest);
		}

		static async Task<int> DelayAndReturnAsync(int val)
		{
			await Task.Delay(TimeSpan.FromSeconds(val));
			return val;
		}
	}


}
