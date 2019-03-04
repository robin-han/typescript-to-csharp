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
    public class Converter
    {
        protected ArgumentSyntax[] ToArgumentList(List<Node> argNodes)
        {
            List<ArgumentSyntax> csArgumetns = new List<ArgumentSyntax>();
            foreach (Node node in argNodes)
            {
                csArgumetns.Add(SyntaxFactory.Argument(node.ToCsNode<ExpressionSyntax>()));
            }

            return csArgumetns.ToArray();
        }

        protected string CommentText(string text)
        {
            return "AAA___" + text + "___AAA";
        }

    }
}