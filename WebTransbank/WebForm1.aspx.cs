using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Webpay.Transbank.Library;
using Webpay.Transbank.Library.Wsdl.Normal;

namespace WebTransbank
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ASPxCallback1_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            string baseurl = string.Empty;

            string seguridad = "http://";

            baseurl = string.Format("{0}{1}", seguridad, HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString());

            string urlFinal = string.Format("{0}{1}{2}", baseurl, "/WebForm1.aspx", "?accion=anulado");

            string urlReturn = string.Format("{0}{1}{2}", baseurl, "/WebForm1.aspx", "?accion=pagado");
            
            var res = new Transbank() { }.Pagar();

            e.Result = res == null ? "no hay respuesta de transbank" : res.token + " " + res.url;
        }
    }


    public class Transbank
    {
        private Webpay.Transbank.Library.Webpay WebPay;

        public Transbank()
        { }

        /// <summary>
        /// Método principal
        /// </summary>
        /// <returns></returns>
        public wsInitTransactionOutput Pagar()
        {
            wsInitTransactionOutput resultado = null;
            try
            {
                var transaccion = new Transaccion
                {
                    Amount = 10,
                    BuyOrder = new Random().Next(100000, 999999999).ToString(),
                    SessionId = "19090000",
                    UrlReturn = @"http://localhost:63065/WebForm1.aspx",
                    UrlFinal = @"http://localhost:63065/WebForm1.aspx",
                };

                string baseurl = string.Empty;
                string seguridad = "http://";
                baseurl = string.Format("{0}{1}", seguridad, HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString());
                string pathBase = HttpContext.Current.Server.MapPath(".");

                Configuration ConfiguracionWebPay = new Configuration
                {
                    Environment = "PRODUCCION",
                    CommerceCode = "597033290136",
                    PublicCert = string.Format("{0}{1}", pathBase, @"\Models\certificados\597033290136\serverTBK.pem"),
                    WebpayCert = string.Format("{0}{1}", pathBase, @"\Models\certificados\597033290136\597033290136.pfx"),
                    Password = "multiweb",
                    StoreCodes = null
                };

                WebPay = new Webpay.Transbank.Library.Webpay(ConfiguracionWebPay);

                resultado = WebPay.getNormalTransaction().initTransaction(transaccion.Amount, transaccion.BuyOrder, transaccion.SessionId, transaccion.UrlReturn, transaccion.UrlFinal);
                
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Write(System.Diagnostics.TraceLevel.Error, "ERROR", ex.StackTrace);

                string error = ex.Message;

                return resultado;
            }
        }
    }

    public class Transaccion
    {
        public decimal Amount { get; set; }

        public string BuyOrder { get; set; }

        public string SessionId { get; set; }

        public string UrlReturn { get; set; }

        public string UrlFinal { get; set; }

        public Transaccion()
        {
            Init();
        }

        private void Init()
        {
            Amount = 0;
            BuyOrder = string.Empty;
            SessionId = string.Empty;
            UrlReturn = string.Empty;
            UrlFinal = string.Empty;
        }
    }
}