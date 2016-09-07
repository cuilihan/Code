<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysInfoSet.aspx.cs" Inherits="DRP.WEB.Module.mSite.LogoSetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../Scripts/Plugin/Uploadify/uploadify.css" rel="stylesheet" />
    <script src="Script/SysInfoSet.js?v=1.1" type="text/javascript"></script>
    <title>基础资料维护</title>
</head>
<body>
    <form id="form1" runat="server">
        <script src="../../Scripts/Plugin/Uploadify/jquery.uploadify-3.1.min.js?r=<%=(new Random()).Next(0, 999).ToString() %>"></script>
        <div title="基本信息维护" iconcls='icon-ok' data-options="region:'center',border:true" style="padding: 10px;">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">旅行社Logo：</td>
                    <td colspan="3">
                        <div>
                            <img id="imgLogo" runat="server" src="/" />
                        </div>
                        <div>
                            <input type="file" name="uploadify" id="uploadify" />
                            <asp:HiddenField ID="LogoUrl" ClientIDMode="Static" runat="server" />
                        </div>
                    </td>

                </tr>
                <tr>

                    <td colspan="4">注：适用于出团书，订单详情头部，上传大小限制2MB，如果为空将不显示
                    </td>
                </tr>
            </table>
            <div style="margin: 20px 0px; text-align: center;">
                <a class="easyui-linkbutton" runat="server" iconcls="icon-save" id="btnSave">保存设置</a>
                <a class="easyui-linkbutton" runat="server" iconcls="icon-remove" id="btnDel">清空</a>
            </div>
        </div>
    </form>
</body>
</html>
