using System;
using System.Collections.Generic;
using System.Text;

namespace Huellas_Bites
{
    public interface ICls_Huella_Bytes
    {
        Byte[] Imagen_Bytes { get; }


        void Obtener_Bytes_Huella(int Huella_Id);
    }

}
