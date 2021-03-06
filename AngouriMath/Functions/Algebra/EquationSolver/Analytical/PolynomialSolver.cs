﻿
/* Copyright (c) 2019-2020 Angourisoft
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software
 * is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
 * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */


using System.Collections.Generic;
using System.Linq;
using AngouriMath.Convenience;
using AngouriMath.Core;
 using AngouriMath.Core.Numerix;
 using AngouriMath.Core.TreeAnalysis;

namespace AngouriMath.Core.TreeAnalysis
{
    internal static partial class TreeAnalyzer
    {
        /// <summary>
        /// That is realized SO badly...
        /// TODO
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal interface IPrimitive<T>
        {
            void Add(T a);
            void AddMp(T a, ComplexNumber b);
            void Assign(T val);
            T GetValue();
        }
        internal class PrimitiveDouble : IPrimitive<decimal>
        {
            private decimal value = 0;
            public void Add(decimal a) => value += a;
            public void AddMp(decimal a, ComplexNumber b) => Add(a * b.Real);
            public void Assign(decimal val) => value = val;
            public static implicit operator decimal(PrimitiveDouble obj) => obj.value;
            internal static IPrimitive<decimal> Create()
            {
                return new PrimitiveDouble();
            }
            public decimal GetValue() => value;
        }
        internal class PrimitiveInt : IPrimitive<long>
        {
            private long value = 0;
            public void Add(long a) => value += a;
            public void AddMp(long a, ComplexNumber b) => Add((a * b.Real).AsIntegerNumber());
            public void Assign(long val) => value = val;
            public static implicit operator long(PrimitiveInt obj) => obj.value;
            internal static IPrimitive<long> Create()
            {
                return new PrimitiveInt();
            }
            public long GetValue() => value;
        }
    }
}

namespace AngouriMath.Functions.Algebra.AnalyticalSolving
{
    /// <summary>
    /// Solves all forms of Polynomials that are trivially solved
    /// </summary>
    internal static class PolynomialSolver
    {
        /// <summary>
        /// Solves ax + b
        /// </summary>
        /// <param name="a">
        /// Coefficient of x
        /// </param>
        /// <param name="b">
        /// Free coefficient
        /// </param>
        /// <returns>
        /// Set of roots
        /// </returns>
        internal static Set SolveLinear(Entity a, Entity b)
        {
            // ax + b = 0
            // ax = -b
            // x = -b / a
            return new Set((-b / a).InnerSimplify());
        }

        /// <summary>
        /// solves ax2 + bx + c
        /// </summary>
        /// <param name="a">
        /// Coefficient of x^2
        /// </param>
        /// <param name="b">
        /// Coefficient of x
        /// </param>
        /// <param name="c">
        /// Free coefficient
        /// </param>
        /// <returns>
        /// Set of roots
        /// </returns>
        internal static Set SolveQuadratic(Entity a, Entity b, Entity c)
        {
            Set res;
            if (TreeAnalyzer.IsZero(c))
            {
                res = SolveLinear(a, b);
                res.Add(0);
                return res;
            }

            if (TreeAnalyzer.IsZero(a))
                return SolveLinear(b, c);

            res = new Set();
            var D = MathS.Sqr(b) - 4 * a * c;
            res.Add(((-b - MathS.Sqrt(D)) / (2 * a)).InnerSimplify());
            res.Add(((-b + MathS.Sqrt(D)) / (2 * a)).InnerSimplify());
            return res;
        }

        /// <summary>
        /// solves ax3 + bx2 + cx + d
        /// </summary>
        /// <param name="a">
        /// Coefficient of x^3
        /// </param>
        /// <param name="b">
        /// Coefficient of x^2
        /// </param>
        /// <param name="c">
        /// Coefficient of x
        /// </param>
        /// <param name="d">
        /// Free coefficient
        /// </param>
        /// <returns>
        /// Set of roots
        /// </returns>
        internal static Set SolveCubic(Entity a, Entity b, Entity c, Entity d)
        {
            // en: https://en.wikipedia.org/wiki/Cubic_equation
            // ru: https://ru.wikipedia.org/wiki/%D0%A4%D0%BE%D1%80%D0%BC%D1%83%D0%BB%D0%B0_%D0%9A%D0%B0%D1%80%D0%B4%D0%B0%D0%BD%D0%BE

            // TODO (to remove sympy code!)

            Set res;

            if (TreeAnalyzer.IsZero(d))
            {
                res = SolveQuadratic(a, b, c);
                res.Add(0);
                return res;
            }

            if (TreeAnalyzer.IsZero(a))
                return SolveQuadratic(b, c, d);

            res = new Set();

            var coeff = MathS.i * MathS.Sqrt(3) / 2;

            var u1 = new NumberEntity(1);
            var u2 = SySyn.Rational(-1, 2) + coeff;
            var u3 = SySyn.Rational(-1, 2) - coeff;
            var D0 = MathS.Sqr(b) - 3 * a * c;
            var D1 = (2 * MathS.Pow(b, 3) - 9 * a * b * c + 27 * MathS.Sqr(a) * d).InnerSimplify();
            var C = MathS.Pow((D1 + MathS.Sqrt(MathS.Sqr(D1) - 4 * MathS.Pow(D0, 3))) / 2, Number.CreateRational(1, 3));

            foreach (var uk in new List<Entity> {u1, u2, u3})
            {
                Entity r;
                if (Const.EvalIfCan(C) == 0 && Const.EvalIfCan(D0) == 0)
                    r = -(b + uk * C) / 3 / a;
                else
                    r = -(b + uk * C + D0 / C / uk) / 3 / a;
                res.Add(r);
            }
            return res;
        }

        /// <summary>
        /// solves ax4 + bx3 + cx2 + dx + e
        /// </summary>
        /// <param name="a">
        /// Coefficient of x^4
        /// </param>
        /// <param name="b">
        /// Coefficient of x^3
        /// </param>
        /// <param name="c">
        /// Coefficient of x^2
        /// </param>
        /// <param name="d">
        /// Coefficient of x
        /// </param>
        /// <param name="e">
        /// Free coefficient
        /// </param>
        /// <returns>
        /// Set of roots
        /// </returns>
        internal static Set SolveQuartic(Entity a, Entity b, Entity c, Entity d, Entity e)
        {
            // en: https://en.wikipedia.org/wiki/Quartic_function
            // ru: https://ru.wikipedia.org/wiki/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4_%D0%A4%D0%B5%D1%80%D1%80%D0%B0%D1%80%D0%B8

            Set res;

            if (TreeAnalyzer.IsZero(e))
            {
                res = SolveCubic(a, b, c, d);
                res.Add(0);
                return res;
            }

            if (TreeAnalyzer.IsZero(a))
                return SolveCubic(b, c, d, e);

            
            res = new Set();

            var alpha = (-3 * MathS.Sqr(b) / (8 * MathS.Sqr(a)) + c / a)
                .InnerSimplify();
            var beta = (MathS.Pow(b, 3) / (8 * MathS.Pow(a, 3)) - (b * c) / (2 * MathS.Sqr(a)) + d / a)
                .InnerSimplify();
            var gamma = (-3 * MathS.Pow(b, 4) / (256 * MathS.Pow(a, 4)) + MathS.Sqr(b) * c / (16 * MathS.Pow(a, 3)) - (b * d) / (4 * MathS.Sqr(a)) + e / a)
                .InnerSimplify();

            if (Const.EvalIfCan(beta) == 0)
            {
                res.FastAddingMode = true;
                for (int s = -1; s <= 1; s += 2)
                for (int t = -1; t <= 1; t += 2)
                {
                    var x = -b / 4 * a + s * MathS.Sqrt((-alpha + t * MathS.Sqrt(MathS.Sqr(alpha) - 4 * gamma)) / 2);
                    res.Add(x);
                }
                res.FastAddingMode = false;
                return res;
            }


            var oneThird = Number.CreateRational(1, 3);
            var P = (-MathS.Sqr(alpha) / 12 - gamma)
                .InnerSimplify();
            var Q = (-MathS.Pow(alpha, 3) / 108 + alpha * gamma / 3 - MathS.Sqr(beta) / 8)
                .InnerSimplify();
            var R = -Q / 2 + MathS.Sqrt(MathS.Sqr(Q) / 4 + MathS.Pow(P, 3) / 27);
            var U = MathS.Pow(R, oneThird)
                .InnerSimplify();
            var y = (Number.CreateRational(-5, 6) * alpha + U + (Const.EvalIfCan(U) == 0 ? -MathS.Pow(Q, oneThird) : -P / (3 * U)))
                .InnerSimplify();
            var W = MathS.Sqrt(alpha + 2 * y)
                .InnerSimplify();
           
            // Now we need to permutate all four combinations
            res.FastAddingMode = true;  /* we are sure that there's no such root yet */
            for (int s = -1; s <= 1; s += 2)
                for (int t = -1; t <= 1; t += 2)
                {
                    var x = -b / (4 * a) + (s * W + t * MathS.Sqrt(-(3 * alpha + 2 * y + s * 2 * beta / W))) / 2;
                    res.Add(x);
                }
            res.FastAddingMode = false;
            return res;
        }

        /// <summary>
        /// So that the final list of powers contains power = 0 and all powers >= 0
        /// (e. g. if the dictionaty's keys are 3, 4, 6, the final answer will contain keys
        /// 0, 1, 3, if the dictionary's keys are -2, 0, 3, the final answer will contain keys
        /// 0, 2, 5)
        /// </summary>
        /// <param name="monomials">
        /// Dictionary to process. Key - power, value - coefficient of the corresponding term
        /// </param>
        /// <returns>
        /// Returns whether all initiall powers where > 0 (if so, x = 0 is a root)
        /// </returns>
        internal static bool ReduceCommonPower(ref Dictionary<long, Entity> monomials)
        {
            var commonPower = monomials.Keys.Min();
            if (commonPower == 0)
                return false;
            var newDict = new Dictionary<long, Entity>();
            foreach (var pair in monomials)
                newDict[pair.Key - commonPower] = pair.Value;
            monomials = newDict;
            return commonPower > 0;
        }

        /// <summary>
        /// Tries to solve as polynomial
        /// </summary>
        /// <param name="expr">
        /// Polynomial of an expression
        /// </param>
        /// <param name="subtree">
        /// The expression the polynomial of (e. g. cos(x)^2 + cos(x) + 1 is a polynomial of cos(x))
        /// </param>
        /// <returns>
        /// a finite Set if successful,
        /// null otherwise
        /// </returns>
        internal static Set SolveAsPolynomial(Entity expr, Entity subtree)
        {
            // Here we find all terms
            expr = expr.Expand(); // (x + 1) * x => x^2 + x
            List<Entity> children;
            Set res = new Set();
            if (expr.entType == Entity.EntType.OPERATOR && expr.Name == "sumf" || expr.Name == "minusf")
                children = TreeAnalyzer.LinearChildren(expr, "sumf", "minusf", Const.FuncIfSum);
            else
                children = new List<Entity> { expr };
            // Check if all are like {1} * x^n & gather information about them
            var monomialsByPower = GatherMonomialInformation<long>(children, subtree);

            if (monomialsByPower == null)
                return null; // meaning that the given equation is not polynomial

            Entity GetMonomialByPower(long power)
            {
                return monomialsByPower.ContainsKey(power) ? monomialsByPower[power].InnerSimplify() : 0;
            }
            if (ReduceCommonPower(ref monomialsByPower)) // x5 + x3 + x2 - common power is 2, one root is 0, then x3 + x + 1
                res.Add(0);
            var powers = new List<long>(monomialsByPower.Keys);
            var gcdPower = Utils.GCD(powers.ToArray());
            // // //



            // Change all replacements, x6 + x3 + 1 => x2 + x + 1
            if (gcdPower != 1)
            {
                for (int i = 0; i < powers.Count; i++)
                    powers[i] /= gcdPower;

                var newMonom = new Dictionary<long, Entity>();
                foreach (var pair in monomialsByPower)
                    newMonom[pair.Key / gcdPower] = pair.Value;
                monomialsByPower = newMonom;
            }
            // // //



            // if we had x^6 + x^3 + 1, we replace it with x^2 + x + 1 and find all cubic roots of the final answer
            Set FinalPostProcess(Set set)
            {
                if (gcdPower != 1)
                {
                    var newSet = new Set();
                    foreach (var root in set.FiniteSet())
                    foreach (var coef in Number.GetAllRoots(1, gcdPower).FiniteSet())
                        newSet.Add(coef * MathS.Pow(root, Number.Create(1.0) / gcdPower));
                    set = newSet;
                }
                return set;
            }

            if (powers.Count == 0)
                return null;
            powers.Sort();
            if (powers.Last() == 0)
                return FinalPostProcess(res);
            if (powers.Last() > 4 && powers.Count > 2)
                return null; // So far, we can't solve equations of powers more than 4
            if (powers.Count == 1)
            {
                res.Add(0);
                return FinalPostProcess(res);
            }
            else if (powers.Count == 2)
            {
                // Provided a x ^ n + b = 0
                // a = -b x ^ n
                // (- a / b) ^ (1 / n) = x
                // x ^ n = (-a / b)
                var value = (-1 * monomialsByPower[powers[0]] / monomialsByPower[powers[1]]).Simplify();
                res.AddRange(TreeAnalyzer.FindInvertExpression(MathS.Pow(subtree, powers[1]), value, subtree));
                return FinalPostProcess(res);
            }
            // By this moment we know for sure that expr's power is <= 4, that expr is not a monomial,
            // and that it consists of more than 2 monomials
            else if (powers.Last() == 2)
            {
                var a = GetMonomialByPower(2);
                var b = GetMonomialByPower(1);
                var c = GetMonomialByPower(0);

                res.AddRange(SolveQuadratic(a, b, c));
                return FinalPostProcess(res);
            }
            else if (powers.Last() == 3)
            {
                var a = GetMonomialByPower(3);
                var b = GetMonomialByPower(2);
                var c = GetMonomialByPower(1);
                var d = GetMonomialByPower(0);

                res.AddRange(SolveCubic(a, b, c, d));
                return FinalPostProcess(res);
            }
            else if (powers.Last() == 4)
            {
                var a = GetMonomialByPower(4);
                var b = GetMonomialByPower(3);
                var c = GetMonomialByPower(2);
                var d = GetMonomialByPower(1);
                var e = GetMonomialByPower(0);

                res.AddRange(SolveQuartic(a, b, c, d, e));
                return FinalPostProcess(res);
            }
            return null;
        }
    
        /// <summary>
        /// Finds all terms of a polynomial
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="terms"></param>
        /// <param name="subtree"></param>
        /// <returns></returns>
        internal static Dictionary<T, Entity> GatherMonomialInformation<T>(List<Entity> terms, Entity subtree)
        {
            var monomialsByPower = new Dictionary<T, Entity>();
            // here we fill the dictionary with information about monomials' coefficiants
            foreach (var child in terms)
            {
                // TODO
                Entity free;
                object pow;
                if (typeof(T) == typeof(decimal))
                    pow = new TreeAnalyzer.PrimitiveDouble();
                else
                    pow = new TreeAnalyzer.PrimitiveInt();
                
                TreeAnalyzer.IPrimitive<T> q = pow as TreeAnalyzer.IPrimitive<T>;
                ParseMonomial<T>(subtree, child, out free, ref q);
                if (free == null)
                    return null;
                if (!monomialsByPower.ContainsKey(q.GetValue()))
                    monomialsByPower[q.GetValue()] = 0;
                monomialsByPower[q.GetValue()] += free;
            }
            // TODO: do we need to simplify all values of monomialsByPower?
            return monomialsByPower;
        }


        internal static void ParseMonomial<T>(Entity aVar, Entity expr, out Entity freeMono, ref TreeAnalyzer.IPrimitive<T> power)
        {
            if (expr.FindSubtree(aVar) == null)
            {
                freeMono = expr;
                return;
            }

            freeMono = 1; // a * b

            bool allowFloat = typeof(T) == typeof(decimal);
            foreach (var mp in TreeAnalyzer.LinearChildren(expr, "mulf", "divf", Const.FuncIfMul))
                if (mp.FindSubtree(aVar) == null)
                {
                    freeMono *= mp;
                }
                else if (mp.entType == Entity.EntType.OPERATOR &&
                    mp.Name == "powf")
                {
                    var pow_num = MathS.CanBeEvaluated(mp.Children[1]) ? mp.Children[1].Eval() : mp.Children[1];

                     // x ^ a is bad
                    if (pow_num.entType != Entity.EntType.NUMBER)
                    {
                        freeMono = null;
                        return;
                    }

                    // x ^ 0.3 is bad
                    if (!allowFloat && !pow_num.Eval().IsInteger())
                    {
                        freeMono = null;
                        return;
                    }

                    if (mp == aVar)
                    {
                        if (allowFloat)
                            (power as TreeAnalyzer.PrimitiveDouble).Add(pow_num.GetValue().Real);
                        else
                            (power as TreeAnalyzer.PrimitiveInt).Add(pow_num.GetValue().Real.AsInt());
                    }
                    else
                    {
                        if (!MathS.CanBeEvaluated(mp.Children[1]))
                        {
                            freeMono = null;
                            return;
                        }
                        Entity tmpFree;
                        // TODO
                        object pow;
                        if (typeof(T) == typeof(decimal))
                            pow = new TreeAnalyzer.PrimitiveDouble();
                        else
                            pow = new TreeAnalyzer.PrimitiveInt();
                        TreeAnalyzer.IPrimitive<T> q = pow as TreeAnalyzer.IPrimitive<T>;
                        ParseMonomial<T>(aVar, mp.Children[0], out tmpFree, ref q);
                        if (tmpFree == null)
                        {
                            freeMono = null;
                            return;
                        }
                        else
                        {
                            // Can we eval it right here?
                            mp.Children[1] = mp.Children[1].Eval();
                            freeMono *= MathS.Pow(tmpFree, mp.Children[1]);
                            power.AddMp(q.GetValue(), mp.Children[1].GetValue());
                        }
                    }
                }
                else if (mp == aVar)
                {
                    if (allowFloat)
                        (power as TreeAnalyzer.PrimitiveDouble).Add(1);
                    else
                        (power as TreeAnalyzer.PrimitiveInt).Add(1);
                }
                else
                {
                    // a ^ x, (a + x) etc. are bad
                    if (mp.FindSubtree(aVar) != null)
                    {
                        freeMono = null;
                        return;
                    }
                    freeMono *= mp;
                }
            // TODO: do we need to simplify freeMono?
        }
    }
}
