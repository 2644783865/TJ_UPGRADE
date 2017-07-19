<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="test2.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.test2" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    技术任务分工
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td>
                            <asp:Button Text="点击按钮" runat="server" onclick="Unnamed1_Click" />
                        </td>
                        <td>
                       技术员：
                       <asp:DropDownList ID="ddlPer" runat="server">
                       <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                       <asp:ListItem Value="155" Text="肖书文"></asp:ListItem>
                       <asp:ListItem Value="67" Text="李小婷"></asp:ListItem>
                       </asp:DropDownList>
                        </td>
                        <td>
                         按照任务号查询：
                        <asp:TextBox ID="txtTaskId" runat="server" ></asp:TextBox>
                        </td>
                        <td align="center">
                        <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                        </td>
                        <td align="center">
                        </td>
                        <td visible="false">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 99%; overflow: auto;">
            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Width="8px" CssClass="checkBoxCss" />
                        </ItemTemplate>
                        <ItemStyle Width="8px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TSA_ID" HeaderText="任务号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TSA_PJID" HeaderText="合同号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CM_PROJ" HeaderText="项目名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TSA_ENGNAME" HeaderText="设备名称" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TSA_CONTYPE" HeaderText="设备类型" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TSA_TCCLERKNM" HeaderText="技术员" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TSA_REVIEWER" HeaderText="评审人" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TSA_STARTDATE" HeaderText="任务开始时间" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TSA_MANCLERKNAME" HeaderText="任务分工人" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="tsa_status" runat="server" Text='<%# Eval("TSA_STATE").ToString()=="0"?"待分工":Eval("TSA_STATE").ToString()=="1"?"进行中...":Eval("TSA_STATE").ToString()=="2"?"完工":"停工" %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="经营计划单" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlPJinfo" CssClass="link" runat="server" NavigateUrl='<%#"~/CM_Data/CM_TaskPinS.aspx?action=look&id="+Eval("ID") %>'>
                                <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                查看单据
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="分工信息" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlTask" CssClass="link" runat="server" NavigateUrl='<%#"TM_Tech_Assign_Detail.aspx?tmdetail_id="+Eval("TSA_ID")%>'>
                                <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                修改
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
               <asp:Panel ID="NoDataPanel" runat="server">
                        没有记录!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
