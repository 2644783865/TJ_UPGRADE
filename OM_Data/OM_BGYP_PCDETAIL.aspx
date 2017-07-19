<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_BGYP_PCDETAIL.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_BGYP_PCDETAIL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    办公用品采购明细查询
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper">
        <div class="box-outer">
            <div>
                <table style="width: 100%;">
                    <tr>
                    </tr>
                    <tr>
                        <td>
                            全选/取消<input id="Checkbox2" type="checkbox" />
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            物料编码<asp:TextBox runat="server" ID="txtWLCode" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            订单编号<asp:TextBox runat="server" ID="txtOrder" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            名称<asp:TextBox runat="server" ID="txtName" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            规格型号<asp:TextBox runat="server" ID="txtGuige" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            是否入库<asp:DropDownList runat="server" ID="ddlIsRK">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                            <asp:ListItem Text="是" Value="1"></asp:ListItem>
                             <asp:ListItem Text="否" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="查 询" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptProNumCost" runat="server">
                        <HeaderTemplate>
                            <tr align="center" style="background-color: #B9D3EE;">
                                <td>
                                    序号
                                </td>
                                <td>
                                    物料编码
                                </td>
                                <td>
                                    订单编号
                                </td>
                                <td>
                                    名称
                                </td>
                                <td>
                                    规格型号
                                </td>
                                <td>
                                    单位
                                </td>
                                <td>
                                    数量
                                </td>
                                <td>
                                    单价
                                </td>
                                <td>
                                    金额
                                </td>
                                <td>
                                    审核状态
                                </td>
                                <td>
                                    是否入库
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);">
                                <td>
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                        Onclick="checkme(this)" />
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                </td>
                                <td>
                                    <%#Eval("WLBM")%>
                                </td>
                                <td>
                                    <%#Eval("CODE")%>
                                </td>
                                <td>
                                    <%#Eval("WLNAME")%>
                                </td>
                                <td>
                                    <%#Eval("WLMODEL")%>
                                </td>
                                <td>
                                    <%#Eval("WLUNIT")%>
                                </td>
                                <td>
                                    <%#Eval("WLNUM")%>
                                </td>
                                <td>
                                    <%#Eval("WLPRICE")%>
                                </td>
                                <td>
                                    <%#Eval("WLJE")%>
                                </td>
                                <td>
                                    <%#Eval("STATE").ToString().Contains("2")?"已通过":"未通过"%>
                                </td>
                                <td>
                                    <%#Eval("STATE_rk").ToString().Contains("1")?"已入库":"未入库"%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr runat="server" id="tr_foot">
                        <td runat="server" id="foot" colspan="3" align="right">
                            <strong>合计：</strong>
                        </td>
                        <td>
                        </td>
                        <td align="center">
                            <asp:Label runat="server" ID="lblEDU"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label runat="server" ID="lblSLTotal"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblCE"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录!<br />
                    <br />
                </asp:Panel>
            </div>
        </div>
    </div>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblTip" runat="server" Text="提示:" Visible="false" Font-Bold="true"></asp:Label>
                <asp:Image ID="imgloading" runat="server" Visible="false" />
                <asp:Label ID="lblTip2" runat="server" Visible="false" Text="数据处理中，请稍后..."></asp:Label>
                <asp:Label ID="LabelDate" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
