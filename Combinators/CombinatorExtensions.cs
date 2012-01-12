using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Jettsonator.Combinators
{
    public static class CombinatorExtensions
    {
        /// <summary>
        /// Checks to see if the type passed in is IEnumerable
        /// Borrowed from http://stackoverflow.com/a/750057
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        private static bool IsEnumerable<T>(T item)
        {
            return (typeof(IEnumerable<int>).IsAssignableFrom(typeof(T)));
        }
        /*
         * K Combinator
         * K x y -> x
         */
        public static T K<T>(this T me, Action<object> action)
        {
            if (IsEnumerable<T>(me))
            {
                foreach (var item in (IEnumerable)me)
                {
                    action(item);
                }
            }
            else
            {
                action(me);
            }

            return me;
        }
        /*
         * C Combinator
         * C f x y -> f(y x)
         * For enumerable types only
         */
        public static T C<T>(this T me, Func<T, T,T> f, T y)
        {
            List<T> results = new List<T>();

            if (IsEnumerable<T>(me))
            {
                foreach (var item in (IEnumerable)me)
                {
                    results.Add(f((T)item, y));
                }
                return (T)results.AsEnumerable();
            }
            else
            {
                return f(me, y);
            }
        }

        /*
         * B Combinator
         * B x y z -> x (y z)
         */
        public static T B<T>(this T me, Func<T, T> x, Func<T, T> y)
        {
            if (IsEnumerable<T>(me))
            {
                List<T> results = new List<T>();
                foreach (var item in (IEnumerable)me)
                {
                    results.Add(x(y((T)item)));
                }
                return (T)results.AsEnumerable();
            }
            else
            {
                return x(y(me));
            }
        }
    }
}
