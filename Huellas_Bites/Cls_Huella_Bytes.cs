using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Huellas_Bites
{
    public class Cls_Huella_Bytes : ICls_Huella_Bytes
    {
       

        private Byte[] p_Imagen_Bytes = null;


        public Byte[] Imagen_Bytes
        {
            get
            {
                return p_Imagen_Bytes;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Huella_Id"></param>
        void ICls_Huella_Bytes.Obtener_Bytes_Huella(int Huella_Id)
        {
            try
            {
                List<Huella_Empleado> Lista_Huellas = new List<Huella_Empleado>();
                string Str_Conexion = "server = IRHQNB021;Database = Sistema_Comedor; Uid = sa; Pwd = Passw0rd";
                DataTable Dt_Consulta = new DataTable();


                using (SqlConnection cn = new SqlConnection(Str_Conexion))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("select Huella_Id, Empleado_id, Huella_Digital from Cat_Empleados_Huellas where Huella_Id = '" + Huella_Id + "'", cn);
                    SqlDataReader dr = cmd.ExecuteReader();


                    while (dr.Read())
                    {
                        Huella_Empleado Huella_ = new Huella_Empleado();
                        Huella_.Huella_ID = dr.GetInt32(0);
                        Huella_.Empleado_ID = dr.GetString(1);
                        Huella_.Huella_Digital = (byte[])(dr.GetValue(2));



                        p_Imagen_Bytes = Huella_.Huella_Digital;

                    }
                    dr.Close();


                }

            }
            catch
            {

            }
        }
    }


    public class Huella_Empleado
    {
        public int Huella_ID { get; set; }
        public string Empleado_ID { get; set; }
        public byte[] Huella_Digital { get; set; }
    }

}
