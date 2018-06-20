using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeTestAtribute : Attribute
{
    private string TestHeader = String.Empty;

    public string Header
    {
        get { return TestHeader; }
    }
    
    public RuntimeTestAtribute()
    {
    }

    public RuntimeTestAtribute(string testHeader)
    {
        TestHeader = testHeader;
    }
}