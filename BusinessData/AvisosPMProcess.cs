using SAP.Middleware.Connector;
using SAPAvisosPM.Entidades;
using SAPAvisosPM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace SAPAvisosPM.BusinessData
{
    public class AvisosPMProcess
    {
        public listResultadoNuevoAvisoPM registrarNuevoAviso(String pambiente, AVisoPM avisoPM)
        {
            DestinationConfig cfg = null;
            RfcRepository repo = null;
            RfcDestination dest = null;
            listResultadoNuevoAvisoPM listresultadoNuevoAvisoPM = new listResultadoNuevoAvisoPM();
            ResultadoNuevoAvisoPM resultadoNuevoAvisoPM;

            try
            {
                cfg = new DestinationConfig();

                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                dest = RfcDestinationManager.GetDestination(pambiente);//"QA");

                try
                {
                    dest.Ping();
                    repo = dest.Repository;
                }
                catch (Exception e1)
                {
                    if (cfg != null)
                    { RfcDestinationManager.UnregisterDestinationConfiguration(cfg); }

                    listresultadoNuevoAvisoPM.codResultado = -1;
                    listresultadoNuevoAvisoPM.DescrpResultado = WebConfigurationManager.AppSettings[Constantes.MSGSERRORREDSOCKS];
                    NetLogger.WriteLog(ELogLevel.ERROR, e1.Message + Environment.NewLine + e1.StackTrace);
                    return listresultadoNuevoAvisoPM;
                }

                IRfcFunction Function_ZRFC_CREAR_AVISO = repo.CreateFunction("ZRFC_CREAR_AVISO");

                RfcStructureMetadata ZST_HEADER_NOTIF_CREATE_MetaData = repo.GetStructureMetadata("ZST_HEADER_NOTIF_CREATE");
                IRfcStructure ZST_HEADER_NOTIF_CREATE = ZST_HEADER_NOTIF_CREATE_MetaData.CreateStructure();
                ZST_HEADER_NOTIF_CREATE.SetValue("EQUIPMENT", avisoPM.resultadoConsulta.numEquipo);
                ZST_HEADER_NOTIF_CREATE.SetValue("FUNCT_LOC", avisoPM.resultadoConsulta.ubicaTecnica);
                ZST_HEADER_NOTIF_CREATE.SetValue("SHORT_TEXT", avisoPM.textoBreve);

                //si Z2 prioridad no es obligatorio y puede ir en blanco
                if (avisoPM.prioridad.prioridad != "-1")
                { ZST_HEADER_NOTIF_CREATE.SetValue("PRIORITY", avisoPM.prioridad.prioridad); }
                else { ZST_HEADER_NOTIF_CREATE.SetValue("PRIORITY", string.Empty); }


                if (avisoPM.grupoPlanificacion != null)
                {
                    ZST_HEADER_NOTIF_CREATE.SetValue("PLANGROUP", avisoPM.grupoPlanificacion.grupoPlanificacion);
                    ZST_HEADER_NOTIF_CREATE.SetValue("PLANPLANT", avisoPM.grupoPlanificacion.centroPlanifiMante);
                }

                ZST_HEADER_NOTIF_CREATE.SetValue("REPORTEDBY", avisoPM.nombreAutor);
                ZST_HEADER_NOTIF_CREATE.SetValue("NOTIF_DATE", DateTime.Now.ToString("yyyyMMdd"));
                ZST_HEADER_NOTIF_CREATE.SetValue("NOTIFTIME", DateTime.Now.ToString("HHmmss"));

                if (avisoPM.Origen != null)
                {
                    ZST_HEADER_NOTIF_CREATE.SetValue("CODE_GROUP", avisoPM.Origen.GrupoCodigo);
                    ZST_HEADER_NOTIF_CREATE.SetValue("CODING", avisoPM.Origen.Codigo);
                }
                else
                {
                    ZST_HEADER_NOTIF_CREATE.SetValue("CODE_GROUP", string.Empty);
                    ZST_HEADER_NOTIF_CREATE.SetValue("CODING", string.Empty);
                }


                ZST_HEADER_NOTIF_CREATE.SetValue("BREAKDOWN", avisoPM.equipoParado ? Constantes.PI_BUSQ_MARCADO : Constantes.PI_BUSQ_VACIO);

                if (avisoPM.repercusion != null)
                { ZST_HEADER_NOTIF_CREATE.SetValue("AUSWK", avisoPM.repercusion.repercusion); }


                RfcStructureMetadata ZST_PSTO_TRABAJO_NOTIF_CREATE_MetaData = repo.GetStructureMetadata("ZST_PSTO_TRABAJO_NOTIF_CREATE");
                IRfcStructure ZST_PSTO_TRABAJO_NOTIF_CREATE = ZST_PSTO_TRABAJO_NOTIF_CREATE_MetaData.CreateStructure();

                if (avisoPM.puestoTrabajo != null)
                {
                    ZST_PSTO_TRABAJO_NOTIF_CREATE.SetValue("WORK_CNTR", avisoPM.puestoTrabajo.puestoTrabajo);
                    ZST_PSTO_TRABAJO_NOTIF_CREATE.SetValue("PLANT", avisoPM.puestoTrabajo.centro);
                }
                else
                {
                    ZST_PSTO_TRABAJO_NOTIF_CREATE.SetValue("WORK_CNTR", string.Empty);
                    ZST_PSTO_TRABAJO_NOTIF_CREATE.SetValue("PLANT", string.Empty);
                }


                //PARAMETROS IMPORT
                Function_ZRFC_CREAR_AVISO.SetValue("NOTIF_TYPE", avisoPM.claseAviso.claseAviso);
                Function_ZRFC_CREAR_AVISO.SetValue("NOTIFHEADER", ZST_HEADER_NOTIF_CREATE);
                Function_ZRFC_CREAR_AVISO.SetValue("PSTO_TRABAJO", ZST_PSTO_TRABAJO_NOTIF_CREATE);

                //CREACION DE TABLA PT_NOTITEM
                IRfcTable Table_PT_NOTITEM = Function_ZRFC_CREAR_AVISO.GetTable("PT_NOTITEM");
                Table_PT_NOTITEM.Append();  //add row

                if (avisoPM.sintoma != null)
                {
                    Table_PT_NOTITEM.SetValue("D_CODEGRP", avisoPM.sintoma.grupoCodigo);
                    Table_PT_NOTITEM.SetValue("D_CODE", avisoPM.sintoma.codigo);
                }

                if (avisoPM.parteObjeto != null)
                {
                    Table_PT_NOTITEM.SetValue("DL_CODEGRP", avisoPM.parteObjeto.grupoCodigo);
                    Table_PT_NOTITEM.SetValue("DL_CODE", avisoPM.parteObjeto.codigo);
                }
                Table_PT_NOTITEM.SetValue("ITEM_SORT_NO", 1);
                Table_PT_NOTITEM.SetValue("DESCRIPT", avisoPM.textoSintoma);
                Table_PT_NOTITEM.SetValue("ITEM_KEY", 1);
                
                if (avisoPM.causa != null || avisoPM.textoCausa != string.Empty)
                {
                    //CREACION DE TABLA PT_CAUSAS
                    IRfcTable Table_PT_CAUSAS = Function_ZRFC_CREAR_AVISO.GetTable("PT_CAUSAS");
                    Table_PT_CAUSAS.Append();  //add row

                    Table_PT_CAUSAS.SetValue("CAUSE_SORT_NO", 1);
                    Table_PT_CAUSAS.SetValue("ITEM_KEY", 1);
                    Table_PT_CAUSAS.SetValue("CAUSETEXT", avisoPM.textoCausa);

                    if (avisoPM.causa != null)
                    {
                        Table_PT_CAUSAS.SetValue("CAUSE_CODEGRP", avisoPM.causa.grupoCodigo);
                        Table_PT_CAUSAS.SetValue("CAUSE_CODE", avisoPM.causa.codigo);
                    }

                    Table_PT_CAUSAS.SetValue("ITEM_SORT_NO", 1);
                }


                //CREACION DE TABLA PT_LONGTEXTS
                IRfcTable Table_PT_LONGTEXTS = Function_ZRFC_CREAR_AVISO.GetTable("PT_LONGTEXTS");

                if (avisoPM.listatextoLargo != null)
                {
                    for (int i = 0; i < avisoPM.listatextoLargo.Count(); i++)
                    {
                        string textoLargo = avisoPM.listatextoLargo.ElementAt(i);

                        Table_PT_LONGTEXTS.Append();  //add row
                        Table_PT_LONGTEXTS.SetValue("OBJTYPE", "QMEL");
                        Table_PT_LONGTEXTS.SetValue("OBJKEY", i + 1);
                        Table_PT_LONGTEXTS.SetValue("FORMAT_COL", "*");
                        Table_PT_LONGTEXTS.SetValue("TEXT_LINE", textoLargo);
                    }
                }

                //EJECUCION DE RFC
                Function_ZRFC_CREAR_AVISO.Invoke(dest);

                //OBTENER EL RESULTADO DE LA EJECUCION
                IRfcTable table_PT_RETURN = Function_ZRFC_CREAR_AVISO.GetTable("PT_RETURN");
                List<ResultadoNuevoAvisoPM> lstresultadoNuevoAvisoPM = new List<ResultadoNuevoAvisoPM>();

                foreach (IRfcStructure row in table_PT_RETURN)
                {
                    resultadoNuevoAvisoPM = new ResultadoNuevoAvisoPM();
                    resultadoNuevoAvisoPM.type = row.GetValue("TYPE").ToString();
                    resultadoNuevoAvisoPM.idResultado = row.GetValue("ID").ToString();
                    resultadoNuevoAvisoPM.numberresultado = row.GetValue("NUMBER").ToString();
                    resultadoNuevoAvisoPM.message = row.GetValue("MESSAGE").ToString();
                    lstresultadoNuevoAvisoPM.Add(resultadoNuevoAvisoPM);
                }

                listresultadoNuevoAvisoPM.lstresultadoNuevoAvisoPM = lstresultadoNuevoAvisoPM;
                RfcDestinationManager.UnregisterDestinationConfiguration(cfg);

                resultadoNuevoAvisoPM = null;

                listresultadoNuevoAvisoPM.codResultado = 0;
                listresultadoNuevoAvisoPM.DescrpResultado = string.Empty;

            }
            catch (Exception ex)
            {
                if (cfg != null)
                { RfcDestinationManager.UnregisterDestinationConfiguration(cfg); }

                listresultadoNuevoAvisoPM.codResultado = -1;
                listresultadoNuevoAvisoPM.DescrpResultado = ex.Message.ToString();
                NetLogger.WriteLog(ELogLevel.ERROR, ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return listresultadoNuevoAvisoPM;
        }

    }
}
