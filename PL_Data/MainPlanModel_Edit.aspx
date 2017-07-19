<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="MainPlanModel_Edit.aspx.cs" Inherits="ZCZJ_DPF.PL_Data.MainPlanModel_Edit" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Src="../Controls/UserDefinedQueryConditions.ascx" TagName="UserDefinedQueryConditions"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<asp:Label ID="lblTitle" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td style="width: 13%">
                            模板编号:<asp:Label ID="lblModelId" runat="server"></asp:Label>
                        </td>
                        <td style="width: 25%">
                            模板名称:<asp:Label ID="lab_contract" runat="server"></asp:Label>
                            <input id="txtModelName" type="text" value=""  runat="server"  />
                        </td>
                        <td style="width: 25%">
                            适用范围:<asp:Label ID="lab_proname" runat="server"></asp:Label>
                            <input id="txtModelRange" type="text" value="" runat="server"  />
                        </td>
                        <td style="width: 25%">
                            备注: <input id="txtNote" type="text" value="" runat="server"  style="width:150px" />
                        </td>
                        <td >
                            <asp:LinkButton runat="server" ID="lbnSave" OnClick="lbnSave_Click">
                                保存
                                <asp:Image ID="ImageTo" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" /></asp:LinkButton>
                        </td>
                       
                    </tr>
                </table>
            </div>
        </div>
    </div>
     <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
        CellPadding="2" ForeColor="#333333" Width="100%">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField HeaderText="行号">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                     <asp:HiddenField ID="hidDetailId" Value='<%#Eval("MP_ID")%>' runat="server" />
                </ItemTemplate>
                <HeaderStyle Wrap="False" />
                <ItemStyle HorizontalAlign="Center" Wrap="False" />
            </asp:TemplateField>
            <asp:BoundField DataField="MP_TYPE" HeaderText="计划类别" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MP_PLNAME" HeaderText="主计划项" HeaderStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
           
            <asp:TemplateField HeaderText="计划天数" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="20px">
                <ItemTemplate>
                    <input type="text" runat="server" value='<%# Eval("MP_DAYS") %>' id="txtDays" style="width: 60px"
                        onchange="CalculateDate(this)" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="预警天数" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="20px">
                <ItemTemplate>
                    <input type="text" runat="server" value='<%# Eval("MP_WARNINGDAYS") %>' id="txtWarningDays"
                        style="width: 60px" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
           
            <asp:TemplateField HeaderText="备  注" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <input id="txtDetailNote" type="text" runat="server" value='<%# Eval("MP_NOTE") %>'
                        style="width: 250px" onfocus="calendar()" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
          
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        <FixRowColumn TableHeight="100%" TableWidth="80%" />
    </yyc:SmartGridView>
</asp:Content>
