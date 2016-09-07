<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="DRP.WeChat.Module.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <h1 class="sub-header">统计报表</h1>

        <div>
            <div class="col-xs-6 col-sm-3 placeholder">
                <a class="btn btn-lg btn-outline" role="button" href="#">订单来源统计</a>
            </div>
            <div class="col-xs-6 col-sm-3 placeholder">

                <a class="btn btn-lg btn-outline" role="button" href="#">员工统计报表</a>
            </div>
            <div class="col-xs-6 col-sm-3 placeholder">

                <a class="btn btn-lg btn-outline" role="button" href="#">收入统计报表</a>
            </div>
            <div class="col-xs-6 col-sm-3 placeholder">
                <a class="btn btn-lg btn-outline" role="button" href="#">支出统计报表</a>
            </div>
        </div>


    </div>
</asp:Content>
