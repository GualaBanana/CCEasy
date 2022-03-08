using Xunit;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace TestSolutionTester;

public class TestSolutionMethodDiscovering
{
    [Theory]
    [MemberData(nameof(ConstructorsOfContainersWithValidSolutionMethods))]
    public void ShouldNotThrow_WhenSolutionContainerDefinesValidSolutionMethod(Action constructor)
    {
        bool exceptionWasThrown = false;
        try
        {
            constructor();
        }
        catch (Exception)
        {
            exceptionWasThrown = true;
        }
        Assert.False(exceptionWasThrown);
    }
    public static IEnumerable<object[]> ConstructorsOfContainersWithValidSolutionMethods = new List<Action[]>()
    {
        new Action[] { () => SolutionTesterConstructorProvider.For<OutputSolution, int>() },
        new Action[] { () => SolutionTesterConstructorProvider.For<InputSolution, int>() }
    };

    [Fact]
    public void ShouldThrowEntryPointNotFoundException_WhenNoSolutionMethodsWereDiscovered()
    {
        Assert.Throws<EntryPointNotFoundException>(SolutionTesterConstructorProvider.For<IgnoredSolutionMethods, int>());
    }

    [Fact]
    public void ShouldThrowAmbiguousMatchException_WhenMultipleSolutionMethodsWereDiscovered()
    {
        Assert.Throws<AmbiguousMatchException>(SolutionTesterConstructorProvider.For<MultipleSolutionMethods, int>());
    }

    [Fact]
    public void ShouldThrowAmbiguousMatchException_WhenBothAttributeApplied()
    {
        Assert.Throws<AmbiguousMatchException>(SolutionTesterConstructorProvider.For<InputAndOutputSolutions, int>());
    }

    [Fact]
    public void ShouldThrowAmbiguousMatchException_WhenMultipleResultAttributesApplied()
    {
        Assert.Throws<AmbiguousMatchException>(SolutionTesterConstructorProvider.For<MultipleInputSolutionAttributes, int>());
    }

    [Fact]
    public void ShouldThrowFormatException_WhenSolutionProviderReturnsVoid()
    {
        Assert.Throws<FormatException>(SolutionTesterConstructorProvider.For<OutputSolutionWrongType, int>());
    }

    [Fact]
    public void ShouldThrowFormatException_WhenResultProviderDoesNotReturnVoid()
    {
        Assert.Throws<FormatException>(SolutionTesterConstructorProvider.For<InputSolutionWrongType, int>());
    }
}