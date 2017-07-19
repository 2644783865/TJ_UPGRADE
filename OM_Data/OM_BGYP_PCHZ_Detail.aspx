<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_BGYP_PCHZ_Detail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_BGYP_PCHZ_Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    办公用品采购汇总审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%">
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                        <asp:Button ID="btndaochu" runat="server" Text="导出" OnClick="btndaochu_click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="Save_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Close" Text="返回" OnClick="close" runat="server" />&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
      <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
        ActiveTabIndex="0" AutoPostBack="false">
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="申请采购明细" TabIndex="0">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div style="margin: 0px 0px 0px 10px">
                            <table width="90%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                                <tr>
                                    <td style="font-size: x-large; text-align: center;" colspan="2">
                                        办公用品采购申请单
                                        <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                            Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="HeadPanel" runat="server" Width="100%" Style="overflow: auto; position: static">
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode"
                                            runat="server"></asp:Label>
                                        <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
                                        <input type="text" id="InputColour" style="display: none" runat="server" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单&nbsp;&nbsp;&nbsp;人：<asp:Label ID="LabelDoc"
                                            runat="server"></asp:Label>
                                        <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
                                    </td>
                              <td>
                                        &nbsp;&nbsp;&nbsp;总&nbsp;&nbsp;&nbsp;额：<asp:Label ID="lbljine" runat="server"></asp:Label>
                                    </td>
                                   
                                </tr>
                                <tr align="center">
                                
                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：<asp:TextBox ID="txt_note"
                                        runat="server" Width="600px" TextMode="MultiLine"></asp:TextBox><asp:Label ID="state"
                                            runat="server" Visible="false"></asp:Label></tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="PanelBody" runat="server" Style="height: 350px;">
                            <div style="width: 100%; margin: 0 auto">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="20"
                                    CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="15px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" Width="15px" CssClass="checkBoxCss" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbindex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                 <input type="hidden" runat="server" id="hidPCCode" value='<%#Eval("PCCODE") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="WLBM" DataFormatString="{0:F2}" HeaderText="编码" HeaderStyle-Wrap="false">
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WLNAME" DataFormatString="{0:F2}" HeaderText="名称" HeaderStyle-Wrap="false">
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WLMODEL" DataFormatString="{0:F2}" HeaderText="规格型号" HeaderStyle-Wrap="false">
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WLUNIT" DataFormatString="{0:F2}" HeaderText="单位" HeaderStyle-Wrap="false">
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WLNUM" DataFormatString="{0:F2}" HeaderText="数量" HeaderStyle-Wrap="false">
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WLPRICE" DataFormatString="{0:F2}" HeaderText="单价" HeaderStyle-Wrap="false">
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WLJE" DataFormatString="{0:F2}" HeaderText="金额" HeaderStyle-Wrap="false">
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="num" DataFormatString="{0:F2}" HeaderText="库存数量" HeaderStyle-Wrap="false">
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DEPNAME" DataFormatString="{0:F2}" HeaderText="申请部门" HeaderStyle-Wrap="false">
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Note" DataFormatString="{0:F2}" HeaderText="备注" HeaderStyle-Wrap="false">
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="审  核" TabIndex="1">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center">
                                    一级审核
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Enabled="false" Text="王自清" Width="80px"></asp:TextBox>
                                                <asp:HiddenField ID="firstid" Value="311" runat="Server" />
                                                <%--<input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />--%>
                                                <%--<asp:HyperLink ID="hlSelect1" Enabled="false" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                                    <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>--%>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfirst" Enabled="false" RepeatColumns="2" runat="server"
                                                    Height="20px">
                                                    <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="first_opinion" Enabled="false" runat="server" TextMode="MultiLine"
                                                    Width="100%" Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
                        <input id="Hidden1" type="hidden" value="" />
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
