﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rubberduck.Navigation.CodeExplorer;
using Rubberduck.Parsing.Symbols;

namespace RubberduckTests.CodeExplorer
{
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class CodeExplorerMemberViewModelTests
    {
        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestSubWithLineLabelName)]
        [TestCase(CodeExplorerTestCode.TestSubWithUnresolvedMemberName)]
        [TestCase(CodeExplorerTestCode.TestFunctionName)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyGet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyLet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertySet)]
        [TestCase(CodeExplorerTestCode.TestTypeName)]
        [TestCase(CodeExplorerTestCode.TestEnumName)]
        [TestCase(CodeExplorerTestCode.TestConstantName)]
        [TestCase(CodeExplorerTestCode.TestFieldName)]
        [TestCase(CodeExplorerTestCode.TestLibraryFunctionName)]
        [TestCase(CodeExplorerTestCode.TestLibraryProcedureName)]
        [TestCase(CodeExplorerTestCode.TestEventName)]
        public void Constructor_SetsDeclaration(string name, DeclarationType type = DeclarationType.Member)
        {
            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out var memberDeclaration, type);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, declarations);

            Assert.AreSame(memberDeclaration, member.Declaration);
        }

        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestSubWithLineLabelName)]
        [TestCase(CodeExplorerTestCode.TestSubWithUnresolvedMemberName)]
        [TestCase(CodeExplorerTestCode.TestFunctionName)]
        [TestCase(CodeExplorerTestCode.TestTypeName)]
        [TestCase(CodeExplorerTestCode.TestEnumName)]
        [TestCase(CodeExplorerTestCode.TestConstantName)]
        [TestCase(CodeExplorerTestCode.TestFieldName)]
        [TestCase(CodeExplorerTestCode.TestLibraryFunctionName)]
        [TestCase(CodeExplorerTestCode.TestLibraryProcedureName)]
        [TestCase(CodeExplorerTestCode.TestEventName)]
        public void Constructor_SetsName_Member(string name)
        {
            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out var memberDeclaration);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, declarations);

            Assert.AreEqual(name, member.Name);
        }

        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyGet, "(Get)")]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyLet, "(Let)")]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertySet, "(Set)")]
        public void Constructor_SetsName_Property(string name, DeclarationType type, string suffix)
        {
            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out var memberDeclaration, type);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, declarations);

            Assert.AreEqual($"{name} {suffix}", member.Name);
        }

        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestSubWithLineLabelName)]
        [TestCase(CodeExplorerTestCode.TestSubWithUnresolvedMemberName)]
        [TestCase(CodeExplorerTestCode.TestFunctionName)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyGet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyLet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertySet)]
        [TestCase(CodeExplorerTestCode.TestTypeName)]
        [TestCase(CodeExplorerTestCode.TestEnumName)]
        [TestCase(CodeExplorerTestCode.TestConstantName)]
        [TestCase(CodeExplorerTestCode.TestFieldName)]
        [TestCase(CodeExplorerTestCode.TestLibraryFunctionName)]
        [TestCase(CodeExplorerTestCode.TestLibraryProcedureName)]
        [TestCase(CodeExplorerTestCode.TestEventName)]
        public void Constructor_NameWithSignatureIsSet(string name, DeclarationType type = DeclarationType.Member)
        {
            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out var memberDeclaration, type);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, declarations);

            Assert.IsFalse(string.IsNullOrEmpty(member.NameWithSignature));
        }

        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestSubWithLineLabelName)]
        [TestCase(CodeExplorerTestCode.TestSubWithUnresolvedMemberName)]
        [TestCase(CodeExplorerTestCode.TestFunctionName)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyGet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyLet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertySet)]
        [TestCase(CodeExplorerTestCode.TestTypeName)]
        [TestCase(CodeExplorerTestCode.TestEnumName)]
        [TestCase(CodeExplorerTestCode.TestConstantName)]
        [TestCase(CodeExplorerTestCode.TestFieldName)]
        [TestCase(CodeExplorerTestCode.TestLibraryFunctionName)]
        [TestCase(CodeExplorerTestCode.TestLibraryProcedureName)]
        [TestCase(CodeExplorerTestCode.TestEventName)]
        public void Constructor_ToolTipIsSet(string name, DeclarationType type = DeclarationType.Member)
        {
            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out var memberDeclaration, type);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, declarations);

            Assert.IsFalse(string.IsNullOrEmpty(member.ToolTip));
        }

        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestSubWithLineLabelName)]
        [TestCase(CodeExplorerTestCode.TestSubWithUnresolvedMemberName)]
        [TestCase(CodeExplorerTestCode.TestFunctionName)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyGet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyLet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertySet)]
        [TestCase(CodeExplorerTestCode.TestTypeName)]
        [TestCase(CodeExplorerTestCode.TestEnumName)]
        [TestCase(CodeExplorerTestCode.TestConstantName)]
        [TestCase(CodeExplorerTestCode.TestFieldName)]
        [TestCase(CodeExplorerTestCode.TestLibraryFunctionName)]
        [TestCase(CodeExplorerTestCode.TestLibraryProcedureName)]
        [TestCase(CodeExplorerTestCode.TestEventName)]
        public void Constructor_SetsIsExpandedFalse(string name, DeclarationType type = DeclarationType.Member)
        {
            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out var memberDeclaration, type);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, declarations);

            Assert.IsFalse(member.IsExpanded);
        }

        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerSortOrder.Undefined, typeof(CompareByName))]
        [TestCase(CodeExplorerSortOrder.Name, typeof(CompareByName))]
        [TestCase(CodeExplorerSortOrder.CodeLine, typeof(CompareByCodeLine))]
        [TestCase(CodeExplorerSortOrder.DeclarationType, typeof(CompareByName))]
        [TestCase(CodeExplorerSortOrder.DeclarationTypeThenName, typeof(CompareByDeclarationTypeAndName))]
        [TestCase(CodeExplorerSortOrder.DeclarationTypeThenCodeLine, typeof(CompareByDeclarationTypeAndCodeLine))]
        public void SortComparerIsCorrectSortOrderType(CodeExplorerSortOrder order, Type comparerType)
        {
            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(CodeExplorerTestCode.TestSubName, out var memberDeclaration);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, declarations);

            member.SortOrder = order;

            Assert.AreEqual(comparerType, member.SortComparer.GetType());
        }

        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestSubWithLineLabelName)]
        [TestCase(CodeExplorerTestCode.TestSubWithUnresolvedMemberName)]
        [TestCase(CodeExplorerTestCode.TestFunctionName)]
        [TestCase(CodeExplorerTestCode.TestTypeName)]
        [TestCase(CodeExplorerTestCode.TestEnumName)]
        [TestCase(CodeExplorerTestCode.TestConstantName)]
        [TestCase(CodeExplorerTestCode.TestFieldName)]
        [TestCase(CodeExplorerTestCode.TestLibraryFunctionName)]
        [TestCase(CodeExplorerTestCode.TestLibraryProcedureName)]
        [TestCase(CodeExplorerTestCode.TestEventName)]
        public void FilteredIsFalseForSubsetsOfName(string name)
        {
            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out var memberDeclaration);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, new List<Declaration> { memberDeclaration });

            for (var characters = 1; characters <= name.Length; characters++)
            {
                member.Filter = name.Substring(0, characters);
                Assert.IsFalse(member.Filtered);
            }

            for (var position = name.Length - 2; position > 0; position--)
            {
                member.Filter = name.Substring(position);
                Assert.IsFalse(member.Filtered);
            }
        }

        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestSubWithLineLabelName)]
        [TestCase(CodeExplorerTestCode.TestSubWithUnresolvedMemberName)]
        [TestCase(CodeExplorerTestCode.TestFunctionName)]
        [TestCase(CodeExplorerTestCode.TestTypeName)]
        [TestCase(CodeExplorerTestCode.TestEnumName)]
        [TestCase(CodeExplorerTestCode.TestConstantName)]
        [TestCase(CodeExplorerTestCode.TestFieldName)]
        [TestCase(CodeExplorerTestCode.TestLibraryFunctionName)]
        [TestCase(CodeExplorerTestCode.TestLibraryProcedureName)]
        [TestCase(CodeExplorerTestCode.TestEventName)]
        public void FilteredIsTrueForCharactersNotInName(string name)
        {
            const string testCharacters = "abcdefghijklmnopqrstuwxyz";

            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out var memberDeclaration);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, new List<Declaration> { memberDeclaration });

            var nonMatching = testCharacters.ToCharArray().Except(name.ToLowerInvariant().ToCharArray());

            foreach (var character in nonMatching.Select(letter => letter.ToString()))
            {
                member.Filter = character;
                Assert.IsTrue(member.Filtered);
            }
        }

        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestSubWithLineLabelName)]
        [TestCase(CodeExplorerTestCode.TestSubWithUnresolvedMemberName)]
        [TestCase(CodeExplorerTestCode.TestFunctionName)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyGet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyLet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertySet)]
        [TestCase(CodeExplorerTestCode.TestTypeName)]
        [TestCase(CodeExplorerTestCode.TestEnumName)]
        [TestCase(CodeExplorerTestCode.TestConstantName)]
        [TestCase(CodeExplorerTestCode.TestFieldName)]
        [TestCase(CodeExplorerTestCode.TestLibraryFunctionName)]
        [TestCase(CodeExplorerTestCode.TestLibraryProcedureName)]
        [TestCase(CodeExplorerTestCode.TestEventName)]
        public void Constructor_PlacesAllTrackedDeclarations(string name, DeclarationType type = DeclarationType.Member)
        {
            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out var memberDeclaration, type);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, declarations);

            var expected = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out _, type)
                .Select(declaration => declaration.QualifiedName.ToString())
                .OrderBy(_ => _);

            var actual = member.GetAllChildDeclarations()
                .Select(declaration => declaration.QualifiedName.ToString())
                .OrderBy(_ => _);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestSubWithLineLabelName)]
        [TestCase(CodeExplorerTestCode.TestSubWithUnresolvedMemberName)]
        [TestCase(CodeExplorerTestCode.TestFunctionName)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyGet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyLet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertySet)]
        [TestCase(CodeExplorerTestCode.TestTypeName)]
        [TestCase(CodeExplorerTestCode.TestEnumName)]
        [TestCase(CodeExplorerTestCode.TestConstantName)]
        [TestCase(CodeExplorerTestCode.TestFieldName)]
        [TestCase(CodeExplorerTestCode.TestLibraryFunctionName)]
        [TestCase(CodeExplorerTestCode.TestLibraryProcedureName)]
        [TestCase(CodeExplorerTestCode.TestEventName)]
        public void Synchronize_ClearsPassedDeclarationList_NoChanges(string name, DeclarationType type = DeclarationType.Member)
        {
            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out var memberDeclaration, type);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, declarations);

            var updates = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out _, type);
            member.Synchronize(updates);

            Assert.AreEqual(0, updates.Count);
        }

        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerTestCode.TestSubName, CodeExplorerTestCode.TestSubWithLineLabelName)]
        [TestCase(CodeExplorerTestCode.TestSubWithLineLabelName, CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestSubWithUnresolvedMemberName, CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestFunctionName, CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, CodeExplorerTestCode.TestSubName, DeclarationType.PropertyGet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, CodeExplorerTestCode.TestSubName, DeclarationType.PropertyLet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, CodeExplorerTestCode.TestSubName, DeclarationType.PropertySet)]
        [TestCase(CodeExplorerTestCode.TestTypeName, CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestEnumName, CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestConstantName, CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestFieldName, CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestLibraryFunctionName, CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestLibraryProcedureName, CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestEventName, CodeExplorerTestCode.TestSubName)]
        public void Synchronize_DoesNotAlterDeclarationList_DifferentComponent(string name, string other, DeclarationType type = DeclarationType.Member)
        {
            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out var memberDeclaration, type);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, declarations);

            var updates = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(other, out _);
            member.Synchronize(updates);

            var expected = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(other, out _)
                .Select(declaration => declaration.QualifiedName.ToString()).OrderBy(_ => _);
            var actual = updates.Select(declaration => declaration.QualifiedName.ToString()).OrderBy(_ => _);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestSubWithLineLabelName)]
        [TestCase(CodeExplorerTestCode.TestSubWithUnresolvedMemberName)]
        [TestCase(CodeExplorerTestCode.TestFunctionName)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyGet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyLet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertySet)]
        [TestCase(CodeExplorerTestCode.TestTypeName)]
        [TestCase(CodeExplorerTestCode.TestEnumName)]
        [TestCase(CodeExplorerTestCode.TestConstantName)]
        [TestCase(CodeExplorerTestCode.TestFieldName)]
        [TestCase(CodeExplorerTestCode.TestLibraryFunctionName)]
        [TestCase(CodeExplorerTestCode.TestLibraryProcedureName)]
        [TestCase(CodeExplorerTestCode.TestEventName)]
        public void Synchronize_PlacesAllTrackedDeclarations_NoChanges(string name, DeclarationType type = DeclarationType.Member)
        {
            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out var memberDeclaration, type);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, declarations);

            var updates = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out _, type);
            member.Synchronize(updates);

            var expected = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out _, type)
                .Select(declaration => declaration.QualifiedName.ToString()).OrderBy(_ => _);

            var actual = member.GetAllChildDeclarations()
                .Select(declaration => declaration.QualifiedName.ToString())
                .OrderBy(_ => _);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [Test]
        [Category("Code Explorer")]
        [TestCase(CodeExplorerTestCode.TestSubName)]
        [TestCase(CodeExplorerTestCode.TestSubWithLineLabelName)]
        [TestCase(CodeExplorerTestCode.TestSubWithUnresolvedMemberName)]
        [TestCase(CodeExplorerTestCode.TestFunctionName)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyGet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertyLet)]
        [TestCase(CodeExplorerTestCode.TestPropertyName, DeclarationType.PropertySet)]
        [TestCase(CodeExplorerTestCode.TestTypeName)]
        [TestCase(CodeExplorerTestCode.TestEnumName)]
        [TestCase(CodeExplorerTestCode.TestConstantName)]
        [TestCase(CodeExplorerTestCode.TestFieldName)]
        [TestCase(CodeExplorerTestCode.TestLibraryFunctionName)]
        [TestCase(CodeExplorerTestCode.TestLibraryProcedureName)]
        [TestCase(CodeExplorerTestCode.TestEventName)]
        public void Synchronize_SetsDeclarationNull_NoDeclarationsForComponent(string name, DeclarationType type = DeclarationType.Member)
        {
            var declarations = CodeExplorerTestSetup.TestProjectOneDeclarations.TestMemberDeclarations(name, out var memberDeclaration, type);
            var member = new CodeExplorerMemberViewModel(null, memberDeclaration, declarations);
            if (member.Declaration is null)
            {
                Assert.Inconclusive("Project declaration is null. Fix test setup and see why no other tests failed.");
            }

            member.Synchronize(Enumerable.Empty<Declaration>().ToList());

            Assert.IsNull(member.Declaration);
        }
    }
}
