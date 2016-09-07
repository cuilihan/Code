<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicInfo.aspx.cs" Inherits="DRP.WEB.Module.Glo.BasicInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>参数定义</title>
    <script src="Script/BasicInfo.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            <a href="javascript:;" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增参数</a>
              <a href="javascript:;" id="btnDelete" class="easyui-linkbutton" iconcls="icon-remove">删除参数</a>
        </div>
     <table id="tblData"></table>
    </form>
</body>
</html>
