<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_CARCK.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CARCK" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    车品出库查询&nbsp;&nbsp;&nbsp;&nbsp;</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function ToOrder() {
            window.open("OM_CARCK_detail.aspx?FLAG=add&id=1");
        }

        $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var id = $(this).find("input[name*=ZDR_SJ]").val();
                window.open("OM_CARCK_detail.aspx?FLAG=read&id=" + id);
            });
        })
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">--%>
    <contenttemplate>
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="100%">
                            <tr>
                              <td>
                            <asp:RadioButtonList runat="server" ID="rblSPZT" AutoPostBack="true" OnSelectedIndexChanged="btnQuery_OnClick"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="审批中" Value="1"></asp:ListItem>
                                <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                                <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                             </td>
                                <td align="left">
                                    名称：
                                    <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    规格或参数：
                                    <asp:TextBox ID="txtModel" runat="server" Text=""></asp:TextBox>&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnQuery" runat="server" Text="查  询" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <table id="tab" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    <td>
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>名称</strong>
                                    </td>
                                    <td>
                                        <strong>规格或参数</strong>
                                    </td>
                                    <td>
                                        <strong>数量</strong>
                                    </td>
                                    <td>
                                        <strong>单位</strong>
                                    </td>
                                        <td>
                                        <strong>单价</strong>
                                    </td>
                                         <td>
                                        <strong>总价</strong>
                                    </td>
                                      <td>
                                        <strong>时间</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td>
                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblmc" runat="server" Text='<%#Eval("SP_MC")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblgg" runat="server" Text='<%#Eval("SP_GG")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblsl" runat="server" Text='<%#Eval("SP_SL")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbdanwei" runat="server" Text='<%#Eval("SP_DANWEI")%>'></asp:Label>
                                    </td>
                                                                       <td>
                                        <asp:Label ID="lbldj" runat="server" Text='<%#Eval("SP_DJ")%>'></asp:Label>
                                    </td>
                                                                       <td>
                                        <asp:Label ID="lblzj" runat="server" Text='<%#Eval("SP_ZJ")%>'></asp:Label>
                                    </td>
                                                                       <td>
                                        <asp:Label ID="lblsj" runat="server"  Text='<%#Eval("ZDR_SJ")%>'></asp:Label>
                                        <input type="hidden" runat="server" id="ZDR_SJ" name="ZDR_SJ"  value='<%#Eval("ZDR_SJ")%>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        没有相关物料信息!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </contenttemplate>
    <%--        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnQuery" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
        </Triggers>--%>
    <%--    </asp:UpdatePanel>--%>
</asp:Content>