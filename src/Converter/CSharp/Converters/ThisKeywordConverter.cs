using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using GrapeCity.CodeAnalysis.TypeScript.Syntax;

namespace GrapeCity.CodeAnalysis.TypeScript.Converter.CSharp
{
    public class ThisKeywordConverter : Converter
    {
        public CSharpSyntaxNode Convert(ThisKeyword node)
        {
            return SyntaxFactory.ThisExpression(SyntaxFactory.Token(SyntaxKind.ThisKeyword));
        }
    }
}
