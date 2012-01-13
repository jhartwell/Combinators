using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;


namespace Jettsonator.Combinators
{
    public static class CombinatorExtensions
    {

        /// <summary>
        /// K Combinator function
        /// K x y -> x
        /// Enumerable extension method only
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TMe K<TMe, TInput>(this TMe me, Action<TInput> action) where TMe : IEnumerable
        {
            foreach (TInput item in me)
            {
                action(item);
            }
            return me;
        }

        /// <summary>
        /// K Combinator function
        /// K x y -> x
        /// Non-enumerable extension method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T k<T>(this T me, Action<T> action)
        {
            action(me);
            return me;
        }

        /// <summary>
        /// C Combinator
        /// C f x y -> f(y x)
        /// IEnumerable version only
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me"></param>
        /// <param name="f"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static TMe C<TMe, TInput>(this TMe me, Func<TInput, TInput, TInput> f, TInput y) where TMe : IEnumerable
        {
            List<TInput> results = new List<TInput>();

            foreach (TInput item in me)
            {
                results.Add(f(y, item));
            }

            return (TMe)results.AsEnumerable();
        }

        /// <summary>
        /// C Combinator
        /// C f xy -> f(x y)
        /// Non-enumerable version 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me"></param>
        /// <param name="f"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static T c<T>(this T me, Func<T, T, T> f, T y)
        {
            return f(y, me);
        }

        /// <summary>
        /// B Combinator
        /// B x y z -> x (y z)
        /// This is used only for Enumerable types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static TResult B<TResult,TInput>(this TResult me, Func<TInput, TInput> x, Func<TInput, TInput> y) where
            TResult : IEnumerable
        {
                List<TInput> results = new List<TInput>();
                foreach (var item in (IEnumerable)me)
                {
                    results.Add(x(y((TInput)item)));
                }
                return (TResult)results.AsEnumerable();
        }

        /// <summary>
        /// b Combinator
        /// B x y z -> x (y z)
        /// This is used for non-enumerable types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static T b<T>(this T me, Func<T, T> x, Func<T, T> y)
        {
            return x(y(me));
        }
    }
}
