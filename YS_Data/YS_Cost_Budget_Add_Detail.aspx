<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="YS_Cost_Budget_Add_Detail.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Cost_Budget_Add_Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    预算详情
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style>
        input
        {
            height: 17px;
            width:120px;
        }
    </style>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <%--头部--%>
    <table width="100%">
        <tr>
            <td>
                合同号：<asp:Label ID="lb_YS_CONTRACT_NO" runat="server"></asp:Label>
            </td>
            <td>
                项目名称：<asp:Label ID="lb_CM_PROJ" runat="server"></asp:Label>
            </td>
            <td>
                任务号：<asp:Label ID="lb_YS_TSA_ID" runat="server"></asp:Label>
            </td>
            <td align="right">
                预算备注：
            </td>
            <td>
                <asp:TextBox ID="txt_YS_NOTE" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="5" align="right" style="padding-top: 5px">
                <asp:Button ID="Button1" runat="server" Text="保存内容" />
                <asp:Button ID="Button2" runat="server" Text="下推至部门反馈" />
                <asp:Button ID="Button3" runat="server" Text="提交反馈" />
                <asp:Button ID="Button4" runat="server" Text="提交至领导审核" />
            </td>
        </tr>
    </table>
    <asp:TabContainer runat="server" ID="tab_Detail" Width="100%" ActiveTabIndex="0">
        <%--黑色金属--%>
        <asp:TabPanel runat="server" HeaderText="黑色金属" ID="TabPanel1">
            <ContentTemplate>
                <div style="overflow: auto; height: 320px">
                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="nowrap toptable grid"
                        border="1" frame="border">
                        <asp:Repeater ID="rpt_YS_FERROUS_METAL" runat="server">
                            <HeaderTemplate>
                                <tr class="tableTitle" style="background-color: #B9D3EE">
                                    <th>
                                        序号
                                    </th>
                                    <th>
                                        物料代码（01.07）
                                    </th>
                                    <th>
                                        物料名称
                                    </th>
                                    <th>
                                        数量（吨）
                                    </th>
                                    <th>
                                        预算单价（元/吨）
                                    </th>
                                    <th>
                                        预算总价（元）
                                    </th>
                                    <th>
                                        反馈单价（元/吨）
                                    </th>
                                    <th>
                                        反馈总价（元）
                                    </th>
                                    <th>
                                    反馈说明
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label1" runat="server" Text="<%#Container.ItemIndex+1%>"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_YS_CODE" runat="server" Text='<% #Eval("YS_CODE") %>'></asp:Label>
                                    </td>
                                    <td align="center">
                                    <asp:Label ID="lb_YS_NAME" runat="server" Text='<% #Eval("YS_NAME") %>'></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lb_YS_Union_Amount" runat="server" Text='<%# Eval("YS_Union_Amount") %>'></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lb_YS_Average_Price" runat="server" Text='<%# Eval("YS_Average_Price","{0:f2}")%>'></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lb_YS_MONEY" runat="server" Text='<%# GetProduct(Eval("YS_Average_Price").ToString(),Eval("YS_Union_Amount").ToString())%>'></asp:Label>
                                    
                                    </td>
                                    <td align="center">
                                        <asp:TextBox Style="text-align: right" runat="server" ID="TextBox2" onkeypress="InputNumberOnly()" Text='<%# Eval("YS_Average_Price_FB") %>'></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox Style="text-align: right" runat="server" ID="TextBox3"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                    <asp:TextBox Style="text-align: right" runat="server" ID="TextBox1"></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--外购件--%>
        <asp:TabPanel runat="server" HeaderText="外 购 件" ID="TabPanel2">
            <ContentTemplate>
                <div style="overflow: auto">
                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class=" nowrap toptable grid"
                        border="1" frame="border">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--加工件--%>
        <asp:TabPanel runat="server" HeaderText="加 工 件" ID="TabPanel3">
        </asp:TabPanel>
        <%--油漆涂料--%>
        <asp:TabPanel runat="server" HeaderText="油漆涂料" ID="TabPanel4">
        </asp:TabPanel>
        <%--电气电料--%>
        <asp:TabPanel runat="server" HeaderText="电气电料" ID="TabPanel5">
        </asp:TabPanel>
        <%--其他材料--%>
        <asp:TabPanel runat="server" HeaderText="其他材料" ID="TabPanel6">
        </asp:TabPanel>
        <%--直接人工--%>
        <asp:TabPanel runat="server" HeaderText="直接人工" ID="TabPanel7">
        </asp:TabPanel>
        <%--厂内分包--%>
        <asp:TabPanel runat="server" HeaderText="厂内分包" ID="TabPanel8">
        </asp:TabPanel>
        <%--生产外协--%>
        <asp:TabPanel runat="server" HeaderText="生产外协" ID="TabPanel9">
        </asp:TabPanel>
        <%--运费--%>
        <asp:TabPanel runat="server" HeaderText="运  费" ID="TabPanel10">
        </asp:TabPanel>
        <%--审核详情--%>
        <asp:TabPanel runat="server" HeaderText="审核详情" ID="TabPanel11">
        </asp:TabPanel>
    </asp:TabContainer>

    <script>
        function InputNumberOnly() {
            if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 46) { event.returnValue = false; alert('请输入数字 ！'); } else { event.returnValue = true; }
        }
    </script>

</asp:Content>
