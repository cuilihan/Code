<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BirthDay.aspx.cs" Inherits="DRP.WEB.Module.Crm.BirthDay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        #toolbar a {
            display: inline-block;
            padding: 4px 6px;
            margin: 0px 2px;
            border: 1px solid #a3a1a1;
            cursor: pointer;
        }

        .on {
            background-color: #ffd800;
            border: 1px solid #4cff00;
            color: #fff;
        }
    </style>
    <script src="Script/CustomerBirthDay.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="客户生日提醒" iconcls="icon-search">
                <div id="toolbar"> 
                    <a v="c">本月过生日的客户</a>
                    <a v="n">下月过生日的客户</a> 
                    <a v="d" id="day" class="on">本日过生日的客户</a>
                    <asp:LinkButton ID="btnSMS" ClientIDMode="Static" CssClass="easyui-linkbutton" icon="icon-message" runat="server" Text="一键群发信息"></asp:LinkButton>
                </div> 
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
