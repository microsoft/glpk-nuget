﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace GlpkNativeApi;

/// <summary>
/// This is the raw GLPK native API. It can be extended as new entry points are determined to be needed.
/// REVIEW: Perhaps include the full exported API now.
/// </summary>
internal static class GlpkNative
{
    private const string Library = "glpk";

    public const int GLP_ON = 1; /* enable something */
    public const int GLP_OFF = 0; /* disable something */

    // optimization direction flag:
    public const int GLP_MIN = 1; /* minimization */
    public const int GLP_MAX = 2; /* maximization */

    // kind of structural variable:
    public const int GLP_CV = 1; /* continuous variable */
    public const int GLP_IV = 2; /* integer variable */
    public const int GLP_BV = 3; /* binary variable */

    /* type of auxiliary/structural variable: */
    public const int GLP_FR = 1; /* free (unbounded) variable */
    public const int GLP_LO = 2; /* variable with lower bound */
    public const int GLP_UP = 3; /* variable with upper bound */
    public const int GLP_DB = 4; /* double-bounded variable */
    public const int GLP_FX = 5; /* fixed variable */

    /* solution status: */
    public const int GLP_UNDEF = 1; /* solution is undefined */
    public const int GLP_FEAS = 2; /* solution is feasible */
    public const int GLP_INFEAS = 3; /* solution is infeasible */
    public const int GLP_NOFEAS = 4; /* no feasible solution exists */
    public const int GLP_OPT = 5; /* solution is optimal */
    public const int GLP_UNBND = 6; /* solution is unbounded */

    /* branching technique: */
    public const int GLP_BR_FFV = 1; /* first fractional variable */
    public const int GLP_BR_LFV = 2; /* last fractional variable */
    public const int GLP_BR_MFV = 3; /* most fractional variable */
    public const int GLP_BR_DTH = 4; /* heuristic by Driebeck and Tomlin */
    public const int GLP_BR_PCH = 5; /* hybrid pseudocost heuristic */

    /* backtracking technique: */
    public const int GLP_BT_DFS = 1; /* depth first search */
    public const int GLP_BT_BFS = 2; /* breadth first search */
    public const int GLP_BT_BLB = 3; /* best local bound */
    public const int GLP_BT_BPH = 4; /* best projection heuristic */

    /* preprocessing technique: */
    public const int GLP_PP_NONE = 0; /* disable preprocessing */
    public const int GLP_PP_ROOT = 1; /* preprocessing only on root level */
    public const int GLP_PP_ALL = 2; /* preprocessing on all levels */

    /// <summary>
    /// Mirrors the GLPK structure. Created by pasting from the source.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct glp_iocp
    {
        /* integer optimizer control parameters */
        public int msg_lev;            /* message level (see glp_smcp) */
        public int br_tech;            /* branching technique: */
        public int bt_tech;            /* backtracking technique: */
        public double tol_int;         /* mip.tol_int */
        public double tol_obj;         /* mip.tol_obj */
        public int tm_lim;             /* mip.tm_lim (milliseconds) */
        public int out_frq;            /* mip.out_frq (milliseconds) */
        public int out_dly;            /* mip.out_dly (milliseconds) */
        public IntPtr cb_func; // void (* cb_func) (glp_tree* T, void* info); /* mip.cb_func */
        public IntPtr cb_info; // void* cb_info;          /* mip.cb_info */
        public int cb_size;            /* mip.cb_size */
        public int pp_tech;            /* preprocessing technique: */
        public double mip_gap;         /* relative MIP gap tolerance */
        public int mir_cuts;           /* MIR cuts       (GLP_ON/GLP_OFF) */
        public int gmi_cuts;           /* Gomory's cuts  (GLP_ON/GLP_OFF) */
        public int cov_cuts;           /* cover cuts     (GLP_ON/GLP_OFF) */
        public int clq_cuts;           /* clique cuts    (GLP_ON/GLP_OFF) */
        public int presolve;           /* enable/disable using MIP presolver */
        public int binarize;           /* try to binarize integer variables */
        public int fp_heur;            /* feasibility pump heuristic */
        public int ps_heur;            /* proximity search heuristic */
        public int ps_tm_lim;          /* proxy time limit, milliseconds */
        public int sr_heur;            /* simple rounding heuristic */
        // #if 1 /* 24/X-2015; not documented--should not be used */
        public int use_sol;            /* use existing solution */
        public IntPtr save_sol; // const char *save_sol;   /* filename to save every new solution */
        public int alien;              /* use alien solver */
        // #endif
        // #if 1 /* 16/III-2016; not documented--should not be used */
        public int flip;               /* use long-step dual simplex */
        // #endif
        public fixed double foo_bar[23];     /* (reserved) */
    }

    /* message level: */
    public const int GLP_MSG_OFF = 0; /* no output */
    public const int GLP_MSG_ERR = 1; /* warning and error messages only */
    public const int GLP_MSG_ON = 2; /* normal output */
    public const int GLP_MSG_ALL = 3; /* full output */
    public const int GLP_MSG_DBG = 4; /* debug output */

    /* simplex method option: */
    public const int GLP_PRIMAL = 1; /* use primal simplex */
    public const int GLP_DUALP = 2; /* use dual; if it fails, use primal */
    public const int GLP_DUAL = 3; /* use dual simplex */

    /* pricing technique: */
    public const int GLP_PT_STD = 0x11; /* standard (Dantzig's rule) */
    public const int GLP_PT_PSE = 0x22; /* projected steepest edge */

    /* ratio test technique: */
    public const int GLP_RT_STD = 0x11; /* standard (textbook) */
    public const int GLP_RT_HAR = 0x22; /* Harris' two-pass ratio test */
    // #if true // /* 16/III-2016 */
    public const int GLP_RT_FLIP = 0x33; /* long-step (flip-flop) ratio test */
    // #endif

    /* option to use A or N: */
    public const int GLP_USE_AT = 1; /* use A matrix in row-wise format */
    public const int GLP_USE_NT = 2; /* use N matrix in row-wise format */

    /* return codes: */
    public const int GLP_EBADB = 0x01; /* invalid basis */
    public const int GLP_ESING = 0x02; /* singular matrix */
    public const int GLP_ECOND = 0x03; /* ill-conditioned matrix */
    public const int GLP_EBOUND = 0x04; /* invalid bounds */
    public const int GLP_EFAIL = 0x05; /* solver failed */
    public const int GLP_EOBJLL = 0x06; /* objective lower limit reached */
    public const int GLP_EOBJUL = 0x07; /* objective upper limit reached */
    public const int GLP_EITLIM = 0x08; /* iteration limit exceeded */
    public const int GLP_ETMLIM = 0x09; /* time limit exceeded */
    public const int GLP_ENOPFS = 0x0A; /* no primal feasible solution */
    public const int GLP_ENODFS = 0x0B; /* no dual feasible solution */
    public const int GLP_EROOT = 0x0C; /* root LP optimum not provided */
    public const int GLP_ESTOP = 0x0D; /* search terminated by application */
    public const int GLP_EMIPGAP = 0x0E; /* relative mip gap tolerance reached */
    public const int GLP_ENOFEAS = 0x0F; /* no primal/dual feasible solution */
    public const int GLP_ENOCVG = 0x10; /* no convergence */
    public const int GLP_EINSTAB = 0x11; /* numerical instability */
    public const int GLP_EDATA = 0x12; /* invalid data */
    public const int GLP_ERANGE = 0x13; /* result out of range */

    /// <summary>
    /// Mirrors the GLPK structure. Created by pasting from the source.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct glp_smcp
    {
        /* simplex solver control parameters */
        public int msg_lev;            /* message level: */
        public int meth;               /* simplex method option: */
        public int pricing;            /* pricing technique: */
        public int r_test;             /* ratio test technique: */
        public double tol_bnd;         /* primal feasibility tolerance */
        public double tol_dj;          /* dual feasibility tolerance */
        public double tol_piv;         /* pivot tolerance */
        public double obj_ll;          /* lower objective limit */
        public double obj_ul;          /* upper objective limit */
        public int it_lim;             /* simplex iteration limit */
        public int tm_lim;             /* time limit, ms */
        public int out_frq;            /* display output frequency, ms */
        public int out_dly;            /* display output delay, ms */
        public int presolve;           /* enable/disable using LP presolver */
        // #if 1 /* 11/VII-2017 (not documented yet) */
        public int excl;               /* exclude fixed non-basic variables */
        public int shift;              /* shift bounds of variables to zero */
        public int aorn;               /* option to use A or N: */
        public fixed double foo_bar[33];     /* (reserved) */
        // #endif
    }

    // Problem api.
    [DllImport(Library)] public static extern IntPtr glp_create_prob();
    [DllImport(Library)] public static extern void glp_delete_prob(IntPtr prob);
    [DllImport(Library)] public static extern int glp_get_num_rows(IntPtr prob);
    [DllImport(Library)] public static extern int glp_get_num_cols(IntPtr prob);
    [DllImport(Library)] public static extern int glp_add_rows(IntPtr prob, int nrs);
    [DllImport(Library)] public static extern int glp_add_cols(IntPtr prob, int ncs);
    [DllImport(Library)] public static extern void glp_set_row_bnds(IntPtr prob, int i, int type, double lb, double ub);
    [DllImport(Library)] public static extern void glp_set_col_bnds(IntPtr prob, int j, int type, double lb, double ub);
    [DllImport(Library)] public static extern void glp_set_col_kind(IntPtr prob, int j, int kind);
    [DllImport(Library)] public static extern void glp_set_obj_dir(IntPtr prob, int dir);
    [DllImport(Library)] public static extern void glp_set_obj_coef(IntPtr prob, int j, double coef);
    [DllImport(Library)] public static extern void glp_set_mat_row(IntPtr prob, int i, int len, int[] ind, double[] val);

    // Simplex api.
    [DllImport(Library)] public static extern int glp_init_smcp(out glp_smcp parm);
    [DllImport(Library)] public static extern int glp_simplex(IntPtr prob, ref glp_smcp parm);
    [DllImport(Library)] public static extern int glp_get_status(IntPtr prob);
    [DllImport(Library)] public static extern double glp_get_obj_val(IntPtr prob);
    [DllImport(Library)] public static extern double glp_get_col_prim(IntPtr prob, int j);

    // MIP api.
    [DllImport(Library)] public static extern void glp_init_iocp(out glp_iocp parm);
    [DllImport(Library)] public static extern int glp_intopt(IntPtr prob, ref glp_iocp parm);
    [DllImport(Library)] public static extern int glp_mip_status(IntPtr prob);
    [DllImport(Library)] public static extern double glp_mip_obj_val(IntPtr prob);
    [DllImport(Library)] public static extern double glp_mip_col_val(IntPtr prob, int j);
}
