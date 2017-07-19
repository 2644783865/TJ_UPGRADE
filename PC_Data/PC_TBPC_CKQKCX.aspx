<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="PC_TBPC_CKQKCX.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_CKQKCX" Title="超定额出库信息" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
  <strong>超定额出库信息</strong>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
<script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>
    
    
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    
    <script type="text/javascript">
        function sTable() {
            var myST = new superTable("tab", {
                cssSkin: "sDefault",
                headerRows: 1,
                fixedCols: 3,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
                    //		        this.start = new Date();
                },
                onFinish: function() {
                    //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                }
            });
        }
       $(function() {
            sTable();
        });
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    
    
            <div class="box-inner">
                        <table style="width: 100%;">
                        <tr>
                            <td style="width: 22%;">
                                时间：
                                <asp:DropDownList ID="dplYear" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth" AutoPostBack="true">
                                </asp:DropDownList>
                                &nbsp;年&nbsp;
                                <asp:DropDownList ID="dplMoth" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth" AutoPostBack="true">
                                </asp:DropDownList>
                                &nbsp;月&nbsp;
                            </td>
                            <td>
                               超出比例：<asp:DropDownList ID="drp_ccbl" runat="server" OnSelectedIndexChanged="drp_ccbl_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                                            <asp:ListItem Value="0" Selected="True">大于5%</asp:ListItem>
                                            <asp:ListItem Value="1">大于15%</asp:ListItem>
                                            <asp:ListItem Value="2">大于30%</asp:ListItem>
                                        </asp:DropDownList>
                               制单人：<asp:DropDownList ID="drp_zdr" runat="server" OnSelectedIndexChanged="btnQuery_OnClick" AutoPostBack="true">
                                       </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                               任务号：<asp:TextBox ID="tbtsaid" runat="server" Width="80px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                               计划跟踪号：<asp:TextBox ID="tbptc" runat="server" Width="150px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                               物料类型：<asp:TextBox ID="tbtype" runat="server" Width="60px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                状态：
                                <asp:RadioButton ID="radio_wqr" runat="server" Text="未确认" GroupName="shenhe" OnCheckedChanged="radio_wqr_CheckedChanged"
                                                    AutoPostBack="True"  Checked="true" />
                                    <asp:RadioButton ID="radio_yqr" runat="server" Text="已确认" GroupName="shenhe" OnCheckedChanged="radio_yqr_CheckedChanged"
                                                    AutoPostBack="True"/>
                                                    <asp:RadioButton ID="radio_all" runat="server" Text="全部" GroupName="shenhe" OnCheckedChanged="radio_all_CheckedChanged"
                                                    AutoPostBack="True"/>
                            </td>
                            <td>
                                 
                            </td>
                            <td align="right">
                                 <asp:Button ID="btnqr" runat="server" Text="确认" OnClick="btnqr_OnClick" Enabled="false" />
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="box-outer">
                    <div style="overflow:scroll;height: 475px; overflow: auto;">
                    <table id="tab" class="nowrap cptable fullwidth" align="center">
                    <asp:Repeater ID="rptProNumCost" runat="server">
                        <HeaderTemplate>
                            
                                <tr align="center">
                                    <td align="center">
                                        <strong>序号</strong>
                                    </td>
                                    <td align="center">
                                        <strong>出库单号</strong>
                                    </td>
                                    <td>
                                        <strong>任务号</strong>
                                    </td>
                                    <td align="center">
                                        <strong>物料编码</strong>
                                    </td>
                                    <td align="center">
                                        <strong>物料名称</strong>
                                    </td>
                                    <td align="center">
                                        <strong>规格</strong>
                                    </td>
                                    <td>
                                        <strong>材质</strong>
                                    </td>
                                    <td>
                                        <strong>长</strong>
                                    </td>
                                    <td>
                                        <strong>宽</strong>
                                    </td>
                                    <td align="center">
                                        <strong>单位</strong>
                                    </td>
                                    <td align="center">
                                        <strong>实发数量</strong>
                                    </td>
                                    <td align="center">
                                        <strong>计划数量</strong>
                                    </td>
                                    <td align="center">
                                        <strong>超出数量</strong>
                                    </td>
                                    <td align="center">
                                        <strong>类型</strong>
                                    </td>
                                    <td align="center">
                                        <strong>制单人</strong>
                                    </td>
                                    <td align="center">
                                        <strong>审核日期</strong>
                                    </td>
                                    <td>
                                        <strong>计划跟踪号</strong>
                                    </td>
                                    <td align="center">
                                        <strong>备注</strong>
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr  id="row" class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server" ></asp:CheckBox>
                                    <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="OutCode" runat="server" Text='<%#Eval("OutCode")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="TSAID" runat="server" Text='<%#Eval("TSAID")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="MaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="MaterialName" runat="server" Text='<%#Eval("MaterialName")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="Standard" runat="server" Text='<%#Eval("Standard")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="Attribute" runat="server" Text='<%#Eval("Attribute")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="Length" runat="server" Text='<%#Eval("Length")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="Width" runat="server" Text='<%#Eval("Width")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="Unit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="RealNumber" runat="server" Text='<%#Eval("RealNumber")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="PUR_NUMR" runat="server" Text='<%#Eval("PUR_NUMR")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="PUR_CCNUM" runat="server" Text='<%#Eval("PUR_CCNUM")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="PlanMode" runat="server" Text='<%#Eval("PlanMode")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="Doc" runat="server" Text='<%#Eval("Doc")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="ApprovedDate" runat="server" Text='<%#Eval("ApprovedDate")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="ptcode" runat="server" Text='<%#Eval("PTC")%>' Visible="false"></asp:Label>
                                    <asp:TextBox ID="PTC" runat="server" ForeColor="#1A438E" Style="background-color: Transparent;
                                                        text-align: center" Width="150px" Text='<%#Eval("PTC")%>' ToolTip='<%#Eval("PTC")%>'></asp:TextBox>
                                    
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="OP_NOTE1" runat="server" ForeColor="#1A438E" Style="background-color: Transparent;
                                                        text-align: center" Width="150px" Text='<%#Eval("OP_NOTE1")%>' ToolTip='<%#Eval("OP_NOTE1")%>'></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </table>
                    
                    <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                                没有记录！</asp:Panel>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
</asp:Content>
