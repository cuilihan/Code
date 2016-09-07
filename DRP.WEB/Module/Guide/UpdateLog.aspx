<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateLog.aspx.cs" Inherits="DRP.WEB.Module.Guide.UpdateLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统更新日志</title>
    <script src="Script/UpdateLog.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-panel" title="更新日志" iconcls="icon-ok" style="padding:10px;">
            <div class="history" id="TraceLog">
            </div>
        </div>
    </form>
</body>
</html>
