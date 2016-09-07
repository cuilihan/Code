<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTASetting.aspx.cs" Inherits="DRP.WEB.Module.Sys.OTASetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        $(function () {
            c.initData();
        });
    </script>
    <script type="text/javascript" src="Script/OTASetting.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="tabs" class="easyui-tabs">
            <div title="OTA接口设置" iconcls="icon-search" style="padding: 6px;">
                <div id="toolbar">
                    <table cellspacing="1" cellpadding="1" class="tblEdit" border="0">
                        <tr>
                            <td class="datalabel" style="width: 10% !important;">平台：
                            </td>
                            <td style="width: 100px;">
                                <asp:DropDownList ID="ddlSupplier" runat="server" ClientIDMode="Static" AppendDataBoundItems="true">
                                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                                </asp:DropDownList></td>
                            <td><a class="easyui-linkbutton" id="btnAdd" iconcls="icon-add">新增</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a></td>
                        </tr>
                    </table>
                </div>
                <table id="tblData">
                </table>
            </div>
        </div>
    </form>
</body>
</html>
