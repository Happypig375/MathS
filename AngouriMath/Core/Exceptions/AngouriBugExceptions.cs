
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

using System;

namespace AngouriMath.Core.Exceptions
{
    /// <summary>
    /// If one was thrown, the exception is probably not foreseen by AM. Repost it is an issue
    /// </summary>
    public class AngouriBugException : Exception
    {
        public AngouriBugException(string msg) : base(msg) { }
    }
    public class UnknownEntityException : AngouriBugException
    {
        public UnknownEntityException() : base("Unknown entity!") { }
    }
    public class UnknownOperatorException : AngouriBugException
    {
        public UnknownOperatorException() : base("Unknown operator!") { }
    }
    public class UnknownFunctionException : AngouriBugException
    {
        public UnknownFunctionException() : base("Unknown function!") { }
    }
    public class UnknownSetException : AngouriBugException
    {
        public UnknownSetException() : base("Unknown set!") { }
    }
    public class MathSException : AngouriBugException { public MathSException(string message) : base(message) { } }
    public class SolvingException : MathSException { public SolvingException(string message) : base(message) { } }
}