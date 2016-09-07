<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteList.aspx.cs" Inherits="DRP.WEB.Module.T.RouteList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>线路模板</title>
    <link href="../../Scripts/Plugin/zTree/zTreeStyle.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/zTree/jquery.ztree.core-3.5.js"></script>
    <script src="Script/RouteList.js" type="text/javascript"></script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
        <div data-options="region:'west',collapsed:false,collapsible:false" iconcls='icon-dir' title="目的地" style="width: 150px; padding: 10px;">
            <ul style="clear: both;" id="treeData" class="ztree">
            </ul>
        </div>
        <div data-options="region:'center'" style="border: 0px;">
            <div id="tabs" class="easyui-tabs">
                <div title="报价单" iconcls="icon-search">
                    <div id="toolbar">
                        线路名称：<asp:TextBox ID="txtRouteName" Width="100" ClientIDMode="Static" runat="server"></asp:TextBox>
                        编号：<asp:TextBox ID="txtRouteNo" ClientIDMode="Static" Width="60" runat="server"></asp:TextBox>

                        人均价格：<asp:DropDownList ID="AvgPrice" runat="server" ClientIDMode="Static">
                            <asp:ListItem Text="所有" Value=""></asp:ListItem>
                            <asp:ListItem Text="150以下" Value="0,150"></asp:ListItem>
                            <asp:ListItem Text="150-200" Value="150,200"></asp:ListItem>
                            <asp:ListItem Text="200-300" Value="200,300"></asp:ListItem>
                            <asp:ListItem Text="300-400" Value="300,400"></asp:ListItem>
                            <asp:ListItem Text="400-500" Value="400,500"></asp:ListItem>
                            <asp:ListItem Text="500-800" Value="500,800"></asp:ListItem>
                            <asp:ListItem Text="800-1200" Value="800,1200"></asp:ListItem>
                            <asp:ListItem Text="1200-1600" Value="1200,1600"></asp:ListItem>
                            <asp:ListItem Text="1600-2000" Value="1600,2000"></asp:ListItem>
                            <asp:ListItem Text="2000-2500" Value="2000,2500"></asp:ListItem>
                            <asp:ListItem Text="2500-3000" Value="2500,3000"></asp:ListItem>
                            <asp:ListItem Text="3000-5000" Value="3000,5000"></asp:ListItem>
                            <asp:ListItem Text="5000以上" Value="5000,-1"></asp:ListItem>
                        </asp:DropDownList>
                        行程天数：<asp:DropDownList ID="Days" runat="server" ClientIDMode="Static">
                            <asp:ListItem Text="所有" Value=""></asp:ListItem>
                            <asp:ListItem Text="1天" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2天" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3天" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4天" Value="4"></asp:ListItem>
                            <asp:ListItem Text="5天" Value="5"></asp:ListItem>
                            <asp:ListItem Text="6天" Value="6"></asp:ListItem>
                            <asp:ListItem Text="7天" Value="7"></asp:ListItem>
                            <asp:ListItem Text="8天" Value="8"></asp:ListItem>
                            <asp:ListItem Text="9天" Value="9"></asp:ListItem>
                            <asp:ListItem Text="10天" Value="10"></asp:ListItem>
                            <asp:ListItem Text="11天" Value="11"></asp:ListItem>
                            <asp:ListItem Text="12天" Value="12"></asp:ListItem>
                            <asp:ListItem Text="12天以上" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                        人数：<asp:DropDownList ID="VisitorNum" runat="server" ClientIDMode="Static">
                            <asp:ListItem Text="所有" Value=""></asp:ListItem>
                            <asp:ListItem Text="5人以内" Value="1,5"></asp:ListItem>
                            <asp:ListItem Text="5-10人" Value="5,10"></asp:ListItem>
                            <asp:ListItem Text="10-15人" Value="10,15"></asp:ListItem>
                            <asp:ListItem Text="15-20人" Value="15,20"></asp:ListItem>
                            <asp:ListItem Text="20-25人" Value="20,25"></asp:ListItem>
                            <asp:ListItem Text="25-30人" Value="25,30"></asp:ListItem>
                            <asp:ListItem Text="30-35人" Value="30,35"></asp:ListItem>
                            <asp:ListItem Text="35-40人" Value="35,40"></asp:ListItem>
                            <asp:ListItem Text="40-45人" Value="40,45"></asp:ListItem>
                            <asp:ListItem Text="45-50人" Value="45,50"></asp:ListItem>
                            <asp:ListItem Text="50人以上" Value="50,-1"></asp:ListItem>
                        </asp:DropDownList>
                        <div style="padding-top: 5px; padding-left: 5px;">
                            <a href="javascript:;" class="easyui-linkbutton" id="btnQuery" iconcls="icon-search">查询</a>
                            <a href="javascript:;" class="easyui-linkbutton" id="btnAdd" iconcls="icon-add">新增线路模板</a> 
                        </div>
                    </div>
                    <table id="tblData"></table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
