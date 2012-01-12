using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jettsonator.Combinators
{
    public static class CombinatorExtensions
    {
        /*
         * K Combinator
         * K x y -> x
         */
        public static T K<T>(this T me, Action<T> action)
        {
            if (me is IEnumerable<T>)
            {
                foreach (var it in (IEnumerable<T>)me)
                {
                    action(me);
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
         */
        public static T C<T>(this T me, Func<T, T,T> f, T y)
        {
            if (me is IEnumerable<T>)
            {
                List<T> results = new List<T>();
                foreach (var it in (IEnumerable<T>)me)
                {
                    results.Add(f(y, it));
                }
                return (T)results.AsEnumerable<T>();
            }
            else
            {
                return f(y, me);
            }
        }

        /*
         * B Combinator
         * B x y z -> x (y z)
         */
        public static T B<T>(this T me, Func<T, T> x, Func<T, T> y)
        {

            if (me is IEnumerable<T>)
            {
                List<T> results = new List<T>();
                foreach (var it in (IEnumerable<T>)me)
                {
                    results.Add(x(y(it)));
                }
                return (T)results.AsEnumerable<T>();
            }
            else
            {
                return x(y(me));
            }

        }
    }
}
