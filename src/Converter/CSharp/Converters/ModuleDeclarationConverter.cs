using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypeScript.Syntax;

namespace TypeScript.Converter.CSharp
{
    public class ModuleDeclarationConverter : Converter
    {
        public CSharpSyntaxNode Convert(ModuleDeclaration module)
        {
            string ns = this.GetNamespace(module);
            if (this.Context.Config.NamespaceMappings.ContainsKey(ns))
            {
                ns = this.Context.Config.NamespaceMappings[ns];
            }
            else if (!string.IsNullOrEmpty(this.Context.Config.Namespace))
            {
                ns = this.Context.Config.Namespace;
            }

            NamespaceDeclarationSyntax nsSyntax = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(ns));
            ModuleBlock mb = module.GetModuleBlock();
            if (mb != null)
            {
                nsSyntax = nsSyntax
                    .AddUsings(mb.TypeAliases.ToCsNodes<UsingDirectiveSyntax>())
                    .WithMembers(mb.ToCsNode<SyntaxList<MemberDeclarationSyntax>>());
            }
            return nsSyntax;
        }

        private string GetNamespace(ModuleDeclaration module)
        {
            List<string> parts = new List<string>();
            ModuleDeclaration md = module;
            while (md != null)
            {
                parts.Add(md.Name.Text);
                md = md.Body as ModuleDeclaration;
            }
            return string.Join('.', parts);
        }
    }
}

