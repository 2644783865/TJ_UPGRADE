<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_CarKuCun.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CarKuCun" Title="车辆库存" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    车品库存</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
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
                                <td align="left">
                                    名称：
                                    <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    规格或参数：
                                    <asp:TextBox ID="txtModel" runat="server" Text=""></asp:TextBox>&nbsp; 
                                    <asp:Button ID="btnQuery" runat="server" Text="查  询" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnTransf" runat="server" Text="出库" BackColor="LightGreen" OnClick="btnTransf_click" />
                                </td>
                                <%--<td align="center">
                                    <asp:Button ID="btnPush" runat="server" Text="下推" OnClick="btnPush_OnClick" OnClientClick="return PushConfirm()"
                                        Visible="false" />
                                </td>--%>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
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
                                        <asp:Label ID="lblbh" runat="server" Text='<%#Eval("KC_MC")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("KC_GG")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("KC_SL")%>'></asp:Label>
                                    </td>
                                     <td>
                                        <asp:Label ID="lbdanwei" runat="server" Text='<%#Eval("KC_DANWEI")%>'></asp:Label>
                                    </td>
                                      <td>
                                        <asp:Label ID="lbdanjia" runat="server" Text='<%#Eval("KC_DJ")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="zongjia" runat="server" Text='<%#Eval("KC_ZJ")%>'></asp:Label>
                                    </td>
                                       <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("KC_SJ")%>'></asp:Label>
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
