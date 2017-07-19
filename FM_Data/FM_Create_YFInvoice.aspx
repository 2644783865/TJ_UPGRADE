<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_Create_YFInvoice.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_Create_YFInvoice" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <p>下推发票</p>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="FM_JS/SelectCondition.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<div class="RightContent">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
            <table width="98%">
            <tr>
            <td align="left" style="width:50%"><strong>提示：以下结算单明细将下推到同一发票上</strong></td>
            <td style="width:30%" align="right">
                <asp:Button ID="btnCreatInv" runat="server" Text="生成发票"                     
                    onclick="btnCreatInv_Click" OnClientClick="javascript:return confirm('确认生成发票吗？')"/>               
            </td>
            <td align="right" style="width:20%">
            <a href="FM_YFInvoice_Managemnt.aspx" title="返回到发票管理界面" >返回</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            </tr>
            </table>
            </div>
        </div>
    </div>
    
     <div class="box-wrapper">
                    <div class="box-outer" style="overflow:scroll">
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
                                        <strong>日期</strong>
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
                                        <strong>单重</strong>
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
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                    <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label> 
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
                                    <asp:Label ID="lbsl" runat="server" Text='<%#Eval("JS_BJSL")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbzl" runat="server" Text='<%#Eval("JS_DANZ")%>'></asp:Label>
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
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                             <tr>
                                <th colspan="2" align="right">
                                合计:
                                </th>
                                <th colspan="8">
                                
                                </th>
                                <th>
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
                             </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                    </table>
                    <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                        没有记录!<br />
                        <br />
                    </asp:Panel>
            </div>
            </div>
        </div>
</asp:Content>
