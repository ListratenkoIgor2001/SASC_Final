using System;
using System.Collections.Generic;
using System.Text;

namespace SASC_Final.Services.Interfaces
{
    public interface ICryptografy
    {
        string Encode(string plain);
        string Decode(string cipher);
    }
}
