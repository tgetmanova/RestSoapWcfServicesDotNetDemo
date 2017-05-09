using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace UserRepositoryServiceTests
{
    /// <summary>
    /// Test Util methods.
    /// </summary>
    internal class TestUtils
    {
        /// <summary>
        /// Aggregates the assertions.
        /// </summary>
        /// <param name="actions">The actions.</param>
        /// <exception cref="Xunit.Sdk.XunitException">Exception aggregating all error messages. </exception>
        internal static void AggregateAssertions(params Action[] actions)
        {
            var exceptionMessages = new List<string>();

            foreach (var action in actions)
            {
                try
                {
                    action.Invoke();
                }
                catch (Xunit.Sdk.XunitException xUnitException)
                {
                    exceptionMessages.Add(xUnitException.Message);
                }
            }

            if (exceptionMessages.Any())
            {
                throw new Xunit.Sdk.XunitException(string.Join($";{Environment.NewLine}", exceptionMessages));
            }
        }

        public static bool IsDateTimeInTheExpectedRange(DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }
    }
}
