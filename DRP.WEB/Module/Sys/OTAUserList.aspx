<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTAUserList.aspx.cs" Inherits="DRP.WEB.Module.Sys.OTAUserList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="Script/OTAUserList.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbar" style="padding: 5px;">
            <table border="0" cellpadding="1" cellspacing="1" class="tblEdit">
                <tr>
                    <td style="text-align: left;">
                        <a class="easyui-linkbutton" id="btnAdd" iconcls="icon-add">新增</a>
                    </td>
                </tr>
            </table>
        </div>
        <table id="tblData" cellspacing="0" cellpadding="0">
        </table>
    </form>
</body>
</html>
