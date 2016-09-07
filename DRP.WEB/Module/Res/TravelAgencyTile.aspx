<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravelAgencyTile.aspx.cs" Inherits="DRP.WEB.Module.Res.TravelAgencyTile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>综合供应商查询</title>
    <script src="Script/TravalAgencyTile.js" type="text/javascript"></script>
    <style type="text/css">
        #tblData a
        {
            display: inline-block;
            margin-right: 20px;
            line-height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="供应商查询" iconcls="icon-search" style="padding: 5px 5px 0px 5px;">
                <div id="wraper">
                    <div class="tabMenu">
                        <ul id="tabMenu">
                            <asp:Literal ID="lblRouteType" runat="server"></asp:Literal>
                        </ul>
                        <span style="display: inline-block; padding: 3px; position: absolute; right: 5px;"><a class="icon_list" href='TravelAgencyQry.aspx'>切换至列表视图</a></span>
                    </div>
                    <div style="height: 2px; margin-bottom: 5px;">
                    </div>
                    <table class="tblInfo" id="tblData">
                    </table>
                    <div style="height: 2px; margin-bottom: 5px;">
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
