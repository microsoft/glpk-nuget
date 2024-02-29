[GLPK](https://www.gnu.org/software/glpk/) is an open source linear programming toolkit licensed under GPL 3.0.

This package contains native builds of GLPK version 5.0 for:
* 64 bit Windows
* 32 bit Windows
* 64 bit Linux (Ubuntu)

It also contains [GlpkNative.cs](https://github.com/microsoft/glpk-nuget/blob/main/content/GlpkNative.cs) which defines a C#
  static class named`GlpkNativeApi.GlpkNative` containing:
* C# `extern` definitions of some of the public entry points of GLPK.
* Some of the constants defined by GLPK.
* Some of the structures defined by GLPK.

This file is not part of the GLPK release, but is a convenient wrapper around it, using the dotnet PInvoke
technology to access the entry points in the GLPK runtime libraries. The use of this file is optional. If it
doesn't fit your needs, feel free to make your own wrapper around the native libraries. We haven't felt the
need to include all of the GLPK external api, just what we've needed. This may be extended over time as needed.
