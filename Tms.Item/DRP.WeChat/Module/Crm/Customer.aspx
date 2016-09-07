<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="DRP.WeChat.Module.Customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/Customer.js"></script>
    <script type="text/javascript">
        $(function () {
            c.init();
        })
    </script>
    <link href="../../UI/index.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <h2 class="sub-header">客户信息:总计<span id="total">[0]</span></h2>
        <div class="table-responsive">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th style="width: 5%">编号</th>
                        <th style="width: 10%">姓名</th>
                        <th style="width: 5%">性别</th>
                        <th style="width: 10%">手机号</th>
                        <th style="width: 8%">客户类型</th>
                        <th>公司名称</th>
                        <th style="width: 8%">消费次数</th>
                        <th  style="width: 8%; text-align:right">消费金额</th>
                        <th style="width: 8%">沟通次数</th>
                    </tr>
                </thead>
                <tbody id="tblData">
                </tbody>

            </table>
        </div>
        <input type="hidden" id="pIndex" />

        <div class="infobox-green">
            <a href="javascript:;" id="moreData" style="line-height: 43px; text-align: center; display: block; color: #fff !important; width: 100%; height: 100%; font-size: 20px">加载更多</a>
        </div>

    </div>
</asp:Content>
