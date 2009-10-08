using System;

namespace FarsiLibrary.WinFormDemo.Demo
{
    public interface IDemoPage
    {
        string Title { get; }

        bool IsNew { get; }
    }
}