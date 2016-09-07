<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PushNotice.aspx.cs" Inherits="DRP.WEB.Module.Om.PushNotice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Script/OmPushNotice.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="全局推送消息管理" runat="server" id="orderTitle" iconcls="icon-reload">
                <div id="toolbar"> 
                    <a href="javascript:;" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">发布消息</a>
                    <span style="color:red; float:right;">备注：每次只能推送一条消息，以有效期内的最后一条为主</span>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
