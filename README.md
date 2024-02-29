# Summary

The purpose of this repository is to publish nuget packages containing compiled forms of GLPK (https://www.gnu.org/software/glpk/).
This repository should **_not_** be used for editing or enhancing GLPK in any way.

This repository contains the unaltered source for GLPK 5.0 in the `glpk` directory. This directory was populated by executing the
`get-glpk.cmd` command. GLPK is licensed under GPL 3.0, as shown in the `LICENSE` file. The `LICENSE` file is an unaltered copy of
the `glpk/glpk-5.0/COPYING` file.

In addition to the source for GLPK, this repository contains:
* Scripts to build GLPK for various platforms in the `build` directory.
* The project `test/GlpkTest.csporj` that is used to perform basic tests of the GLPK builds.
* The project `package/Glpk.Native.csproj` that is used solely to define and create a nuget package.

# Building

The `build` directory contains scripts for building GLPK.

To build for Windows (64 bit or 32 bit), on a Windows machine:

* Ensure that Micrsoft Visual Studio is properly installed and includes the native tools.
* If the path for `HOME_VCVARS` in `Build_Win.cmd` is not correct, edit that file to fix the path.
* cd to the `build` directory.
* Run `Build_Win.cmd x64` to compile for 64 bit Windows. The results are in the generated `xout_x64` directory.
* Run `Build_Win.cmd x86` to compile for 32 bit Windows. The results are in the generated `xout_x86` directory.

To build for Linux (or WSL), on a Linux machine:

* cd to the `build` directory.
* If needed run `chmod +x bld.sh`.
* Run `./bld.sh`. The results are in the generated `xout_linux` directory. Note that this uses the standard build
  process as specified by the GLPK documentation, so may be customized accordingly.

TODO: Complete this document once nuget generation and publishing is completed.

# Testing

To test the builds on either Windows or Linux:

* First build GLPK as described above.
* cd to the `test` directory.
* Run `dotnet test GlpkTest.csproj`.

# Packaging

To create the nuget package:

* Ensure that all builds have been performed in the same directory structure.
* cd to the `package` directory.
* Run `dotnet pack Glpk.Native.csproj`.
* The package will be in `package/bin/Release/Glpk.Native.<ver>.nupkg`, were `<ver>` is the current version.
