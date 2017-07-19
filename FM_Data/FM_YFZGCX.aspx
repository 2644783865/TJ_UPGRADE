<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_YFZGCX.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_YFZGCX" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
结算单信息
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
<script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="FM_JS/SelectCondition.js" type="text/javascript"></script>
    
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
            <div class="box-inner">
                        <table style="width: 100%;">
                        <tr>
                            <td style="width: 21%;">
                                时间：
                                <asp:DropDownList ID="dplYear" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                                &nbsp;年&nbsp;
                                <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                                &nbsp;月&nbsp;
                            </td>
                            <td align="center">
                                勾稽状态：
                                    <asp:DropDownList ID="DropDownifgj" runat="server" Width="120px">
                                                        <asp:ListItem Value="NO" Selected="True" Text=""></asp:ListItem>
                                                        <asp:ListItem Value="0">当月勾稽</asp:ListItem>
                                                        <asp:ListItem Value="1">未勾稽查询</asp:ListItem>
                                                        <asp:ListItem Value="2">当月被勾稽</asp:ListItem>
                                                        <asp:ListItem Value="3">跨月勾稽以前</asp:ListItem>
                                   </asp:DropDownList>
                            </td>
                            <td align="center">
                                结算单号：<asp:TextBox ID="tbjsdh" runat="server"></asp:TextBox>
                            </td>
                                                                                    <td align="center">
                                供应商：<asp:TextBox ID="tx_gys" runat="server"></asp:TextBox>
                            </td>
                            <td align="center" style="width:21%">
                                任务号:<asp:TextBox ID="txtrwh" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />
                            </td>
                            <td>
                                <asp:Button ID="btnexport" runat="server" Text="任务号汇总导出" Font-Bold="false" OnClick="btnexport_click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="box-outer">
                    <div style="overflow:scroll">
                    <table id="table1" cellpadding="2" width="100%" cellspacing="1" class="toptable grid nowrap"
                                border="1">
                    <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                        <HeaderTemplate>
                            
                                <tr align="center">
                                    <td align="center">
                                        <strong>序号</strong>
                                    </td>
                                    <td align="center">
                                        <strong>结算单号</strong>
                                    </td>
                                    <td align="center">
                                        <strong>结算单日期</strong>
                                    </td>
                                    <td align="center">
                                        <strong>供应商名称</strong>
                                    </td>
                                    <td align="center">
                                        <strong>制单人姓名</strong>
                                    </td>
                                    <td align="center">
                                        <strong>计划跟踪号</strong>
                                    </td>
                                    <td align="center">
                                        <strong>任务号</strong>
                                    </td>
                                    
                                    <td align="center">
                                        <strong>图号</strong>
                                    </td>
                                    <td align="center">
                                        <strong>设备名称</strong>
                                    </td>
                                    <td align="center">
                                        <strong>数量</strong>
                                    </td>
                                    <td align="center">
                                        <strong>重量</strong>
                                    </td>
                                    <td align="center">
                                        <strong>金额</strong>
                                    </td>
                                    <td align="center">
                                        <strong>税率</strong>
                                    </td>
                                    <td align="center">
                                        <strong>含税金额</strong>
                                    </td>
                                    <td align="center">
                                        <strong>发票金额</strong>
                                    </td>
                                    <td align="center">
                                        <strong>发票含税金额</strong>
                                    </td>
                                    <td align="center">
                                        <strong>发票编号</strong>
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                    <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label> 
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server" Checked="false" Onclick="checkme(this)"></asp:CheckBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbjsdh" runat="server" Text='<%#Eval("JS_BH")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbdate" runat="server" Text='<%#Eval("JS_RQ")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbgysmc" runat="server" Text='<%#Eval("JS_GYS")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbzdrxm" runat="server" Text='<%#Eval("JS_ZDR")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbjhgzh" runat="server" Text='<%#Eval("JS_JHGZH")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbrwh" runat="server" Text='<%#Eval("JS_RWH")%>'></asp:Label>
                                </td>
                                
                                <td runat="server" align="center">
                                    <asp:Label ID="lbtuhao" runat="server" Text='<%#Eval("JS_TUHAO")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbmname" runat="server" Text='<%#Eval("JS_SBMC")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbsl" runat="server" Text='<%#Eval("JS_JSSL")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbzl" runat="server" Text='<%#Eval("JS_ZONGZ")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbje" runat="server" Text='<%#Eval("MONEY")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbshuilv" runat="server" Text='<%#Eval("JS_SHUIL")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbhsje" runat="server" Text='<%#Eval("JS_HSJE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbfpje" runat="server" Text='<%#Eval("YFGJ_JE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbfphsje" runat="server" Text='<%#Eval("YFGJ_HSJE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbfpbh" runat="server" Text='<%#Eval("YFGJ_FPID")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                             <tr>
                                <th colspan="2" align="right">
                                合计:
                                </th>
                                <th colspan="8">
                                
                                </th>
                                <th align="center">
                                     <asp:Label ID="lbzlhj" runat="server"></asp:Label>
                                </th>
                                <th>
                                   <asp:Label ID="lbjehj" runat="server"></asp:Label>
                                </th>
                                <th>
                                
                                </th>
                                <th align="center">
                                     <asp:Label ID="lbhsjehj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                     <asp:Label ID="lbfpjehj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                     <asp:Label ID="lbfphsjehj" runat="server"></asp:Label>
                                </th>
                                <th>
                                
                                </th>
                             </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                    </table>
                    <asp:Panel ID="palNoData" runat="server" Visible="false">
                                没有记录！</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </div>
        </div>
</asp:Content>