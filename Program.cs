using System;
using System.Collections.Generic;
using System.Linq;

namespace PoisonedApples
{
    /// <summary>
    /// Poisoned apples LINQ exercise
    /// https://corndel.atlassian.net/wiki/spaces/AC/pages/28835934/6+-+Functional+Programming+and+LINQ
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var appleTree = new AppleTree();
            // Call appleTree.PickApples() to get a sequence of apples.
            var Apples = appleTree.PickApples();
            int counter = 0;
            
            // For the first 10,000 apples:
            // 1. How many apples are poisoned?

            foreach (var apple in Apples.Take(10000).Where(a => a.Poisoned))
            {               
                counter++;
            }
            Console.WriteLine($"{ counter} apples are poisoned");   //244 apples
            // 2. The majority of poisoned apples are Red. Which is the next most common colour
            //    for poisoned apples?
            foreach(var apple in Apples.Take(10000).Where(a => a.Poisoned).GroupBy(c => c.Colour))
            {
                Console.WriteLine($"{apple.Key} {apple.Count()}");  //Yellow is the next most common colour
            }

            // 3. What's the maximum number of non-poisoned Red apples that get picked in
            //    succession?
            var LongestSequence = 0;
            var CurrentSequence = 0;

            foreach(var apple in Apples.Take(10000))
            {
                bool match = apple.Colour == AppleColour.Red && !apple.Poisoned;
                if (match)
                {
                    CurrentSequence++;
                }
                else
                {
                    if (CurrentSequence != 0)
                    {
                        if (CurrentSequence > LongestSequence)
                        {
                            LongestSequence = CurrentSequence;
                        }
                        CurrentSequence = 0;
                    }
                }
            }
            if (CurrentSequence != 0)
            {
                if (CurrentSequence > LongestSequence)
                {
                    LongestSequence = CurrentSequence;
                }
            }
            Console.WriteLine(LongestSequence);

            var i = new int[] { 1, 2, 3 };
            var s = i.Aggregate(0, (a, b) => a + b);

            var aggregatedState = Apples.Take(10000).Aggregate(
                (current: 0, longest: 0),
                (state, apple) =>
                {
                    bool match = apple.Colour == AppleColour.Red && !apple.Poisoned;
                    if (match)
                    {
                        state.current++;
                    }
                    else
                    {
                        if (state.current != 0)
                        {
                            if (state.current > state.longest)
                            {
                                state.longest = state.current;
                            }
                            state.current = 0;
                        }
                    }
                    return state;
                });
            Console.WriteLine(Math.Max(aggregatedState.longest, aggregatedState.current));


            // 4. If you pick a Green apple, how many times will the next apple also be Green?
        }
    }
}
