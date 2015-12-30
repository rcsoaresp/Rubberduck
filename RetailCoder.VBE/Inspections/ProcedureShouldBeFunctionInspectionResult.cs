﻿using System;
using System.Collections.Generic;
using System.Linq;
using Rubberduck.Parsing;
using Rubberduck.Parsing.Grammar;
using Rubberduck.Parsing.VBA;
using Rubberduck.VBEditor;

namespace Rubberduck.Inspections
{
    public class ProcedureShouldBeFunctionInspectionResult : CodeInspectionResultBase
    {
       private readonly IEnumerable<CodeInspectionQuickFix> _quickFixes;

       public ProcedureShouldBeFunctionInspectionResult(IInspection inspection, RubberduckParserState state, QualifiedContext<VBAParser.ArgListContext> argListQualifiedContext, QualifiedContext<VBAParser.SubStmtContext> subStmtQualifiedContext, QualifiedContext<VBAParser.ArgContext> argQualifiedContext)
           : base(inspection,
                string.Format(inspection.Description, subStmtQualifiedContext.Context.ambiguousIdentifier().GetText()),
                subStmtQualifiedContext.ModuleName,
                subStmtQualifiedContext.Context.ambiguousIdentifier())
        {
            _quickFixes = new[]
            {
                new ChangeProcedureToFunction(state, argListQualifiedContext, subStmtQualifiedContext, argQualifiedContext, QualifiedSelection), 
            };
        }

        public override IEnumerable<CodeInspectionQuickFix> QuickFixes { get { return _quickFixes; } }
    }

    public class ChangeProcedureToFunction : CodeInspectionQuickFix
    {
        private readonly RubberduckParserState _state;
        private readonly QualifiedContext<VBAParser.ArgListContext> _argListQualifiedContext;
        private readonly QualifiedContext<VBAParser.SubStmtContext> _subStmtQualifiedContext;
        private readonly QualifiedContext<VBAParser.ArgContext> _argQualifiedContext;

        public ChangeProcedureToFunction(RubberduckParserState state,
                                         QualifiedContext<VBAParser.ArgListContext> argListQualifiedContext,
                                         QualifiedContext<VBAParser.SubStmtContext> subStmtQualifiedContext,
                                         QualifiedContext<VBAParser.ArgContext> argQualifiedContext,
                                         QualifiedSelection selection)
            : base(subStmtQualifiedContext.Context, selection, InspectionsUI.ProcedureShouldBeFunctionInspectionQuickFix)
        {
            _state = state;
            _argListQualifiedContext = argListQualifiedContext;
            _subStmtQualifiedContext = subStmtQualifiedContext;
            _argQualifiedContext = argQualifiedContext;
        }

        public override void Fix()
        {
            UpdateSignature();
            UpdateCalls();
        }

        private void UpdateSignature()
        {
            var argListText = _argListQualifiedContext.Context.GetText();
            var subStmtText = _subStmtQualifiedContext.Context.GetText();
            var argText = _argQualifiedContext.Context.GetText();

            var newFunctionWithoutReturn = subStmtText.Insert(subStmtText.IndexOf(argListText, StringComparison.Ordinal) + argListText.Length,
                                                              _argQualifiedContext.Context.asTypeClause().GetText())
                                                      .Replace("Sub", "Function")
                                                      .Replace(argText,
                                                               argText.Contains("ByRef ")
                                                                 ? argText.Replace("ByRef ", "ByVal ")
                                                                 : "ByVal " + argText);

            var newfunctionWithReturn = newFunctionWithoutReturn
                .Insert(newFunctionWithoutReturn.LastIndexOf(Environment.NewLine, StringComparison.Ordinal),
                    "    " + _subStmtQualifiedContext.Context.ambiguousIdentifier().GetText() + " = " +
                    _argQualifiedContext.Context.ambiguousIdentifier().GetText());

            var rewriter = _state.GetRewriter(_subStmtQualifiedContext.ModuleName.Component);
            rewriter.Replace(_subStmtQualifiedContext.Context.Start, newfunctionWithReturn);

            var module = _argListQualifiedContext.ModuleName.Component.CodeModule;
            module.DeleteLines(_subStmtQualifiedContext.Context.Start.Line,
                _subStmtQualifiedContext.Context.Stop.Line - _subStmtQualifiedContext.Context.Start.Line + 1);
            module.InsertLines(_subStmtQualifiedContext.Context.Start.Line, newfunctionWithReturn);
        }

        private void UpdateCalls()
        {
            var procedureName = _subStmtQualifiedContext.Context.ambiguousIdentifier().GetText();

            var procedure =
                _state.AllDeclarations.SingleOrDefault(d =>
                        !d.IsBuiltIn &&
                        d.IdentifierName == procedureName &&
                        d.Context is VBAParser.SubStmtContext &&
                        d.ComponentName == _subStmtQualifiedContext.ModuleName.ComponentName &&
                        d.Project == _subStmtQualifiedContext.ModuleName.Project);

            if (procedure == null) { return; }

            foreach (var reference in procedure.References.OrderByDescending(o => o.Selection.StartLine).ThenByDescending(d => d.Selection.StartColumn))
            {
                var module = reference.QualifiedModuleName.Component.CodeModule;

                var referenceParent = reference.Context.Parent as VBAParser.ICS_B_ProcedureCallContext;
                if (referenceParent == null) { continue; }
                
                var referenceText = reference.Context.Parent.GetText();
                var newCall = _argQualifiedContext.Context.ambiguousIdentifier().GetText() + 
                              " = " + _subStmtQualifiedContext.Context.ambiguousIdentifier().GetText() +
                              "(" + referenceParent.argsCall().GetText() + ")";

                var oldLines = module.Lines[reference.Selection.StartLine, reference.Selection.LineCount];

                var newText = oldLines.Remove(reference.Selection.StartColumn - 1, referenceText.Length)
                    .Insert(reference.Selection.StartColumn - 1, newCall);

                module.DeleteLines(reference.Selection.StartLine, reference.Selection.LineCount);
                module.InsertLines(reference.Selection.StartLine, newText);
            }
        }
    }
}