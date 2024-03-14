﻿// Copyright (c) Microsoft. All rights reserved.

using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlpkTest;

using Native = GlpkNativeApi.GlpkNative;

[TestClass]
public sealed class BasicTests
{
    [TestMethod]
    public void CreateProblem()
    {
        var prob = Native.glp_create_prob();
        try
        {
        }
        finally
        {
            Native.glp_delete_prob(prob);
        }
    }

    [TestMethod]
    public void AddRowsAndCols()
    {
        var prob = Native.glp_create_prob();
        try
        {
            int r = Native.glp_add_rows(prob, 3);
            int c = Native.glp_add_cols(prob, 5);
            Assert.AreEqual(1, r);
            Assert.AreEqual(1, c);
            Assert.AreEqual(3, Native.glp_get_num_rows(prob));
            Assert.AreEqual(5, Native.glp_get_num_cols(prob));
        }
        finally
        {
            Native.glp_delete_prob(prob);
        }
    }

    [TestMethod]
    public void Simplex()
    {
        var prob = Native.glp_create_prob();
        try
        {
            int r = Native.glp_add_rows(prob, 1);
            int c = Native.glp_add_cols(prob, 2);
            Assert.AreEqual(1, r);
            Assert.AreEqual(1, c);
            Assert.AreEqual(1, Native.glp_get_num_rows(prob));
            Assert.AreEqual(2, Native.glp_get_num_cols(prob));

            // Note that indices are 1-based!

            // Define the objective: maximize x + y.
            Native.glp_set_obj_coef(prob, 1, 10);
            Native.glp_set_obj_coef(prob, 2, 11);
            Native.glp_set_obj_dir(prob, Native.GLP_MAX);

            // Set variable bounds.
            Native.glp_set_col_bnds(prob, 1, Native.GLP_DB, 0, 5.5);
            Native.glp_set_col_bnds(prob, 2, Native.GLP_DB, 0, 5.5);

            // x + y <= 8.
            Native.glp_set_row_bnds(prob, 1, Native.GLP_UP, double.NegativeInfinity, 8);
            // Note that because indices are 1-based, there needs to be a dummy initial term in each array.
            // These values are ignored by GLPK, but needed!
            Native.glp_set_mat_row(prob, 1, 2, new[] { -1, 1, 2 }, new[] { double.NaN, 1.0, 1.0 });

            // Solve.
            Native.glp_init_smcp(out var smcp);
            smcp.presolve = Native.GLP_ON;
            var res = Native.glp_simplex(prob, ref smcp);
            int status = Native.glp_get_status(prob);

            // Verify.
            Assert.AreEqual(0, res);
            Assert.AreEqual(Native.GLP_OPT, status);
            Assert.AreEqual(2.5, Native.glp_get_col_prim(prob, 1));
            Assert.AreEqual(5.5, Native.glp_get_col_prim(prob, 2));
            Assert.AreEqual(85.5, Native.glp_get_obj_val(prob));
        }
        finally
        {
            Native.glp_delete_prob(prob);
        }
    }

    [TestMethod]
    public void Mip()
    {
        var prob = Native.glp_create_prob();
        try
        {
            int r = Native.glp_add_rows(prob, 1);
            int c = Native.glp_add_cols(prob, 2);
            Assert.AreEqual(1, r);
            Assert.AreEqual(1, c);
            Assert.AreEqual(1, Native.glp_get_num_rows(prob));
            Assert.AreEqual(2, Native.glp_get_num_cols(prob));

            // Note that indices are 1-based!

            // Define the objective: maximize x + y.
            Native.glp_set_obj_coef(prob, 1, 10);
            Native.glp_set_obj_coef(prob, 2, 11);
            Native.glp_set_obj_dir(prob, Native.GLP_MAX);

            // Set variable bounds and kinds.
            Native.glp_set_col_kind(prob, 1, Native.GLP_IV);
            Native.glp_set_col_bnds(prob, 1, Native.GLP_DB, 0, 5);
            Native.glp_set_col_bnds(prob, 2, Native.GLP_DB, 0, 5.5);

            // x + y <= 8.
            Native.glp_set_row_bnds(prob, 1, Native.GLP_UP, double.NegativeInfinity, 8);
            // Note that because indices are 1-based, there needs to be a dummy initial term in each array.
            // These values are ignored by GLPK, but needed!
            Native.glp_set_mat_row(prob, 1, 2, new[] { -1, 1, 2 }, new[] { double.NaN, 1.0, 1.0 });

            // Solve.
            Native.glp_init_iocp(out var iocp);
            iocp.presolve = Native.GLP_ON;
            var res = Native.glp_intopt(prob, ref iocp);
            int status = Native.glp_mip_status(prob);

            // Verify.
            Assert.AreEqual(0, res);
            Assert.AreEqual(Native.GLP_OPT, status);
            Assert.AreEqual(3, Native.glp_mip_col_val(prob, 1));
            Assert.AreEqual(5, Native.glp_mip_col_val(prob, 2));
            Assert.AreEqual(85, Native.glp_mip_obj_val(prob));
        }
        finally
        {
            Native.glp_delete_prob(prob);
        }
    }
}
