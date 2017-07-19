<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_GZTZedit.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GZTZedit" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
   薪酬异动编辑页
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
          <div class="box-inner">
            <table style="width: 100%">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnsave" runat="server" Text="保存" OnClick="btnsave_click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td> 
                </tr>
            </table>
          </div>
          <div style="overflow: scroll;height: 500px;">
                <table id="table1" align="center" cellpadding="2" cellspacing="1"
                    border="1">
                               <asp:Repeater ID="rptProNumCost" runat="server">
                                     <HeaderTemplate>
                                        <tr align="center" style="background-color: #B9D3EE;text-overflow:ellipsis;white-space:nowrap;">
                                            <td>
                                                序号
                                            </td>
                                            <td>
                                                年月
                                            </td>
                                            <td>
                                                姓名
                                            </td>
                                            <td>
                                                工号
                                            </td>
                                            <td>
                                                部门
                                            </td>
                                            <td>
                                                班组
                                            </td>
                                            <td>
                                                补发加班费
                                            </td>
                                            <td>
                                                补发中夜班费
                                            </td>
                                            <td>
                                                补发金额
                                            </td>
                                            <td>
                                                补扣金额 
                                            </td>
                                            <td>
                                                备注
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="row" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);" style="text-overflow:ellipsis;white-space:nowrap;">
                                        <td>
                                           <asp:Label ID="lb_QD_ID" runat="server" Text='<%#Eval("QD_ID")%>' Visible="false"></asp:Label>
                                           <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false" Onclick="checkme(this)"/>
                                            <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_QD_YEARMONTH" runat="server" Text='<%#Eval("QD_YEARMONTH")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_ST_WORKNO" runat="server" Text='<%#Eval("ST_WORKNO")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_ST_NAME" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                            <asp:Label ID="lb_ST_ID" runat="server" Text='<%#Eval("ST_ID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbDEP_NAME" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbST_DEPID1" runat="server" Text='<%#Eval("ST_DEPID1")%>'></asp:Label>
                                        </td>
                                        
                                        <td align="center">
                                            <asp:TextBox ID="tb_GZTZ_BFJBF" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                                Text=""  width="70px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="tb_GZTZ_BFZYBF" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                                Text="" width="70px"></asp:TextBox>
                                        </td>
                                        
                                        <td align="center">
                                            <asp:TextBox ID="tb_GZTZ_BF" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                                Text=""  width="70px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="tb_GZTZ_BK" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                                Text="" width="70px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="tb_GZTZ_NOTE" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                                Text="" width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                    </asp:Repeater>
                 </table>
              </div>
</asp:Content>
