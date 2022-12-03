
using DPFP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Huellas_Comedor
{
    public class Verificador : IVerificador
    {

        public int p_Id = 0;


        public int Id
        {
            get
            {
                return p_Id;
            }
        }


        void IVerificador.Verificar(Sample Sample)
        {

            DPFP.Template Template = new DPFP.Template();
            DPFP.Verification.Verification Verificator = new DPFP.Verification.Verification();
            //Sistema_ComedorEntities contexto = new Sistema_ComedorEntities();
            DPFP.FeatureSet features = new DPFP.FeatureSet();


            List<Huella_Empleado> Lista_Huellas = new List<Huella_Empleado>();

            string Str_Conexion = "server = IRHQNB021;Database = Sistema_Comedor; Uid = sa; Pwd = Passw0rd";
            DataTable Dt_Consulta = new DataTable();

            Int32 empleado_id = 0;


            // Process the sample and create a feature set for the enrollment purpose.
            //DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);


            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;

            Extractor.CreateFeatureSet(Sample, DPFP.Processing.DataPurpose.Verification, ref feedback, ref features);            // TODO: return features as a result?
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
            {
                // Compare the feature set with our template
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();

                DPFP.Template template = new DPFP.Template();
                Stream stream;



                using (SqlConnection cn = new SqlConnection(Str_Conexion))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("select Huella_Id, Empleado_id, Huella_Digital from Cat_Empleados_Huellas", cn);
                    SqlDataReader dr = cmd.ExecuteReader();


                    while (dr.Read())
                    {
                        Huella_Empleado Huella_ = new Huella_Empleado();
                        Huella_.Huella_ID = dr.GetInt32(0);
                        Huella_.Empleado_ID = dr.GetString(1);
                        Huella_.Huella_Digital = (byte[])(dr.GetValue(2));
                        Lista_Huellas.Add(Huella_);

                    }
                    dr.Close();


                    foreach (var valor in Lista_Huellas)
                    {

                        stream = new MemoryStream(valor.Huella_Digital);

                        template = new DPFP.Template(stream);

                        Verificator.Verify(features, template, ref result);

                        if (result.Verified)
                        {
                            empleado_id = Convert.ToInt32(valor.Empleado_ID);
                            break;
                        }


                    }


                }



            }
            else
            {

            }


            this.p_Id = empleado_id;
        }
    }

    public class Huella_Empleado
    {
        public int Huella_ID { get; set; }
        public string Empleado_ID { get; set; }
        public byte[] Huella_Digital { get; set; }
    }


}
