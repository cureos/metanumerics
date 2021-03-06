Portable Meta Numerics
======================

[![NuGet](https://img.shields.io/nuget/v/Portable.Meta.Numerics.svg)](https://www.nuget.org/packages/Portable.Meta.Numerics/)
[![NuGet](https://img.shields.io/nuget/dt/Portable.Meta.Numerics.svg)](https://www.nuget.org/packages/Portable.Meta.Numerics/)

Copyright (c) 2008-2015 *David Wright*, portable adaptations (c) 2013-2015 *Anders Gustafsson, Cureos AB*.<br/>The library is licensed under the [Microsoft Public License (Ms-PL)](http://opensource.org/licenses/MS-PL).

This is a manual fork of *David Wright's* [Meta Numerics](https://metanumerics.codeplex.com/) project, with adaptations necessary to build *Meta Numerics* as a portable class library.

The portable class library currently targets:

* Windows 8 (f.k.a. Metro or Store applications) and higher
* .NET Framework version 4 and higher
* Windows Phone Silverlight version 8 and higher
* Windows Phone 8.1
* Silverlight version 5
* Xamarin.Android
* Xamarin.iOS (Classic and Unified API)

The solution contains one portable and one .NET only class library project. Except for binary serialization and ADO.NET support that is only available in the .NET dedicated library, these two projects share exactly the same code.

*Portable Meta Numerics* aims to follow closely the updates of source code and binary releases made of *David Wright's Meta Numerics*. Currently, *Portable Meta Numerics* is associated with *Meta Numerics* changeset [73356](https://metanumerics.codeplex.com/SourceControl/changeset/73356) committed on September 10, 2015.
