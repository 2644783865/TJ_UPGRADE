<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_GdzcPcDetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GdzcPcDetail" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    固定资产购置申请&nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <asp:HiddenField ID="HiddenFieldContent" runat="server" />
                                    <td align="right">
                                        <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_OnClick" CssClass="button-outer" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReturn" runat="server" Text="返 回" CausesValidation="False" OnClick="btnReturn_OnClick"
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
                        <table align="center" cellpadding="4" cellspacing="1" runat="server" class="toptable grid"
                            border="1">
                            <tr align="center">
                                <td align="center" colspan="6" style="font-weight: bold; font-size: large">
                                    固定资产购置申请表(No.<asp:Label ID="lblCode" runat="server"></asp:Label>)
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width: 15%">
                                    申请部门：
                                </td>
                                <td style="width: 15%">
                                    <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    联系人：
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtLinkman" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 15%">
                                    联系电话：
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <%--  <tr>
                                <td align="left" colspan="6">
                                    申购清单如下所示：
                                </td>
                               
                            </tr>--%>
                            <tr>
                                <td colspan="6">
                                    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" ForeColor="#333333">
                                        <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                    <asp:CheckBox ID="chk" runat="server" CssClass="checkBoxCss" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="名称" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                                ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtName" runat="server" Text='<%#Eval("NAME") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="型号或参数" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                                ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtModel" runat="server" Text='<%#Eval("MODEL") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                                ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNum" runat="server" Text='<%#Eval("NUM") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="放置地点" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                                ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLocation" runat="server" Text='<%#Eval("LOCATION") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="需求时间" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                                ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtXqtime" runat="server" Text='<%#Eval("XQTIME") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                    <asp:Panel ID="NoDataPanel" runat="server">
                                        没有记录!</asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    新增行数：<asp:TextBox ID="txtLines" runat="server" Width="75px"></asp:TextBox>
                                    <asp:Button ID="btnAdd" runat="server" Text="增 加" OnClick="btnAdd_OnClick" CausesValidation="false" />
                                </td>
                                <td align="left" colspan="2">
                                    <asp:Button ID="btnDelRow" runat="server" Text="删除行" OnClick="btnDelRow_OnClick"
                                        CausesValidation="false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 60px">
                                    申购理由：
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtReason" runat="server" Height="50px" Width="95%" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td style="height: 60px">
                                    备注：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNote" runat="server" Height="50px" Width="95%" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 40px">
                                <td align="right">
                                    图片上传：
                                </td>
                                <td class="category">
                                    <asp:FileUpload ID="FileUpload1" runat="server" /><asp:Button ID="btnAddFU" runat="server"
                                        Text="上传图片" OnClick="btnUp_Click" CausesValidation="False" />
                                    <br />
                                    <asp:Label ID="filesError" runat="server" ForeColor="Red" Visible="False" EnableViewState="false"></asp:Label>
                                    <asp:GridView ID="AddGridViewFiles" runat="server" CellPadding="4" CssClass="toptable grid"
                                        AutoGenerateColumns="False" PageSize="5" ForeColor="#333333" DataKeyNames="fileID"
                                        Width="50%">
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                            <asp:BoundField DataField="fileName" HeaderText="文件名称" HeaderStyle-Wrap="false">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fileUpDate" HeaderText="文件上传时间" HeaderStyle-Wrap="false">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="删除" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Assets/images/erase.gif"
                                                        Height="15px" Width="15px" OnClick="imgbtnDelete_Click" CausesValidation="False"
                                                        ToolTip="删除" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ControlStyle Font-Size="Small" />
                                                <%--<HeaderStyle Width="30px" />--%>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="下载" HeaderStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDF" runat="server" ImageUrl="~/Assets/images/pdf.jpg"
                                                        OnClick="imgbtnDF_Click" Height="15px" Width="15px" CausesValidation="False"
                                                        ToolTip="下载" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ControlStyle Font-Size="Small" />
                                                <%--<HeaderStyle Width="30px" />--%>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" Font-Size="X-Small" ForeColor="White"
                                            Height="10px" />
                                        <RowStyle BackColor="#EFF3FB" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    申请人：
                                </td>
                                <td>
                                    <asp:Label ID="lblAgent" runat="server"></asp:Label><asp:Label ID="lblAgent_id" Visible="false"
                                        runat="server"></asp:Label>
                                </td>
                                <td>
                                    申请时间：
                                </td>
                                <td>
                                    <asp:Label ID="lblAddtime" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
     
    
</asp:Content>
