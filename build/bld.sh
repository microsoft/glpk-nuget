chmod +x ../glpk/glpk-5.0/configure
rm -rf xout_linux/
mkdir xout_linux
pushd xout_linux
../../glpk/glpk-5.0/configure
make
ls -la src/.libs/libglpk.so*
popd
