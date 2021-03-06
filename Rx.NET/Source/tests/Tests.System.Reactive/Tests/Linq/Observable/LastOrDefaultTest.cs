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
    public class LastOrDefaultTest : ReactiveTest
    {

        [Fact]
        public void LastOrDefault_ArgumentChecking()
        {
            ReactiveAssert.Throws<ArgumentNullException>(() => Observable.LastOrDefault(default(IObservable<int>)));
            ReactiveAssert.Throws<ArgumentNullException>(() => Observable.LastOrDefault(default(IObservable<int>), _ => true));
            ReactiveAssert.Throws<ArgumentNullException>(() => Observable.LastOrDefault(DummyObservable<int>.Instance, default(Func<int, bool>)));
        }

        [Fact]
        public void LastOrDefault_Empty()
        {
            Assert.Equal(default(int), Observable.Empty<int>().LastOrDefault());
        }

        [Fact]
        public void LastOrDefaultPredicate_Empty()
        {
            Assert.Equal(default(int), Observable.Empty<int>().LastOrDefault(_ => true));
        }

        [Fact]
        public void LastOrDefault_Return()
        {
            var value = 42;
            Assert.Equal(value, Observable.Return<int>(value).LastOrDefault());
        }

        [Fact]
        public void LastOrDefault_Throw()
        {
            var ex = new Exception();

            var xs = Observable.Throw<int>(ex);

            ReactiveAssert.Throws(ex, () => xs.LastOrDefault());
        }

        [Fact]
        public void LastOrDefault_Range()
        {
            var value = 42;
            Assert.Equal(value, Observable.Range(value - 9, 10).LastOrDefault());
        }

        [Fact]
        public void LastOrDefaultPredicate_Range()
        {
            var value = 42;
            Assert.Equal(50, Observable.Range(value, 10).LastOrDefault(i => i % 2 == 0));
        }

    }
}
