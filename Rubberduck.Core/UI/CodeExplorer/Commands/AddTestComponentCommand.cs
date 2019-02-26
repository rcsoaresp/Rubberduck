﻿using System;
using System.Linq;
using System.Runtime.InteropServices;
using Rubberduck.Navigation.CodeExplorer;
using Rubberduck.Parsing.VBA;
using Rubberduck.UI.UnitTesting.ComCommands;
using Rubberduck.UnitTesting.CodeGeneration;
using Rubberduck.VBEditor.Events;
using Rubberduck.VBEditor.SafeComWrappers.Abstract;

namespace Rubberduck.UI.CodeExplorer.Commands
{
    public class AddTestComponentCommand : AddTestModuleCommand
    {
        private static readonly Type[] ApplicableNodes =
        {
            typeof(CodeExplorerCustomFolderViewModel),
            typeof(CodeExplorerProjectViewModel),
            typeof(CodeExplorerComponentViewModel),
            typeof(CodeExplorerMemberViewModel)
        };

        public AddTestComponentCommand(IVBE vbe, RubberduckParserState state, ITestCodeGenerator codeGenerator, IVBEEvents vbeEvents) 
            : base(vbe, state, codeGenerator, vbeEvents) { }

        protected override bool EvaluateCanExecute(object parameter)
        {
            if (parameter == null || 
                !ApplicableNodes.Contains(parameter.GetType()) ||
                !(parameter is CodeExplorerItemViewModel node))
            {
                return false;
            }

            try
            {
                return node.Declaration?.Project != null || Vbe.ProjectsCount != 1;
            }
            catch (COMException)
            {
                return false;
            }
        }
    }
}
