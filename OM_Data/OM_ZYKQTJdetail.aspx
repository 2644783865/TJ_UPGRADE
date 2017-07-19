<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_ZYKQTJdetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_ZYKQTJdetail" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
整月考勤明细
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
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:FileUpload ID="FileUpload1" Width="130px" runat="server" ToolTip="导 入" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_importclient" runat="server" Text="导入" OnClientClick="viewCondition()" />
                        <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btn_importclient"
                                    PopupControlID="PanelCondition" Drag="True" Enabled="True" DynamicServicePath=""
                                    Y="80" X="900">
                        </asp:ModalPopupExtender>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   </td>
                </tr>
            </table>
            <asp:Panel ID="PanelCondition" runat="server" Width="300px" Style="display: none">
                            <table width="90%" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td colspan="8" align="center">
                                        <asp:Button ID="QueryButton" runat="server" OnClick="btn_import_Click" Text="确认" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="取消" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="message" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                    </td> 
                                </tr>
                                <tr>
                                    <td align="left">
                                        考勤月份
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_yearmonth" Width="120px" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                ID="CalendarExtender1" runat="server" Format="yyyy-MM" TargetControlID="tb_yearmonth">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
           </asp:Panel>
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
                                            <asp:Label ID="BTZY_1" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_2" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_3" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_4" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_5" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_6" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_7" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_8" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_9" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_10" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_11" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_12" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_13" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_14" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_15" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_16" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_17" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_18" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_19" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_20" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_21" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_22" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_23" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_24" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_25" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_26" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_27" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_28" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_29" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_30" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BTZY_31" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td rowspan="2">
                                            工作日出勤
                                        </td>
                                    </tr>
                                    <tr style="background-color: #B9D3EE;">
                                        <td>
                                            <asp:Label ID="WEEKBTZY_1" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_2" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_3" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_4" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_5" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_6" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_7" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_8" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_9" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_10" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_11" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_12" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_13" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_14" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_15" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_16" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_17" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_18" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_19" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_20" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_21" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_22" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_23" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_24" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_25" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_26" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_27" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_28" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_29" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_30" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WEEKBTZY_31" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);">
                                        <td runat="server">
                                            <asp:Label runat="server" ID="lbKQ_ST_ID" Visible="false" Text='<%#Eval("MXZY_STID")%>'></asp:Label>
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
                                        <td id="td_MXZY_YEARMONTH" runat="server" align="center">
                                            <%#Eval("MXZY_YEARMONTH")%>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMXZY_SJD" runat="server" Text='<%#Eval("MXZY_SJD")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_1" runat="server" Text='<%#Eval("MXZY_1")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_2" runat="server" Text='<%#Eval("MXZY_2")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_3" runat="server" Text='<%#Eval("MXZY_3")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_4" runat="server" Text='<%#Eval("MXZY_4")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_5" runat="server" Text='<%#Eval("MXZY_5")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMXZY_6" runat="server" Text='<%#Eval("MXZY_6")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_7" runat="server" Text='<%#Eval("MXZY_7")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_8" runat="server" Text='<%#Eval("MXZY_8")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_9" runat="server" Text='<%#Eval("MXZY_9")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_10" runat="server" Text='<%#Eval("MXZY_10")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_11" runat="server" Text='<%#Eval("MXZY_11")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMXZY_12" runat="server" Text='<%#Eval("MXZY_12")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_13" runat="server" Text='<%#Eval("MXZY_13")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_14" runat="server" Text='<%#Eval("MXZY_14")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_15" runat="server" Text='<%#Eval("MXZY_15")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_16" runat="server" Text='<%#Eval("MXZY_16")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_17" runat="server" Text='<%#Eval("MXZY_17")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMXZY_18" runat="server" Text='<%#Eval("MXZY_18")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_19" runat="server" Text='<%#Eval("MXZY_19")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_20" runat="server" Text='<%#Eval("MXZY_20")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_21" runat="server" Text='<%#Eval("MXZY_21")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_22" runat="server" Text='<%#Eval("MXZY_22")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_23" runat="server" Text='<%#Eval("MXZY_23")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMXZY_24" runat="server" Text='<%#Eval("MXZY_24")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_25" runat="server" Text='<%#Eval("MXZY_25")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMXZY_26" runat="server" Text='<%#Eval("MXZY_26")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_27" runat="server" Text='<%#Eval("MXZY_27")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMXZY_28" runat="server" Text='<%#Eval("MXZY_28")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_29" runat="server" Text='<%#Eval("MXZY_29")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:Label ID="lbMXZY_30" runat="server" Text='<%#Eval("MXZY_30")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbMXZY_31" runat="server" Text='<%#Eval("MXZY_31")%>'></asp:Label>
                                        </td>
                                        <td id="td_MXZY_GZRCQ" runat="server" align="center">
                                            <%#Eval("MXZY_GZRCQ")%>
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
