<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTAUser.aspx.cs" Inherits="DRP.WEB.Module.Sys.OTAUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="Script/OTAUser.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="tabs" class="easyui-tabs">
            <div title="用户管理" iconcls="icon-reload">
                <div id="toolbar" style="padding: 5px;">
                    <table border="0" cellpadding="1" cellspacing="1" class="tblInfo">
                        <tr>
                            <td class="rowlabel" style="width: 120px;">用户姓名或部门名称：
                            </td>
                            <td style="width: 200px;">
                               <input  id="txtKey" class="textbox" style="height:26px;" type="text" />
                            </td>
                            <td style="text-align: left;">
                                <a class="easyui-linkbutton" id="btnQuery" iconcls="icon-search">查询</a>
                            </td>
                            <td>平台名称： 
                <asp:DropDownList ID="ddlOTA" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                <a class="easyui-linkbutton" href="javascript:;" id="btnOTA" onclick="initOTAData()">初始化数据</a>
                            </td>
                        </tr>
                    </table>
                </div>
                <table id="tblData" cellspacing="0" cellpadding="0">
                </table>
            </div>
        </div>
    </form>
</body>
</html>
