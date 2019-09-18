using System;
using System.Collections;

namespace RateMyP.Exceptions
    {
    /// <summary>Helper class for validating method arguments</summary>
    public static class ArgumentChecker
        {
        /// <summary>Throws an exception if the object to check is null</summary>
        /// <param name="value">object to check for null</param>
        /// <param name="parameterName">optional, name of the parameter, to use in exception</param>
        /// <exception cref="ArgumentNullException">if <paramref name="value" /> is null.</exception>
        public static void ThrowIfNull(object value, string parameterName = null)
            {
            if (value != null)
                return;
            if (parameterName == null)
                throw new ArgumentNullException();
            throw new ArgumentNullException(parameterName);
            }

        /// <summary>
        /// Throws an exception if the string to check empty (will not throw if the string is null)
        /// </summary>
        /// <param name="value">string to check</param>
        /// <param name="parameterName">optional, name of the parameter, to use in exception</param>
        /// <exception cref="ArgumentException">if <paramref name="value" /> is empty.</exception>
        public static void ThrowIfEmpty(string value, string parameterName = null)
            {
            if (value == null || value.Length != 0)
                return;
            if (parameterName == null)
                throw new ArgumentException("Argument must not be empty.");
            throw new ArgumentException("Argument must not be empty.", parameterName);
            }

        /// <summary>
        /// Throws an exception if the string to check is null or empty
        /// </summary>
        /// <param name="value">string to check</param>
        /// <param name="parameterName">optional, name of the parameter, to use in exception</param>
        /// <exception cref="ArgumentNullException">if <paramref name="value" /> is null.</exception>
        /// <exception cref="ArgumentException">if <paramref name="value" /> is empty.</exception>
        public static void ThrowIfNullOrEmpty(string value, string parameterName = null)
            {
            ThrowIfNull(value, parameterName);
            ThrowIfEmpty(value, parameterName);
            }

        /// <summary>
        /// Throws an exception if the string to check is null or contains only whitespace
        /// </summary>
        /// <param name="value">string to check</param>
        /// <param name="parameterName">optional, name of the parameter, to use in exception</param>
        /// <exception cref="ArgumentNullException">if <paramref name="value" /> is null.</exception>
        /// <exception cref="ArgumentException">if <paramref name="value" /> is empty or only white spaces.</exception>
        public static void ThrowIfNullOrWhiteSpace(string value, string parameterName = null)
            {
            ThrowIfNull(value, parameterName);
            if (!string.IsNullOrWhiteSpace(value))
                return;
            if (parameterName == null)
                throw new ArgumentException("Argument must not be empty or only be whitespace.");
            throw new ArgumentException("Argument must not be empty or only be whitespace.", parameterName);
            }

        /// <summary>
        /// Throws an exception if the collection to check is null or empty
        /// </summary>
        /// <param name="value">collection to check</param>
        /// <param name="parameterName">optional, name of the parameter, to use in exception</param>
        /// <exception cref="ArgumentNullException">if <paramref name="value" /> is null.</exception>
        /// <exception cref="ArgumentException">if <paramref name="value" /> is empty.</exception>
        public static void ThrowIfNullOrEmpty(ICollection value, string parameterName = null)
            {
            ThrowIfNull(value, parameterName);
            if (value.Count != 0)
                return;
            if (parameterName == null)
                throw new ArgumentException("Argument must not be empty.");
            throw new ArgumentException("Argument must not be empty.", parameterName);
            }
        }
    }
