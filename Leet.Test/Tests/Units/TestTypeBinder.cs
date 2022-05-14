﻿//MIT License

//Copyright (c) 2022 GualaBanana

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Leet.Services;
using Leet.Test.Framework.TestData;
using System;
using Xunit;

namespace Leet.Test.Tests.Units;

public class TestTypeBinder
{
    [Theory]
    [MemberData(nameof(TypeData.ReferenceTypes), MemberType = typeof(TypeData))]
    public void CanHoldNull_ReferenceType_ReturnsTrue(Type type)
    {
        Assert.True(TypeBinder.CanHoldNull(type));
    }

    [Theory]
    [MemberData(nameof(TypeData.NullableTypes), MemberType = typeof(TypeData))]
    public void CanHoldNull_NullableType_ReturnsTrue(Type type)
    {
        Assert.True(TypeBinder.CanHoldNull(type));
    }



    [Theory]
    [MemberData(nameof(TypeData.ValueTypes), MemberType = typeof(TypeData))]
    [MemberData(nameof(TypeData.ReferenceTypes), MemberType = typeof(TypeData))]
    [MemberData(nameof(TypeData.NullableTypes), MemberType = typeof(TypeData))]
    public void CanBind_SameTypes_ReturnsTrue(Type type)
    {
        Assert.True(TypeBinder.CanBind(type, type));
    }

    [Theory]
    [MemberData(nameof(TypeData.ValueTypes), MemberType = typeof(TypeData))]
    [MemberData(nameof(TypeData.NullableTypes), MemberType = typeof(TypeData))]
    public void CanBind_ChildAndParentType_ReturnsTrue(Type type)
    {
        Assert.True(TypeBinder.CanBind(type, typeof(object)));
    }
}
