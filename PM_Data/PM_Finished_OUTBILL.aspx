<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_Finished_OUTBILL.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Finished_OUTBILL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    成品出库单&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="button-outer" OnClick="btnSave_OnClick" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReturn" runat="server" Text="返 回" CausesValidation="false" OnClick="btnReturn_OnClick"
                                            CssClass="button-outer" />
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <table width="100%">
                            <tr align="center">
                                <td align="center" colspan="5">
                                    <asp:Label ID="lbltitle1" runat="server" Text="成品出库单" Font-Bold="true" Font-Size="Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                               <td>
                                出库单号：<asp:Label ID="docnum" runat="server"></asp:Label>
                               </td>
                                <td align="left">
                                    发货日期：<asp:Label ID="lblOutDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                            <td colspan='5'>
                            备注：
                            <asp:TextBox ID="txt_note" runat="server" ></asp:TextBox>
                            </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            CellPadding="4" ForeColor="#333333" EmptyDataText="没有相关数据！">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                           <asp:Label ID="lblfid" runat="server" Text='<%#Eval("CM_FID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="CM_ID" runat="server" Text='<%#Eval("CM_ID")%>' Visible="false"></asp:Label>
                                     </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="编号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbianhao" runat="server" Text='<%#Eval("CM_BIANHAO")%>'></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="项目名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblproj" runat="server" Text='<%#Eval("KC_PROJ")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="合同号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontr" runat="server" Text='<%#Eval("KC_CONTR")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="任务单号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltsaid" runat="server" Text='<%#Eval("TSA_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="总序" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="TFO_ZONGXU" runat="server" Text='<%#Eval("TFO_ZONGXU") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="图号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtmap" runat="server" Text='<%#Eval("KC_MAP")%>' ></asp:Label>
                                        </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="设备名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtengname" runat="server" Text='<%#Eval("KC_NAME")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="出库数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtnumber" runat="server" Text='<%#Eval("TFO_CKNUM")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="库存数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtkcnum" runat="server" Text='<%#Eval("TFO_KCNUM")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="交货期" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtfhdate" runat="server" Text='<%#Eval("CM_JHTIME")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtnote" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>--%>
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <div>
                            <table width="100%" style="text-align: center">
                                <tr>
                                    <td>
                                        负责人:
                                        <asp:DropDownList ID="cob_fuziren" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        申请人:
                                        <asp:DropDownList ID="cob_sqren" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        制单人:<asp:TextBox ID="TextBoxexecutor" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="TextBoxexecutorid" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
