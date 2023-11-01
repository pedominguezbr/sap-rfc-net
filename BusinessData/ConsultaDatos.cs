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
    public class ConsultaDatos
    {
        public ListaResultadoConsulta Consultar(String pambiente, String tipoBusqueda, String valorBuscado)
        {
            DestinationConfig cfg = null;
            RfcRepository repo = null;
            RfcDestination dest = null;
            ListaResultadoConsulta listResultado = new ListaResultadoConsulta();

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

                    listResultado.codResultado = -1;
                    listResultado.DescrpResultado = WebConfigurationManager.AppSettings[Constantes.MSGSERRORREDSOCKS];
                    NetLogger.WriteLog(ELogLevel.ERROR, e1.Message + Environment.NewLine + e1.StackTrace);
                    return listResultado;
                }


                repo = dest.Repository;

                IRfcFunction Function__ZRFC_CONSULTA = repo.CreateFunction("ZRFC_CONSULTA");

                Function__ZRFC_CONSULTA.SetValue("PI_BUSQ1", tipoBusqueda == Constantes.PI_BUSQ1 ? Constantes.PI_BUSQ_MARCADO : Constantes.PI_BUSQ_VACIO);
                Function__ZRFC_CONSULTA.SetValue("PI_BUSQ2", tipoBusqueda == Constantes.PI_BUSQ2 ? Constantes.PI_BUSQ_MARCADO : Constantes.PI_BUSQ_VACIO);
                Function__ZRFC_CONSULTA.SetValue("PI_BUSQ3", tipoBusqueda == Constantes.PI_BUSQ3 ? Constantes.PI_BUSQ_MARCADO : Constantes.PI_BUSQ_VACIO);
                Function__ZRFC_CONSULTA.SetValue("PI_BUSQ4", tipoBusqueda == Constantes.PI_BUSQ4 ? Constantes.PI_BUSQ_MARCADO : Constantes.PI_BUSQ_VACIO);
                Function__ZRFC_CONSULTA.SetValue("PI_BUSQ5", tipoBusqueda == Constantes.PI_BUSQ5 ? Constantes.PI_BUSQ_MARCADO : Constantes.PI_BUSQ_VACIO);
                Function__ZRFC_CONSULTA.SetValue("PI_BUSQ6", tipoBusqueda == Constantes.PI_BUSQ6 ? Constantes.PI_BUSQ_MARCADO : Constantes.PI_BUSQ_VACIO);

                //CONVIRTIENDO EL VALOR BUSCADO A MAYUSCULA
                Function__ZRFC_CONSULTA.SetValue("PI_TEXTO", valorBuscado.ToUpper());

                Function__ZRFC_CONSULTA.Invoke(dest);

                IRfcTable tableResultado = Function__ZRFC_CONSULTA.GetTable("PT_RESULTADO");
                listResultado.listaResultadoConsulta = new List<ResultadoConsulta>();

                foreach (IRfcStructure row in tableResultado)
                {
                    ResultadoConsulta resultado = new ResultadoConsulta();

                    resultado.ubicaTecnica = row.GetValue("TPLNR").ToString();
                    resultado.numEquipo = row.GetValue("EQUNR").ToString();
                    resultado.denominaUbicaTecnica = row.GetValue("PLTXU").ToString();
                    resultado.denominaEquipo = row.GetValue("EQKTU").ToString();
                    resultado.clasificacion = row.GetValue("EQFNR").ToString();
                    resultado.grupoPlanificador = row.GetValue("INGRP").ToString();
                    resultado.perfildeCatalogo = row.GetValue("RBNR").ToString();
                    resultado.idResultado = listResultado.listaResultadoConsulta.Count;
                    listResultado.listaResultadoConsulta.Add(resultado);
                }

                RfcDestinationManager.UnregisterDestinationConfiguration(cfg);
                listResultado.codResultado = 0;
                listResultado.DescrpResultado = string.Empty;
            }
            catch (Exception ex)
            {
                if (cfg != null)
                { RfcDestinationManager.UnregisterDestinationConfiguration(cfg); }

                listResultado.codResultado = -1;
                listResultado.DescrpResultado = ex.Message.ToString();
                NetLogger.WriteLog(ELogLevel.ERROR, ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return listResultado;
        }

        public ResultadoMachCode ConsultarMatchCode(String pambiente, String perfilCatalogo)
        {
            DestinationConfig cfg = null;
            RfcRepository repo = null;
            RfcDestination dest = null;
            ResultadoMachCode resultadoMachCode = new ResultadoMachCode();

            try
            {
                cfg = new DestinationConfig();

                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                dest = RfcDestinationManager.GetDestination(pambiente);//"QA");
                //dest.Ping();

                //repo = dest.Repository;

                try
                {
                    dest.Ping();
                    repo = dest.Repository;
                }
                catch (Exception e1)
                {
                    if (cfg != null)
                    { RfcDestinationManager.UnregisterDestinationConfiguration(cfg); }

                    resultadoMachCode.codResultado = -1;
                    resultadoMachCode.DescrpResultado = WebConfigurationManager.AppSettings[Constantes.MSGSERRORREDSOCKS];
                    NetLogger.WriteLog(ELogLevel.ERROR, e1.Message + Environment.NewLine + e1.StackTrace);
                    return resultadoMachCode;
                }

                IRfcFunction Function__ZRFC_CONSULTAMATCHCODE = repo.CreateFunction("ZRFC_GET_MATCH_CODE");

                //PARAMETROS IMPORT
                Function__ZRFC_CONSULTAMATCHCODE.SetValue("PI_RBNR", perfilCatalogo);


                Function__ZRFC_CONSULTAMATCHCODE.Invoke(dest);


                //OBTENIENDO TABLA ORIGEN
                IRfcTable tableOrigen = Function__ZRFC_CONSULTAMATCHCODE.GetTable("PT_ORIGEN");

                List<Origen> listOrigen = new List<Origen>();
                Origen origen;
                foreach (IRfcStructure row in tableOrigen)
                {
                    origen = new Origen();
                    origen.GrupoCodigo = row.GetValue("CODEGRUPPE").ToString();
                    origen.DescripcionGrupoCodigo = row.GetValue("KURZTEXT_1").ToString();
                    origen.Codigo = row.GetValue("CODE").ToString();
                    origen.TextoCodigo = row.GetValue("KURZTEXT_2").ToString();
                    origen.idResultado = listOrigen.Count;

                    listOrigen.Add(origen);
                }
                resultadoMachCode.listOrigen = listOrigen;

                //OBTENIENDO TABLA GRUPO PLANIFICACION
                IRfcTable tableGrupoPlanificacion = Function__ZRFC_CONSULTAMATCHCODE.GetTable("PT_GRUPO_PLANIFICACION");

                List<GrupoPlanificacion> listGrupoPlanificacion = new List<GrupoPlanificacion>();
                GrupoPlanificacion grupoPlanificacion;
                foreach (IRfcStructure row in tableGrupoPlanificacion)
                {
                    grupoPlanificacion = new GrupoPlanificacion();
                    grupoPlanificacion.centroPlanifiMante = row.GetValue("IWERK").ToString();
                    grupoPlanificacion.grupoPlanificacion = row.GetValue("INGRP").ToString();
                    grupoPlanificacion.nombreGrupoPlanificacion = row.GetValue("INNAM").ToString();
                    grupoPlanificacion.idResultado = listGrupoPlanificacion.Count;

                    listGrupoPlanificacion.Add(grupoPlanificacion);
                }
                resultadoMachCode.listGrupoPlanificacion = listGrupoPlanificacion;

                //OBTENIENDO TABLA PUESTO TRABAJO
                IRfcTable tablePuestoTrabajo = Function__ZRFC_CONSULTAMATCHCODE.GetTable("PT_PUESTO_TRABAJO");

                List<PuestoTrabajo> listpuestoTrabajo = new List<PuestoTrabajo>();
                PuestoTrabajo puestoTrabajo;
                foreach (IRfcStructure row in tablePuestoTrabajo)
                {
                    puestoTrabajo = new PuestoTrabajo();
                    puestoTrabajo.clasePuestoTrabajo = row.GetValue("VERWE").ToString();
                    puestoTrabajo.centro = row.GetValue("WERKS").ToString();
                    puestoTrabajo.puestoTrabajo = row.GetValue("ARBPL").ToString();
                    puestoTrabajo.denominacionBreve = row.GetValue("KTEXT_UP").ToString();
                    puestoTrabajo.idResultado = listpuestoTrabajo.Count;

                    listpuestoTrabajo.Add(puestoTrabajo);
                }
                resultadoMachCode.listPuestoTrabajo = listpuestoTrabajo;


                //OBTENIENDO TABLA PRIORIDAD
                IRfcTable tablePrioridad = Function__ZRFC_CONSULTAMATCHCODE.GetTable("PT_PRIORIDAD");

                List<Prioridad> listprioridad = new List<Prioridad>();
                Prioridad prioridad;
                foreach (IRfcStructure row in tablePrioridad)
                {
                    prioridad = new Prioridad();
                    prioridad.prioridad = row.GetValue("PRIOK").ToString();
                    prioridad.textoPrioridad = row.GetValue("PRIOKX").ToString();

                    listprioridad.Add(prioridad);
                }
                resultadoMachCode.listPrioridad = listprioridad;

                //OBTENIENDO TABLA REPERCUSION
                IRfcTable tableRepercusion = Function__ZRFC_CONSULTAMATCHCODE.GetTable("PT_REPERCUSION");

                List<Repercusion> listrepercusion = new List<Repercusion>();
                Repercusion repercusion;
                foreach (IRfcStructure row in tableRepercusion)
                {
                    repercusion = new Repercusion();
                    repercusion.repercusion = row.GetValue("AUSWK").ToString();
                    repercusion.textoRepercusion = row.GetValue("AUWKT").ToString();

                    repercusion.repercusionTextoConcat = string.Format("{0} - {1}", repercusion.repercusion, repercusion.textoRepercusion);

                    listrepercusion.Add(repercusion);
                }
                resultadoMachCode.listRepercusion = listrepercusion;

                //OBTENIENDO TABLA SINTOMAS
                IRfcTable tableSintomas = Function__ZRFC_CONSULTAMATCHCODE.GetTable("PT_SINTOMAS");

                List<Sintoma> listsintoma = new List<Sintoma>();
                Sintoma sintoma;
                foreach (IRfcStructure row in tableSintomas)
                {
                    sintoma = new Sintoma();
                    sintoma.grupoCodigo = row.GetValue("CODEGRUPPE").ToString();
                    sintoma.grupoCodigoDescripcion = row.GetValue("KURZTEXT_1").ToString();
                    sintoma.codigo = row.GetValue("CODE").ToString();
                    sintoma.codigoTexto = row.GetValue("KURZTEXT_2").ToString();
                    sintoma.idResultado = listsintoma.Count;

                    listsintoma.Add(sintoma);
                }
                resultadoMachCode.listSintoma = listsintoma;

                //OBTENIENDO TABLA CAUSAS
                IRfcTable tableCausas = Function__ZRFC_CONSULTAMATCHCODE.GetTable("PT_CAUSAS");

                List<Causa> listCausa = new List<Causa>();
                Causa causa;
                foreach (IRfcStructure row in tableCausas)
                {
                    causa = new Causa();
                    causa.grupoCodigo = row.GetValue("CODEGRUPPE").ToString();
                    causa.grupoCodigoDescripcion = row.GetValue("KURZTEXT_1").ToString();
                    causa.codigo = row.GetValue("CODE").ToString();
                    causa.codigoTexto = row.GetValue("KURZTEXT_2").ToString();
                    causa.idResultado = listCausa.Count;

                    listCausa.Add(causa);
                }
                resultadoMachCode.listCausa = listCausa;

                //OBTENIENDO TABLA PARTE OBJETO
                IRfcTable tableParteObjeto = Function__ZRFC_CONSULTAMATCHCODE.GetTable("PT_PARTE_OBJETO");

                List<ParteObjeto> listparteObjeto = new List<ParteObjeto>();
                ParteObjeto parteObjeto;
                foreach (IRfcStructure row in tableParteObjeto)
                {
                    parteObjeto = new ParteObjeto();
                    parteObjeto.grupoCodigo = row.GetValue("CODEGRUPPE").ToString();
                    parteObjeto.grupoCodigoDescripcion = row.GetValue("KURZTEXT_1").ToString();
                    parteObjeto.codigo = row.GetValue("CODE").ToString();
                    parteObjeto.codigoTexto = row.GetValue("KURZTEXT_2").ToString();
                    parteObjeto.idResultado = listparteObjeto.Count;

                    listparteObjeto.Add(parteObjeto);
                }
                resultadoMachCode.listParteObjeto = listparteObjeto;

                //OBTENIENDO TABLA CLASE AVISO
                IRfcTable tableClaseAviso = Function__ZRFC_CONSULTAMATCHCODE.GetTable("PT_CLASE_AVISO");

                List<ClaseAviso> listclaseAviso = new List<ClaseAviso>();
                ClaseAviso claseAviso;
                foreach (IRfcStructure row in tableClaseAviso)
                {
                    claseAviso = new ClaseAviso();
                    claseAviso.claseAviso = row.GetValue("QMART").ToString();
                    claseAviso.claseAvisoTexto = row.GetValue("QMARTX").ToString();
                    claseAviso.claseAvisoTextoConcat = string.Format("{0} - {1}", claseAviso.claseAviso, claseAviso.claseAvisoTexto);
                    listclaseAviso.Add(claseAviso);
                }
                resultadoMachCode.listClaseAviso = listclaseAviso;

                //Devolviendo todas las tablas
                RfcDestinationManager.UnregisterDestinationConfiguration(cfg);

                resultadoMachCode.codResultado = 0;
                resultadoMachCode.DescrpResultado = string.Empty;

            }
            catch (Exception ex)
            {
                if (cfg != null)
                { RfcDestinationManager.UnregisterDestinationConfiguration(cfg); }

                resultadoMachCode.codResultado = -1;
                resultadoMachCode.DescrpResultado = ex.Message.ToString();
                NetLogger.WriteLog(ELogLevel.ERROR, ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return resultadoMachCode;
        }

    }
}
