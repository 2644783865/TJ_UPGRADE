<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_KQTJdetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KQTJdetail" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    跨月考勤明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper">
        <div class="box-outer">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <strong>时间：</strong>
                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_OnSelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;年&nbsp;
                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_OnSelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;月&nbsp;
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <strong>部门：</strong>&nbsp;
                        <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedIndexChanged="dplbm_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <strong>班组：</strong>
                        <asp:DropDownList ID="ddlbz" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlbz_OnSelectedIndexChanged">
                        </asp:DropDownList>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <strong>姓名：</strong><asp:TextBox ID="txtName" Width="50px" ForeColor="Gray" runat="server"></asp:TextBox>
                        &nbsp;&nbsp;
                        
                        <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click"></asp:Button>
                   </td>
                </tr>
            </table>
        </div>
    </div>
    
    
    
    
    
    
    <div class="box-wrapper">
                <div class="box-outer">
                    <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                        <table align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                            border="1" width="100%">
                            <asp:Repeater ID="rptKQTJ" runat="server">
                                <HeaderTemplate>
                                    <tr align="center" style="background-color: #B9D3EE;">
                                        <td rowspan="2">
                                            序号
                                        </td>
                                        <td rowspan="2">
                                            工号
                                        </td>
                                        <td rowspan="2">
                                            姓名
                                        </td>
                                        <td rowspan="2">
                                            部门
                                        </td>
                                        <td rowspan="2">
                                            班组
                                        </td>
                                        <td rowspan="2">
                                            年月
                                        </td>
                                        <td rowspan="2">
                                            时间
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_1" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_2" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_3" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_4" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_5" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_6" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_7" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_8" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_9" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_10" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_11" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_12" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_13" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_14" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_15" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_16" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_17" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_18" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_19" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_20" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_21" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_22" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_23" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_24" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_25" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_26" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_27" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_28" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_29" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_30" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BT_31" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td rowspan="2">
                                            工作日出勤
                                        </td>
                                    </tr>
                                    <tr style="background-color: #B9D3EE;">
                                        <td>
                                            <asp:Label ID="WEEKBT_1" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_2" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_3" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_4" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_5" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_6" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_7" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_8" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_9" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_10" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_11" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_12" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_13" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_14" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_15" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_16" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_17" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_18" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_19" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_20" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_21" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_22" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_23" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_24" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_25" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_26" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_27" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_28" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_29" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_30" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBT_31" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);">
                                        <td runat="server">
                                            <asp:Label runat="server" ID="lbKQ_ST_ID" Visible="false" Text='<%#Eval("MX_STID")%>'></asp:Label>
                                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1+(Convert.ToDouble(UCPaging1.CurrentPage)-1)*(Convert.ToDouble(UCPaging1.PageSize))%>'></asp:Label>
                                        </td>
                                        <td id="td_ST_WORKNO" runat="server" align="center">
                                            <%#Eval("ST_WORKNO")%>
                                        </td>
                                        <td id="td_ST_NAME" runat="server" align="center">
                                            <%#Eval("ST_NAME")%>
                                        </td>
                                        <td id="td_DEP_NAME" runat="server" align="center">
                                            <%#Eval("DEP_NAME")%>
                                        </td>
                                        <td id="td_ST_DEPID1" runat="server" align="center">
                                            <%#Eval("ST_DEPID1")%>
                                        </td>
                                        <td id="td_MX_YEARMONTH" runat="server" align="center">
                                            <%#Eval("MX_YEARMONTH")%>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMX_SJD" runat="server" Text='<%#Eval("MX_SJD")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_1" runat="server" Text='<%#Eval("MX_1")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_2" runat="server" Text='<%#Eval("MX_2")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_3" runat="server" Text='<%#Eval("MX_3")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_4" runat="server" Text='<%#Eval("MX_4")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_5" runat="server" Text='<%#Eval("MX_5")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMX_6" runat="server" Text='<%#Eval("MX_6")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_7" runat="server" Text='<%#Eval("MX_7")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_8" runat="server" Text='<%#Eval("MX_8")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_9" runat="server" Text='<%#Eval("MX_9")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_10" runat="server" Text='<%#Eval("MX_10")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_11" runat="server" Text='<%#Eval("MX_11")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMX_12" runat="server" Text='<%#Eval("MX_12")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_13" runat="server" Text='<%#Eval("MX_13")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_14" runat="server" Text='<%#Eval("MX_14")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_15" runat="server" Text='<%#Eval("MX_15")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_16" runat="server" Text='<%#Eval("MX_16")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_17" runat="server" Text='<%#Eval("MX_17")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMX_18" runat="server" Text='<%#Eval("MX_18")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_19" runat="server" Text='<%#Eval("MX_19")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_20" runat="server" Text='<%#Eval("MX_20")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_21" runat="server" Text='<%#Eval("MX_21")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_22" runat="server" Text='<%#Eval("MX_22")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_23" runat="server" Text='<%#Eval("MX_23")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMX_24" runat="server" Text='<%#Eval("MX_24")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_25" runat="server" Text='<%#Eval("MX_25")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMX_26" runat="server" Text='<%#Eval("MX_26")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_27" runat="server" Text='<%#Eval("MX_27")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMX_28" runat="server" Text='<%#Eval("MX_28")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_29" runat="server" Text='<%#Eval("MX_29")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMX_30" runat="server" Text='<%#Eval("MX_30")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMX_31" runat="server" Text='<%#Eval("MX_31")%>'></asp:Label>
                                        </td>
                                        <td id="td_MX_GZRCQ" runat="server" align="center">
                                            <%#Eval("MX_GZRCQ")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                            没有记录!<br />
                            <br />
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
    
    
    
</asp:Content>
