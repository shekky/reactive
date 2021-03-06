﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Microsoft.Reactive.Testing;
using Xunit;
using ReactiveTests.Dummies;
using System.Reflection;

namespace ReactiveTests.Tests
{
    public class SingleOrDefaultTest : ReactiveTest
    {

        [Fact]
        public void SingleOrDefault_ArgumentChecking()
        {
            ReactiveAssert.Throws<ArgumentNullException>(() => Observable.SingleOrDefault(default(IObservable<int>)));
            ReactiveAssert.Throws<ArgumentNullException>(() => Observable.SingleOrDefault(default(IObservable<int>), _ => true));
            ReactiveAssert.Throws<ArgumentNullException>(() => Observable.SingleOrDefault(DummyObservable<int>.Instance, default(Func<int, bool>)));
        }

        [Fact]
        public void SingleOrDefault_Empty()
        {
            Assert.Equal(default(int), Observable.Empty<int>().SingleOrDefault());
        }

        [Fact]
        public void SingleOrDefaultPredicate_Empty()
        {
            Assert.Equal(default(int), Observable.Empty<int>().SingleOrDefault(_ => true));
        }

        [Fact]
        public void SingleOrDefault_Return()
        {
            var value = 42;
            Assert.Equal(value, Observable.Return<int>(value).SingleOrDefault());
        }

        [Fact]
        public void SingleOrDefault_Throw()
        {
            var ex = new Exception();

            var xs = Observable.Throw<int>(ex);

            ReactiveAssert.Throws(ex, () => xs.SingleOrDefault());
        }

        [Fact]
        public void SingleOrDefault_Range()
        {
            var value = 42;
            ReactiveAssert.Throws<InvalidOperationException>(() => Observable.Range(value, 10).SingleOrDefault());
        }

        [Fact]
        public void SingleOrDefaultPredicate_Range()
        {
            var value = 42;
            ReactiveAssert.Throws<InvalidOperationException>(() => Observable.Range(value, 10).SingleOrDefault(i => i % 2 == 0));
        }

        [Fact]
        public void SingleOrDefault_Range_ReducesToSingle()
        {
            var value = 42;
            Assert.Equal(45, Observable.Range(value, 10).SingleOrDefault(i => i == 45));
        }

        [Fact]
        public void SingleOrDefault_Range_ReducesToNone()
        {
            var value = 42;
            Assert.Equal(0, Observable.Range(value, 10).SingleOrDefault(i => i > 100));
        }

    }
}
