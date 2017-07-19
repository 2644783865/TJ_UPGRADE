<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="PM_CNFB_DETAILL.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_CNFB_DETAILL" Title="修改数据页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
   修改数据
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
  <asp:Panel ID="Panel1" runat="server">
   <div style=" overflow:scroll">
          <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                border="1" width="100%">
          
                <asp:Repeater ID="rptProNumCost" runat="server">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                                <th>
                                    序号
                                </th>
                                <th>
                                    项目名称
                                </th>
                                <th>
                                    合同号
                                </th>
                                <th>
                                    任务号
                                </th>
                                <th>
                                    图号
                                </th>
                                <th>
                                    设备名称
                                </th>
                                <th>
                                    数量
                                </th>
                                <th>
                                    本月明义结算金额（元）
                                </th>
                                <th>
                                    本月实际结算金额（元）
                                </th>
                                <th>
                                    年份
                                </th>
                                <th>
                                    月份
                                </th>
                                <th>
                                    班组
                                </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;   
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbprojname" runat="server" Text='<%#Eval("CNFB_PROJNAME")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbprojid" runat="server" Text='<%#Eval("CNFB_HTID")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbrwh" runat="server" Text='<%#Eval("CNFB_TSAID")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbth" runat="server" Text='<%#Eval("CNFB_TH")%>'></asp:TextBox>
                                </td>
                                <td style="width:300px;white-space:normal" align="center">
                                    <asp:TextBox ID="tbsbname" runat="server" Text='<%#Eval("CNFB_SBNAME")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbsl" runat="server" Text='<%#Eval("CNFB_NUM")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbbymymoney" runat="server" align="center" Text='<%#Eval("CNFB_BYMYMONEY")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbbyrealmoney" runat="server" align="center" Text='<%#Eval("CNFB_BYREALMONEY")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbyear" runat="server" align="center" Text='<%#Eval("CNFB_YEAR")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbmonth" runat="server" align="center" Text='<%#Eval("CNFB_MONTH")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbtype" runat="server" align="center" Text='<%#Eval("CNFB_TYPE")%>'></asp:TextBox>
                                </td> 
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            </div>
            <div class="box_right" style="height:20px; padding-top:5px;" align="center" >
                    <asp:Button ID="btnConfirm" runat="server" Text="修改" 
                        onclick="btnConfirm_Click" Width="6%" BackColor="White" 
                        BorderStyle="Solid" Font-Bold="True" Font-Size="Small"
                      />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取消" CausesValidation="false" 
                            onclick="btnCancel_Click" Width="6%" BackColor="White" 
                        BorderStyle="Solid" Font-Bold="True" Font-Size="Small" />
            </div>
         </asp:Panel>
</asp:Content>
