using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Collections;
namespace SAPAvisosPM.Helper
{
  public  class Herramienta
    {
      public static void CargarDropDownListSeleccione(DropDownList ddl, IList listaDDL, string campoValor, string CampoDescripcion)
      {
          ddl.DataSource = listaDDL;
          ddl.DataTextField = CampoDescripcion;
          ddl.DataValueField = campoValor;
          ddl.DataBind();
          ddl.Items.Insert(0, new ListItem(WebConfigurationManager.AppSettings["ItemSeleccione"].ToString(), "-1"));
      }

      public static void CargarDropDownListItemNinguno(DropDownList ddl, IList listaDDL, string campoValor, string CampoDescripcion)
      {
          ddl.DataSource = listaDDL;
          ddl.DataTextField = CampoDescripcion;
          ddl.DataValueField = campoValor;
          ddl.DataBind();
          ddl.Items.Insert(0, new ListItem(WebConfigurationManager.AppSettings["ItemNinguno"].ToString(), "-1"));
      }
     

    }
}
