<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_Bgyp_DingE.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_Bgyp_DingE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    办公用品定额管理
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
                        <td style="width: 25%;">
                            <strong>时间：</strong>
                            <asp:DropDownList ID="dplYear" runat="server">
                            </asp:DropDownList>
                            &nbsp;年&nbsp;
                            <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplMoth_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;月&nbsp;
                        </td>
                        <td align="right" width="100px">
                            部门：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedIndexChanged="dplMoth_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 58%;">
                          
                            &nbsp; &nbsp;   <asp:Button ID="btnCreat" runat="server" Text="添加该月额度" OnClick="btnCreat_Click"></asp:Button>
                            &nbsp; &nbsp; <asp:Button ID="btnEditDE" runat="server" Text="修改基础定额" OnClick="btnEditDE_Click"></asp:Button>
                               &nbsp; &nbsp; <asp:Button ID="btnDaochu" runat="server" Text="导 出" OnClick="btnDaochu_OnClick" />
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
                                    年
                                </td>
                                <td>
                                    月
                                </td>
                                <td>
                                    部门
                                </td>
                                <td>
                                    额度
                                </td>
                                <td>
                                    实领金额
                                </td>
                                <td>
                                    剩余额度
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
                                <td id="Td1" runat="server" align="center">
                                    <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                </td>
                                <td id="Td2" runat="server" align="center">
                                    <asp:Label ID="lblMonth" runat="server" Text='<%#Eval("Month")%>'></asp:Label>
                                </td>
                                <td id="Td3" runat="server" align="center">
                                    <asp:Label ID="lblPart" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                </td>
                                <td id="Td4" runat="server" align="center">
                                    <asp:Label ID="Max" runat="server" Text='<%#Eval("MONTH_MAX")%>'></asp:Label>
                                </td>
                                <td id="Td12" runat="server" align="center">
                                    <asp:Label ID="lblSLJE" runat="server" Text='<%#Eval("SLJE")%>'></asp:Label>
                                </td>
                                <td id="Td5" runat="server" align="center">
                                    <asp:Label ID="lblCE" runat="server" Text='<%#Eval("CE")%>'></asp:Label>
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
                        <td align="center"> <asp:Label runat="server" ID="lblCE"></asp:Label>
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
