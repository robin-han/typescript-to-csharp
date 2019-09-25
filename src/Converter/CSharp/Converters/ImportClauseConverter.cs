﻿using TypeScript.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypeScript.Converter.CSharp
{
    public class ImportClauseConverter : Converter
    {
        public SyntaxList<UsingDirectiveSyntax> Convert(ImportClause node)
        {
            SyntaxList<UsingDirectiveSyntax> usings = new SyntaxList<UsingDirectiveSyntax>();
            if (node.Name != null)
            {
                ImportDeclaration import = node.Ancestor(NodeKind.ImportDeclaration) as ImportDeclaration;
                Syntax.Document fromDoc = node.Project.GetDocumentByPath(import.ModulePath);
                UsingDirectiveSyntax usignSyntax = SyntaxFactory.UsingDirective(
                    SyntaxFactory.NameEquals(node.Name.Text),
                    SyntaxFactory.ParseName(fromDoc.GetPackageName() + "." + fromDoc.GetExportDefaultName()));
                usings = usings.Add(usignSyntax);
            }

            if (node.NamedBindings != null)
            {
                switch (node.NamedBindings.Kind)
                {
                    case NodeKind.NamespaceImport:
                        usings = usings.Add(node.NamedBindings.ToCsNode<UsingDirectiveSyntax>());
                        break;

                    case NodeKind.NamedImports:
                        usings = usings.AddRange(node.NamedBindings.ToCsNode<IEnumerable<UsingDirectiveSyntax>>());
                        break;

                    default:
                        break;
                }
            }

            return usings;
        }
    }
}