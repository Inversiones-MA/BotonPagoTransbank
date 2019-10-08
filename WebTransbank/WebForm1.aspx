<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebTransbank.WebForm1" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">

        function click() {
            ASPxLabel1.SetText(null);
            ASPxCallback1.PerformCallback();
        }

        function endCall(s, e) {
            ASPxLabel1.SetText(e.result);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxButton ID="ASPxButton1" ClientInstanceName="ASPxButton1" runat="server" Text="ASPxButton" AutoPostBack="false">
                <ClientSideEvents Click="click" />
            </dx:ASPxButton>

            <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="ASPxCallback1" OnCallback="ASPxCallback1_Callback">
                <ClientSideEvents CallbackComplete="endCall" />
            </dx:ASPxCallback>

            <dx:ASPxLabel ID="ASPxLabel1" ClientInstanceName="ASPxLabel1" runat="server"></dx:ASPxLabel>

        </div>
    </form>
</body>
</html>
