<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectOTAUser.aspx.cs" Inherits="DRP.WEB.Module.Sys.Service.SelectOTAUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="Script/SelectOTAUser.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbar" style="padding: 5px;">
            <table class="tblEdit" cellpadding="1" cellspacing="1" border="0">
                <tr>
                    <td class="datalabel" style="width: 100px!important;">平台：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSupplier" runat="server" ClientIDMode="Static">
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                        <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>&nbsp;&nbsp;<a href="javascript:;" id="btnOk" class="easyui-linkbutton" iconcls="icon-ok">确定</a>
                    </td>
                </tr>
            </table>
        </div>
        <table id="tblData">
        </table>
    </form>
</body>
</html>
