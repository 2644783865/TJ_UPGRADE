<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="TM_Md_Detail_Search_1.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Md_Detail_Search_1" Title="无标题页" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
关键部件任务管理</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/PDMN.js" type="text/javascript" charset="GB2312"></script>
<script type="text/javascript" charset="GB2312">
        function getClientId()
        {
            var paraId1 = '<%= GridView1.ClientID %>';//注册控件1
            return {Id1:paraId1};//生成访问器
        }
</script>
    <div style="height: 6px" class="box_top">
    </div>
    <div class="box-outer">
        <table width="100%">
            <tr>
                <td style="font-size: large; text-align: center;" colspan="8">
                    任务管理
                </td>
            </tr>
            <tr>
                <td style="width: 8%" align="right">
                    生产制号:
                </td>
                <td style="width: 15%">
                    <asp:Label ID="tsa_id" runat="server" Width="100%" />
                </td>
                <td style="width: 8%" align="right">
                    项目名称:
                </td>
                <td style="width: 14%">
                    <asp:Label ID="lab_proname" runat="server" Width="100%" />
                </td>
                <td style="width: 8%" align="right">
                    工程名称:
                </td>
                <td style="width: 14%">
                    <asp:Label ID="lab_engname" runat="server" Width="100%" />
                     <asp:Label ID="bomtime" runat="server" Visible="false" ></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="btn_save" runat="server" OnClientClick="return IsAddCGPDate()" OnClick="btn_save_Click" Text="保存" />
                </td>
                <td ><asp:Button ID="Delt" runat="server" OnClick="Delt_Click" Text="删除" /></td>
                <td ><asp:Button ID="Reset" runat="server" OnClick="Reset_Click" Text="返回" /></td>
            </tr>
            <input id="eng_type" type="text" runat="server" readonly="readonly" value="" style="display: none" />
        </table>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <yyc:SmartGridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnDataBound="GridView1_DataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chtask" runat="server" />
                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="MS_NEWINDEX" HeaderText="组装序号" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField  HeaderText="物料编码" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblmarid" runat="server" Text='<%#Eval("MS_MARID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MS_NAME" HeaderText="任务名称" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField  HeaderText="规格" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblguige" runat="server" Text='<%#Eval("MS_GUIGE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText="负责人" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddl_fuzeren" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="采购单位" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <input id="ttbz" type="text" runat="server"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="计划开始时间" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id='ttpstime' runat="server" type="text"  onclick="setday(this)" readonly="readonly"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="计划完成时间" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id='ttpftime' runat="server" type="text"  onclick="setday(this)" readonly="readonly"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="主键" FooterStyle-CssClass="hidden " ItemStyle-CssClass="hidden " HeaderStyle-CssClass="hidden ">
                    <ItemTemplate>
                          <asp:Label ID="lblmsid" runat="server" Text='<%# Eval("MS_ID") %>'></asp:Label>
                        <asp:Label ID="lblkeycoms" runat="server" Text='<%#Eval("MS_KEYCOMS") %>'></asp:Label>
                        <asp:Label ID="lblpdstate" runat="server" Text='<%#Eval("MS_PDSTATE") %>'></asp:Label>
                        <asp:Label ID="lblbgpid" runat="server" Text='<%#Eval("MS_CHGPID") %>'></asp:Label>
                        <asp:Label ID="lblzzindex" runat="server" Text='<%#Eval("MS_NEWINDEX") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <FixRowColumn FixRowType="Header,Pager" TableHeight="500px" TableWidth="100%" FixColumns="0,1" />
                    </yyc:SmartGridView>
                    <asp:Label ID="lbl_Info" runat="server" Text="Label" Visible="False"></asp:Label>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        没有记录!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
