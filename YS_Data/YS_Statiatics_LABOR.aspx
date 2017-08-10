<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="YS_Statiatics_LABOR.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Statiatics_LABOR" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="LabelName" runat="server" Text="直接人工实际费用明细"></asp:Label>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">

    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Button ID="daochu" runat="server" Text="导出" OnClick="daochu_Click" Visible=false />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="overflow: auto">
                <asp:Panel ID="NoDataPanel" runat="server">
                    <div style="text-align: center; font-size: medium;">
                        <br />
                        没有记录!</div>
                </asp:Panel>
                <yyc:SmartGridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="合同号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="GS_CONTR" runat="server" Text='<%#Eval("GS_CONTR") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="任务号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="GS_TSAID" runat="server" Text='<%#Eval("GS_TSAID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目名称" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="GS_CUSNAME" runat="server" Text='<%#Eval("GS_CUSNAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="设备图号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="GS_TUHAO" runat="server" Text='<%#Eval("GS_TUHAO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="设备名称" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="GS_TUMING" runat="server" Text='<%#Eval("GS_TUMING") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="实际工时费用" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="GS_MONEY" runat="server" Text='<%#Eval("GS_MONEY") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="班组" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="GS_TYPE" runat="server" Text='<%#Eval("GS_TYPE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发生时间" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="DATE" runat="server" Text='<%#Eval("DATE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        
                        
                        
                    </Columns>
                    <PagerStyle CssClass="bomcolor" Wrap="false" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" Wrap="false" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </yyc:SmartGridView>
                <div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;合计金额:<asp:Label ID="je" runat="server" CssClass="Error"
                        Text="0"></asp:Label>
                </div>
                <uc1:UCPaging ID="UCPaging1" runat="server" Visible="false" />
            </div>
        </div>
    </div>
</asp:Content>
