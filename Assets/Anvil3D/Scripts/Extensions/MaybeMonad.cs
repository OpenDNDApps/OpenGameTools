using System;

/* *****************************************************************************
 * File:    MaybeMonad.cs
 * Author:  Philip Pierce - Thursday, September 18, 2014
 * Description:
 *  Allows checking of null values on monads or nested chains of properties/functions
 *  
 * History:
 *  Thursday, September 18, 2014 - Created
 * ****************************************************************************/

namespace Anvil3D
{
	/// <summary>
	/// Allows checking of null values on monads or nested chains of properties/functions
	/// </summary>
	/// <remarks>https://www.dotnetcurry.com/patterns-practices/1510/maybe-monad-csharp</remarks>
	public static class MaybeMonad
	{
		/// <summary>
		/// Checks if an object has a value
		/// </summary>
		/// <typeparam name="TInput">object type to check</typeparam>
		/// <typeparam name="TResult">resulting object type</typeparam>
		/// <param name="o">the object to check for a value</param>
		/// <param name="evaluator">function for evaluating the object</param>
		/// <example>
		/// string postCode = this.With(x => person)
		///     .With(x => x.Address)
		///     .With(x => x.PostCode);
		/// </example>
		public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
			where TResult : class
			where TInput : class
		{
			return (o == null) ?
				null :
				evaluator(o);
		}

		/// <summary>
		/// This method will return the ‘current’ value just like Where() does, but in 
		/// case null was passed, it will return a different value that we supply.
		/// </summary>
		/// <typeparam name="TInput">input data type</typeparam>
		/// <typeparam name="TResult">output data type</typeparam>
		/// <param name="o">the object to check</param>
		/// <param name="evaluator">evaluation function</param>
		/// <param name="failureValue">value to return if object is null</param>
		/// <example>
		/// string postCode = this.With(x => person).With(x => x.Address)
		///           .Return(x => x.PostCode, string.Empty);
		/// </example>
		public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue)
			where TInput : class
		{
			return (o == null) ?
				failureValue :
				evaluator(o);
		}

		/// <summary>
		/// This method will return the ‘current’ value just like Where() does, but in 
		/// case null was passed, it will return a different value that we supply.
		/// </summary>
		/// <typeparam name="TInput">input data type</typeparam>
		/// <typeparam name="TResult">output data type</typeparam>
		/// <param name="o">the object to check</param>
		/// <param name="evaluator">evaluation function</param>
		/// <param name="failureValue">value to return if object is null</param>
		/// <example>
		/// string postCode = this.With(x => person).With(x => x.Address)
		///           .Return(x => x.PostCode, string.Empty);
		/// </example>
		public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, Func<TInput, TResult> failureValue)
			where TInput : class
		{
			return (o == null) ?
				failureValue(o) :
				evaluator(o);
		}

		/// <summary>
		/// Compares <paramref name="o"/> with the evaluator function
		/// </summary>
		/// <typeparam name="TInput">input type</typeparam>
		/// <param name="o">object to check</param>
		/// <param name="evaluator">if based function returning result of <paramref name="o"/> comparison</param>
		public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator)
			where TInput : class
		{
			if (o == null)
				return null;
			return evaluator(o) ? o : null;
		}

		/// <summary>
		/// Works the same as an If Not (returns null if evaluator is true, otherwise returns <paramref name="o"/>)
		/// </summary>
		/// <typeparam name="TInput">input type</typeparam>
		/// <param name="o">object to compare</param>
		/// <param name="evaluator">if not function for comparison</param>
		public static TInput Unless<TInput>(this TInput o, Func<TInput, bool> evaluator)
			where TInput : class
		{
			if (o == null)
				return null;
			return evaluator(o) ? null : o;
		}

		/// <summary>
		/// Allows you call a delegate from within inline LINQ
		/// </summary>
		/// <typeparam name="TInput">input data type</typeparam>
		/// <param name="o">object to pass to the delegate</param>
		/// <param name="action">the delegate to process</param>
		/// <example>
		/// string postCode = this.With(x => person)
		///    .If(x => HasMedicalRecord(x))
		///     .With(x => x.Address)
		///     .Do(x => CheckAddress(x))
		///     .With(x => x.PostCode)
		///     .Return(x => x.ToString(), "UNKNOWN");
		/// </example>
		public static TInput Do<TInput>(this TInput o, Action<TInput> action)
			where TInput : class
		{
			if (o == null)
				return null;
			action(o);
			return o;
		}

		/// <summary>
		/// if the object this method is called on is not null, runs the given function and returns the value.
		/// if the object is null, returns default(TResult)
		/// </summary>
		/// <remarks>http://extensionmethod.net/csharp/object/ifnotnull-t-tresult</remarks>
		public static TResult IfNotNull<T, TResult>(this T target, Func<T, TResult> getValue)
		{
			return target != null ? getValue(target) : default(TResult);
		}
	}
}