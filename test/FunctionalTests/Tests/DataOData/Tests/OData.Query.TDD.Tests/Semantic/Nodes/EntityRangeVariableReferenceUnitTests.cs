﻿//---------------------------------------------------------------------
// <copyright file="EntityRangeVariableReferenceUnitTests.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.Test.OData.Query.TDD.Tests.Semantic
{
    #region Namespaces

    using System;
    using FluentAssertions;
    using Microsoft.OData.Core.UriParser.TreeNodeKinds;
    using Microsoft.OData.Edm;
    using Microsoft.OData.Edm.Library;
    using Microsoft.OData.Core;
    using Microsoft.OData.Core.UriParser;
    using Microsoft.OData.Core.UriParser.Semantic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    #endregion Namespaces

    /// <summary>
    /// Unit tests for the EntityRangeVariableReferenceNode class
    /// </summary>
    [TestClass]
    public class EntityRangeVariableReferenceUnitTests
    {
        [TestMethod]
        public void NameCannotBeNull()
        {
            EntityRangeVariable rangeVariable = new EntityRangeVariable("dogs", HardCodedTestModel.GetDogTypeReference(), HardCodedTestModel.GetDogsSet());
            Action createWithNullName = () => new EntityRangeVariableReferenceNode(null, rangeVariable);
            createWithNullName.ShouldThrow<Exception>(Error.ArgumentNull("name").ToString());
        }

        [TestMethod]
        public void RangeVariableIsSetCorrectly()
        {
            EntityRangeVariable rangeVariable = new EntityRangeVariable("dogs", HardCodedTestModel.GetDogTypeReference(), HardCodedTestModel.GetDogsSet());
            EntityRangeVariableReferenceNode referenceNode = new EntityRangeVariableReferenceNode(rangeVariable.Name, rangeVariable);
            referenceNode.RangeVariable.ShouldBeEntityRangeVariable(HardCodedTestModel.GetDogTypeReference()).And.NavigationSource.Should().Be(HardCodedTestModel.GetDogsSet());
        }

        [TestMethod]
        public void EntitySetComesFromRangeVariable()
        {
            EntityRangeVariable rangeVariable = new EntityRangeVariable("dogs", HardCodedTestModel.GetDogTypeReference(), HardCodedTestModel.GetDogsSet());
            EntityRangeVariableReferenceNode referenceNode = new EntityRangeVariableReferenceNode(rangeVariable.Name, rangeVariable);
            referenceNode.NavigationSource.Should().Be(HardCodedTestModel.GetDogsSet());
        }

        [TestMethod]
        public void TypeReferenceComesFromRangeVariable()
        {
            EntityRangeVariable rangeVariable = new EntityRangeVariable("dogs", HardCodedTestModel.GetDogTypeReference(), HardCodedTestModel.GetDogsSet());
            EntityRangeVariableReferenceNode referenceNode = new EntityRangeVariableReferenceNode(rangeVariable.Name, rangeVariable);
            referenceNode.TypeReference.FullName().Should().Be(HardCodedTestModel.GetDogTypeReference().FullName());
        }

        [TestMethod]
        public void TypeReferenceIsEdmEntityTypeReference()
        {
            EntityRangeVariable rangeVariable = new EntityRangeVariable("dogs", HardCodedTestModel.GetDogTypeReference(), HardCodedTestModel.GetDogsSet());
            EntityRangeVariableReferenceNode referenceNode = new EntityRangeVariableReferenceNode(rangeVariable.Name, rangeVariable);
            referenceNode.TypeReference.Should().BeOfType<EdmEntityTypeReference>();
        }

        [TestMethod]
        public void EntityTypeReferenceIsSameAsTypeReference()
        {
            EntityRangeVariable rangeVariable = new EntityRangeVariable("dogs", HardCodedTestModel.GetDogTypeReference(), HardCodedTestModel.GetDogsSet());
            EntityRangeVariableReferenceNode referenceNode = new EntityRangeVariableReferenceNode(rangeVariable.Name, rangeVariable);
            referenceNode.EntityTypeReference.Should().BeSameAs(referenceNode.TypeReference);
        }

        [TestMethod]
        public void KindIsEntityRangeVariableReferenceNode()
        {
            EntityRangeVariable rangeVariable = new EntityRangeVariable("dogs", HardCodedTestModel.GetDogTypeReference(), HardCodedTestModel.GetDogsSet());
            EntityRangeVariableReferenceNode referenceNode = new EntityRangeVariableReferenceNode(rangeVariable.Name, rangeVariable);
            referenceNode.InternalKind.Should().Be(InternalQueryNodeKind.EntityRangeVariableReference);
        }
    }
}
