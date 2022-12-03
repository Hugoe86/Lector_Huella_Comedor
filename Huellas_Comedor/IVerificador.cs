using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huellas_Comedor
{
    public interface IVerificador
    {
        Int32 Id { get; }


        void Verificar(DPFP.Sample Sample);

    }
}
