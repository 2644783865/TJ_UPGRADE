<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_DYWGJZG.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_DYWGJZG" Title="无标题页" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<strong>未勾稽暂估单汇总</strong>
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="FM_JS/SelectCondition.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script type="text/javascript" language="javascript">
function gyshz() {
        window.open("FM_GYSHZ.aspx?FLAG=to");
    }
function datehz() {
        window.open("FM_DATEHZ.aspx?FLAG=to");
    }
</script>
<asp:Panel ID="Panel1" runat="server">
<div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                 <table style="width: 100%;">
                        <tr>
                            <td align="left">
                                  时间从：
                                <%--<asp:DropDownList ID="dplYear" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                                    AutoPostBack="true">
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="txtStartTime" Width="90px" class="easyui-datebox" runat="server" onClick="setday(this)"></asp:TextBox>
                                &nbsp;到:&nbsp;
                                <%--<asp:DropDownList ID="dplMoth" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                                    AutoPostBack="true">
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="txtEndTime" Width="90px"  runat="server" class="easyui-datebox" onClick="setday(this)"></asp:TextBox>  
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                物料类型：
                                    <asp:DropDownList ID="dplwltype" runat="server" Width="60px">
                                                        <asp:ListItem Value="NO" Selected="True" Text=""></asp:ListItem>
                                                        <asp:ListItem Value="0">钢材</asp:ListItem>
                                                        <asp:ListItem Value="1">五金库</asp:ListItem>
                                   </asp:DropDownList>
                             &nbsp;&nbsp;&nbsp;物料编码：<asp:TextBox ID="matcode" runat="server" Width="70px" ></asp:TextBox>
                             &nbsp;&nbsp;&nbsp;供应商：<asp:TextBox ID="gys" runat="server" Width="80px" ></asp:TextBox>
                             &nbsp;&nbsp;&nbsp;单据类型：<asp:TextBox ID="billtype" runat="server" Width="50px" ></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />
                            </td>
                            <td align="right">
                                <input id="gyshz" type="button" value="按供应商汇总" onclick="gyshz()" runat="server" />
                                &nbsp;&nbsp;&nbsp;
                                <input id="datehz" type="button" value="按时间段汇总" onclick="datehz()" runat="server" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnexport" runat="server" Text="导出" OnClick="btnexport_OnClick" />&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        
                    </table>
              </div>
         </div>
   <div style=" overflow:scroll">
          <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                border="1" width="100%">
          
                <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                        <th>
                        序号
                        </th>
                        <th>
                        入库单号
                        </th>
                        <th>
                        日期
                        </th>
                        <th>
                        供应商
                        </th>
                        <th>
                        物料编码
                        </th>
                        <th>
                        物料名称
                        </th>
                        <th>
                        规格
                        </th>
                        <th>
                        材质
                        </th>
                        <th>
                        数量
                        </th>
                        <th>
                        单价
                        </th>
                        <th>
                        金额
                        </th>
                        <th>
                        含税金额
                        </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                            <td>
                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbrkdh" runat="server" Enabled="false" Text='<%#Eval("WG_CODE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbdate" runat="server" Enabled="false" Text='<%#Eval("WG_VERIFYDATE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbgys" runat="server" Enabled="false" Text='<%#Eval("SupplierName")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbwlbm" runat="server" Enabled="false" Text='<%#Eval("WG_MARID")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbwlname" runat="server" Enabled="false" Text='<%#Eval("MNAME")%>'></asp:Label>
                            </td>
                            
                            <td align="center">
                                <asp:Label ID="lbguige" runat="server" Enabled="false" Text='<%#Eval("GUIGE")%>'></asp:Label>
                            </td>
                            
                            <td align="center">
                                <asp:Label ID="lbcaizhi" runat="server" Enabled="false" Text='<%#Eval("CAIZHI")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbsl" runat="server" Enabled="false" Text='<%#Eval("WG_RSNUM")%>'></asp:Label>
                            </td>
                            
                            <td align="center">
                                <asp:Label ID="lbdj" runat="server" Enabled="false" Text='<%#Eval("WG_UPRICE")%>'></asp:Label>
                            </td>
                            
                            <td align="center">
                                <asp:Label ID="lbje" runat="server" Enabled="false" Text='<%#Eval("WG_AMOUNT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbhsje" runat="server" Enabled="false" Text='<%#Eval("WG_CTAMTMNY")%>'></asp:Label>
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
                               <asp:Label ID="lbjehj" runat="server"></asp:Label>
                            </th>
                            <th align="center">
                                 <asp:Label ID="lbhsjehj" runat="server"></asp:Label>
                            </th>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="NoDataPanel" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                没有记录！</asp:Panel>
            <uc1:ucpaging ID="UCPaging1" runat="server" />
            </div>
 </div>
         </asp:Panel>
</asp:Content>
