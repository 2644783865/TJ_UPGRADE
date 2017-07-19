<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_Create_wxInvoice.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_Create_wxInvoice" Title="无标题页" %>
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
            <a href="FM_wxInvoice_Managemnt.aspx" title="返回到发票管理界面" >返回</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <th align="center">
                                        序号
                                    </th>
                                    <th align="center">
                                        结算单号
                                    </th>
                                    <th align="center">
                                        供应商名称
                                    </th>
                                    <th align="center">
                                        制单人姓名
                                    </th>
                                    <th align="center">
                                        日期
                                    </th>
                                    <th align="center">
                                        计划跟踪号
                                    </th>
                                    <th align="center">
                                        任务号
                                    </th>
                                    <th align="center">
                                        外协件编号
                                    </th>
                                    <th align="center">
                                        外协件名称
                                    </th>
                                    <th align="center">
                                        数量
                                    </th>
                                    <th align="center">
                                        重量
                                    </th>
                                    <th align="center">
                                        单价
                                    </th>
                                    <th align="center">
                                        金额
                                    </th>
                                    <th align="center">
                                        税率
                                    </th>
                                    <th align="center">
                                        含税单价
                                    </th>
                                    <th align="center">
                                        含税金额
                                    </th>
                                    <th align="center">
                                        外协类型
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbjsdh" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("TA_DOCNUM")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbgysmc" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("TA_SUPPLYNAME")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbzdrxm" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("TA_ZDRNAME")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbdate" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("TA_ZDDATE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbjhgzh" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("TA_PTC")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbrwh" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("TA_ENGID")%>'></asp:Label>
                                </td>
                                
                                <td runat="server" align="center">
                                    <asp:Label ID="lbtuhao" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("TA_TUHAO")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbmname" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("TA_MNAME")%>'></asp:Label>
                                </td>
                                
                                
                                
                                
                                <td runat="server" align="center">
                                    <asp:Label ID="lbsl" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("TA_NUM")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbzl" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("TA_WGHT")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbdj" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("PRICE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbje" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("MONEY")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbshuilv" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("PIC_SHUILV")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbhsdj" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("TA_PRICE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbhsje" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("TA_MONEY")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbwxtype" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("TA_WXTYPE")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                             <tr>
                                <th colspan="2" align="right">
                                合计
                                </th>
                                <th colspan="8">
                                
                                </th>
                                <th align="center">
                                     <asp:Label ID="lbzlhj" runat="server"></asp:Label>
                                </th>
                                <th>
                                
                                </th>
                                <th>
                                   <asp:Label ID="lbjehj" runat="server"></asp:Label>
                                </th>
                                <th colspan="2">
                                
                                </th>
                                <th align="center">
                                     <asp:Label ID="lbhsjehj" runat="server"></asp:Label>
                                </th>
                                <th>
                                
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
