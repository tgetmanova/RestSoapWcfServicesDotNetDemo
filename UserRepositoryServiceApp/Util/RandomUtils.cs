using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using SysRandom = System.Random;

namespace UserRepositoryServiceApp.Util
{

    /// <summary>
    /// The utilities to generate random values.
    /// </summary>s
    public static class RandomUtils
    {
        /// <summary>
        /// The randomizer.
        /// </summary>
        private static readonly SysRandom Random = new SysRandom((int)DateTime.Now.Ticks);

        /// <summary>
        /// Gets the random number.
        /// </summary>
        /// <returns>Returns a nonnegative random number.</returns>
        public static int GetRandomInt()
        {
            return Random.Next();
        }

        /// <summary>
        /// Gets the random number.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>Returns a random number in a specified range.</returns>
        public static int GetRandomInt(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        /// <summary>
        /// Gets the random decimal number.
        /// </summary>
        /// <returns>The random decimal number.</returns>
        public static decimal GetRandomDecimal()
        {
            return new decimal(
                lo: GetRandomInt(),
                mid: GetRandomInt(),
                hi: GetRandomInt(),
                isNegative: GetRandomBool(),
                scale: (byte)Random.Next(29));
        }

        /// <summary>
        /// Gets the random boolean value.
        /// </summary>
        /// <returns>Returns true or false.</returns>
        public static bool GetRandomBool()
        {
            var d = Random.NextDouble();
            return d >= 0.5;
        }

        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>Returns a random sequence of char elements.</returns>
        public static string GetRandomString(int size)
        {
            return GetRandomString(size, "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="alphabet">The alphabet.</param>
        /// <returns>Returns a random sequence of specified chars. </returns>
        public static string GetRandomString(int size, string alphabet)
        {
            return string.Join(string.Empty, GetRandomElements(size, alphabet.ToList()));
        }

        /// <summary>
        /// Gets the random string of numbers.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>Returns a random sequence of numbers.</returns>
        public static string GetRandomStringOfNumbers(int size)
        {
            return GetRandomString(size, "0123456789");
        }

        /// <summary>
        /// Gets the random date time.
        /// </summary>
        /// <returns>Returns a random date and time between 1900 and 2199. </returns>
        public static DateTime GetRandomDateTime()
        {
            var y = GetRandomInt(1900, 2100);
            var m = GetRandomInt(1, 12);
            var d = GetRandomInt(1, 28);
            var h = GetRandomInt(0, 23);
            var mm = GetRandomInt(0, 59);
            var ss = GetRandomInt(0, 59);
            return new DateTime(y, m, d, h, mm, ss);
        }

        /// <summary>
        /// Gets the random enumeration value.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <returns>Returns a random enumeration value.</returns>
        public static T GetRandomEnum<T>()
        {
            var values = (T[])Enum.GetValues(typeof(T));
            return values[Random.Next(0, values.Length)];
        }

        /// <summary>
        /// Gets the random element.
        /// </summary>
        /// <typeparam name="T">The type of elements of the collection. </typeparam>
        /// <param name="items">The items.</param>
        /// <returns>The random element of the collection. </returns>
        public static T GetRandomElement<T>(ICollection<T> items)
        {
            return GetRandomElements(items).First();
        }

        /// <summary>
        /// Gets the collection of the random elements.
        /// </summary>
        /// <typeparam name="T">The type of elements of the collection.</typeparam>
        /// <param name="size">The size of the resulting collection.</param>
        /// <param name="items">The source items.</param>
        /// <returns>The collection of the random elements.</returns>
        public static ICollection<T> GetRandomElements<T>(int size, ICollection<T> items)
        {
            return GetRandomElements(items).Take(size).ToList();
        }

        /// <summary>
        /// Gets the sequence of the random elements.
        /// </summary>
        /// <typeparam name="T">The type of elements of the collection.</typeparam>
        /// <param name="items">The source items.</param>
        /// <returns>The sequence of the random elements of the collection.</returns>
        public static IEnumerable<T> GetRandomElements<T>(ICollection<T> items)
        {
            if (items == null || !items.Any())
            {
                throw new ArgumentException("There are no elements in the input collection");
            }

            var list = items as IList<T> ?? items.ToArray();
            while (true)
            {
                yield return list[Random.Next(0, list.Count)];
            }
        }

        /// <summary>
        /// Gets the random IP address.
        /// </summary>
        /// <returns>The random IP address.</returns>
        // ReSharper disable once InconsistentNaming
        public static IPAddress GetRandomIPAddress()
        {
            var address = new byte[4];
            Random.NextBytes(address);

            return new IPAddress(address);
        }

        /// <summary>
        /// Gets the random domain name in zone '.test'.
        /// </summary>
        /// <returns>The random domain name.</returns>
        public static string GetRandomDomain()
        {
            return GetRandomDomain("test");
        }

        /// <summary>
        /// Gets the random domain name in zone '.test'.
        /// </summary>
        /// <param name="count">The count of domains.</param>
        /// <returns>The random domain name.</returns>
        public static IEnumerable<string> GetRandomDomains(int count)
        {
            return Enumerable.Range(0, count).Select(_ => GetRandomDomain());
        }

        /// <summary>
        /// Gets the random domain in the specified zone.
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <returns>The random domain name.</returns>
        public static string GetRandomDomain(string zone)
        {
            return string.Format("{0}.{1}", GetRandomString(7), zone);
        }

        /// <summary>
        /// Gets the random domain in the specified zone.
        /// </summary>
        /// <param name="count">The count of domains.</param>
        /// <param name="zone">The zone.</param>
        /// <returns>The random domain name.</returns>
        public static IEnumerable<string> GetRandomDomains(int count, string zone)
        {
            return Enumerable.Range(0, count).Select(_ => GetRandomDomain(zone));
        }

        /// <summary>
        /// Gets the random email address in the random domain.
        /// </summary>
        /// <returns>The random email address.</returns>
        public static string GetRandomEmailAddress()
        {
            return GetRandomEmailAddress(GetRandomDomain());
        }

        /// <summary>
        /// Gets the random email addresses in the random domain.
        /// </summary>
        /// <param name="count">The count of addresses.</param>
        /// <returns>The random email addresses sequence.</returns>
        public static IEnumerable<string> GetRandomEmailAddresses(int count)
        {
            return Enumerable.Range(0, count).Select(_ => GetRandomEmailAddress());
        }

        /// <summary>
        /// Gets the random email.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>The random email address.</returns>
        public static string GetRandomEmailAddress(string domain)
        {
            return string.Format("{0}@{1}", GetRandomString(8), domain);
        }

        /// <summary>
        /// Gets the random email addresses in the random domain.
        /// </summary>
        /// <param name="count">The count of addresses.</param>
        /// <param name="domain">The domain.</param>
        /// <returns>The random email addresses sequence.</returns>
        public static IEnumerable<string> GetRandomEmailAddresses(int count, string domain)
        {
            return Enumerable.Range(0, count).Select(_ => GetRandomEmailAddress(domain));
        }
    }
}