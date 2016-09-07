<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaTreeTable.aspx.cs" Inherits="DRP.WEB.Module.Sys.AreaTreeTable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../../Scripts/Plugin/lhgdialog/lhgdialog.min.js?skin=iblue" type="text/javascript"></script>
    <script src="Script/AreaTree.js?v=1.2"></script>
    <script type="text/javascript">
        $(function () {
            t.init();
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <div class="toolbar" style="padding: 5px 0px 3px 10px; border-bottom: 1px solid #95B8E7;">
                平台名称： 
                <asp:DropDownList ID="ddlOTA" runat="server" ClientIDMode="Static"></asp:DropDownList>
                <a class="easyui-linkbutton" href="javascript:;" id="btnOTA">初始化数据</a>
            </div>
            <table id="tblData" cellspacing="0" cellpadding="0">
            </table>
        </div>
    </form>
</body>
</html>
