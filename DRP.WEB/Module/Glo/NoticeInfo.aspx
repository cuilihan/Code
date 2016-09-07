<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeBehind="NoticeInfo.aspx.cs" Inherits="DRP.WEB.Module.Glo.NoticeInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>通知公告</title>
    <style type="text/css">
        .userlist {
        margin: 20px 0px; padding-top: 5px; border-top: 1px dotted #ccc; line-height: 25px; text-align:left;
        }
        .userlist span {
            display: inline-block;
            margin-right: 8px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="formedit">
        <div style="text-align: center; width: 998px; margin: 0 auto;">
            <div class="tblwrap" style="padding: 50px 40px; min-height: 550px; font-size: 14px;">
                <div style="text-align: center; font-weight: bold; font-size: 20px;">
                    <asp:Literal ID="Subject" runat="server"></asp:Literal>
                </div>
                <div style="text-align: right; margin: 15px 0px 30px 0px; color: #ccc;">
                    发布人：<asp:Literal ID="CreateUserName" runat="server"></asp:Literal>
                    &nbsp;&nbsp;发布日期：<asp:Literal ID="CreateDate" runat="server"></asp:Literal>
                </div>
                <div style="text-align: left; line-height: 26px;">
                    <asp:Literal ID="nContext" runat="server"></asp:Literal>
                </div>
                <div class="userlist">
                    已阅读人： 
                    <asp:Literal ID="lblUserList" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
