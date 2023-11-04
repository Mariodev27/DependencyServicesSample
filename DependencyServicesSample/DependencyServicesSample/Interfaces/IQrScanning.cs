using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DependencyServicesSample.Interfaces
{
    public interface IQrScanning
    {
        Task<string> ScanAsync();
    }
}
