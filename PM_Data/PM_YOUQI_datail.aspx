<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_YOUQI_datail.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.master" Inherits="ZCZJ_DPF.PM_Data.PM_YOUQI_datail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    油漆涂装细化方案
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../PC_Data/PcJs/superTables_compressed.js" type="text/javascript"></script>

    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />

    <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/TM_BlukCopy.js" type="text/javascript" charset="GB2312"></script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="box-wrapper" style="height: 472px">
        <div style="height: 6px" class="box_top">
        </div>
        <div class="box-outer">
            <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <tr>
                    <td style="font-size: large; text-align: center;" colspan="7">
                        油漆涂装细化方案
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnChuli" Text="处理" Width="40px" OnClick="btnChuli_OnClick" />
                    </td>
                    <td align="right">
                        <asp:Image ID="Image3" ToolTip="返回上一页" CssClass="hand" Height="16" Width="16" runat="server"
                            onclick="history.go(-1);" ImageUrl="~/Assets/icons/back.png" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 8%" align="right">
                        合同号:
                    </td>
                    <td style="width: 15%">
                        <asp:Label ID="tsaid" runat="server" Width="100%" />
                    </td>
                    <td style="width: 8%" align="right">
                        项目名称:
                    </td>
                    <td style="width: 14%">
                        <asp:Label ID="proname" runat="server" Width="100%" />
                        <input id="proid" type="text" runat="server" readonly="readonly" style="display: none" />
                    </td>
                    <td style="width: 8%" align="right">
                        设备名称:
                    </td>
                    <td style="width: 14%">
                        <asp:Label ID="engname" runat="server" Width="100%" />
                    </td>
                    <td style="width: 8%" align="right">
                        计划编号:
                    </td>
                    <td style="width: 25%">
                        <asp:Label ID="ps_no" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 8%" align="right">
                        油漆品牌:
                    </td>
                    <td colspan="3">
                        <asp:Label ID="paint_brand" runat="server" Width="100%" />
                    </td>
                    <td style="width: 8%" align="right">
                        编制日期:
                    </td>
                    <td colspan="3">
                        <asp:Label ID="plandate" runat="server" Width="100%" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="box-outer">
            <asp:Panel ID="Panel1" runat="server" Style="height: 400px; width: 100%; position: static">
                <div style="border: 1px solid #000000; height: 385px">
                    <div class="cpbox4 xscroll">
                        <table id="tab" class="nowrap cptable fullwidth">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="rpt_OnItemDataBound" >
                                <HeaderTemplate>
                                    <tr align="center" style="background-color: #B9D3EE" id="row">
                                        <td rowspan="2">
                                            任务号
                                        </td>
                                        <td rowspan="2">
                                            部件名称
                                        </td>
                                        <td rowspan="2">
                                            图号
                                        </td>
                                        <td rowspan="2">
                                            除锈级别
                                        </td>
                                        <td rowspan="2">
                                            涂装面积(m2)
                                        </td>
                                        <td colspan="4">
                                            底漆
                                        </td>
                                        <td colspan="4">
                                            中间漆
                                        </td>
                                        <td colspan="6">
                                            面漆
                                        </td>
                                        <td rowspan="2">
                                            总厚度
                                        </td>
                                        <td rowspan="2">
                                            备注
                                        </td>
                                        <td rowspan="2">
                                            变更备注
                                        </td>
                                    </tr>
                                    <tr align="center" style="background-color: #B9D3EE" id="Tr1">
                                        <td>
                                            种类
                                        </td>
                                        <td>
                                            厚度(um)
                                        </td>
                                        <td>
                                            用量(L)
                                        </td>
                                        <td>
                                            稀释剂(L)
                                        </td>
                                        <td>
                                            种类
                                        </td>
                                        <td>
                                            厚度(um)
                                        </td>
                                        <td>
                                            用量(L)
                                        </td>
                                        <td>
                                            稀释剂(L)
                                        </td>
                                        <td>
                                            种类
                                        </td>
                                        <td>
                                            厚度(um)
                                        </td>
                                        <td>
                                            用量(L)
                                        </td>
                                        <td>
                                            稀释剂(L)
                                        </td>
                                        <td>
                                            颜色
                                        </td>
                                        <td>
                                            色号
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" runat="server" id="tr">
                                        <td>
                                            <%# Eval("PS_ENGID") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_NAME") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_TUHAO") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_LEVEL") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_MIANJI") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_BOTSHAPE") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_BOTHOUDU") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_BOTYONGLIANG") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_BOTXISHIJI") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_MIDSHAPE") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_MIDHOUDU") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_MIDYONGLIANG") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_MIDXISHIJI") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_TOPSHAPE") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_TOPHOUDU") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_TOPYONGLIANG") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_TOPXISHIJI") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_TOPCOLOR") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_TOPCOLORLABEL") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_TOTALHOUDU") %>
                                        </td>
                                        <td>
                                            <%# Eval("PS_BEIZHU") %>
                                        </td>
                                        <td>
                                            <div style="width: 150px; white-space: normal">
                                                <asp:Label runat="server" ID="PS_BGBEIZHU" Text='<%#Eval("PS_BGBEIZHU")%>'></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
