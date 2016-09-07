<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QRCode.aspx.cs" Inherits="DRP.WEB.Module.Om.QRCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center; color:red; margin-bottom:10px; margin-top:10px;">
            机构二维码
        </div>
        <div style="text-align:center;">
            <asp:Image runat="server" ID="QRCodeImg" Visible="false" />
        </div>
    </form>
</body>
</html>
