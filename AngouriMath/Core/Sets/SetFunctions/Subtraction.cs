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

namespace AngouriMath.Core
{
    partial record SetNode
    {
        partial record Complement
        {
            public override SetNode Eval()
            {
                if (!(A is Set a && B is Set b))
                    return A - B;
                var (goodAPieces, badAPieces) = GatherEvaluablePieces(a);
                var (goodBPieces, badBPieces) = GatherEvaluablePieces(b);
                var newGoodPieces = new List<Piece>();
                newGoodPieces.AddRange(goodAPieces);
                foreach (var goodB in goodBPieces)
                {
                    var newNewGoodPieces = new List<Piece>();
                    foreach (var newGoodPiece in newGoodPieces)
                        newNewGoodPieces.AddRange(PieceFunctions.Subtract(newGoodPiece, goodB));
                    newGoodPieces = newNewGoodPieces;
                }

                newGoodPieces.AddRange(badAPieces);
                var newSet = new Set { Pieces = newGoodPieces };
                if (badBPieces.Count == 0)
                    return newSet;
                else
                    return new Complement(newSet, new Set { Pieces = badBPieces });
            }
        }
    }
}