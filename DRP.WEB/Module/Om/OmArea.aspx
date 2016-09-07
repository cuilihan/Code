<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OmArea.aspx.cs" Inherits="DRP.WEB.Module.Om.OmArea" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>行政区域管理</title>
    <script src="Script/OmArea.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            <a href="javascript:;" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增区域</a>
              <a href="javascript:;" id="btnCollapse" class="easyui-linkbutton" >折叠全部</a>
            <a href="javascript:;" id="btnExpand" class="easyui-linkbutton" >展开全部</a>
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
