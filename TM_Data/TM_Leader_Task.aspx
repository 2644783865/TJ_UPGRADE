<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="TM_Leader_Task.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Leader_Task" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    任务审核信息列表</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/DatePicker.js" language="javascript" type="text/javascript"></script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <table style="width: 100%">
                <tr>
                    <td align="right">
                        <strong>查询类别:</strong>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblstate" RepeatColumns="2" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="rblstate_SelectedIndexChanged">
                            <asp:ListItem Text="正常" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="变更" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbltask" RepeatColumns="5" runat="server" OnSelectedIndexChanged="rbltask_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:RadioButtonList>
                    </td>
                    <td align="right">
                        <asp:DropDownList ID="ddlQueryType" runat="server">
                            <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                            <asp:ListItem Text="批号" Value="批号"></asp:ListItem>
                            <asp:ListItem Text="项目名称" Value="项目名称"></asp:ListItem>
                            <asp:ListItem Text="合同号" Value="合同号"></asp:ListItem>
                            <asp:ListItem Text="设备名称" Value="设备名称"></asp:ListItem>
                            <asp:ListItem Text="任务号" Value="任务号"></asp:ListItem>
                            <asp:ListItem Text="技术员" Value="技术员"></asp:ListItem>
                        </asp:DropDownList>
                           <asp:TextBox ID="txtQueryContent" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnQuery" runat="server" UseSubmitBehavior="false" OnClick="btnQuery_OnClick"
                            Text="查 询" />&nbsp;&nbsp;&nbsp;
                    </td>
                   
                </tr>
                <tr>
                    <td align="right">
                        <strong>审核状态:</strong>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblstatus" RepeatColumns="5" runat="server" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <strong>技术员查询:&nbsp;&nbsp;</strong><asp:CheckBox ID="ckbTech" runat="server" AutoPostBack="true"
                            OnCheckedChanged="rblstatus_SelectedIndexChanged" />只显示我提交审批的任务 (待完成项:未提交<asp:Label
                                ID="lblUSub" runat="server" ForeColor="Red" Text="Label"></asp:Label>，审核中<asp:Label
                                    ID="lblInRvw" runat="server" ForeColor="Red" Text="Label"></asp:Label>，驳回<asp:Label
                                        ID="lblRej" runat="server" ForeColor="Red" Text="Label"></asp:Label>)
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Width="25px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="25px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="批号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Width="100%" Text='<%# Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PJNAME" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ENGNAME" HeaderText="设备名称" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MAP" HeaderText="图号" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SHAPE" HeaderText="类型" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SUBMITNM" HeaderText="技术员" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SUBMITTM" HeaderText="提交日期" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ADATE" HeaderText="审核日期" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REVIEWANAME" HeaderText="审核人" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REVIEWAADVC" HeaderText="意见" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" Visible="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REVIEWBNAME" HeaderText="审核人" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REVIEWBADVC" HeaderText="意见" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" Visible="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REVIEWCNAME" HeaderText="审核人" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REVIEWCADVC" HeaderText="意见" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" Visible="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="status" runat="server" Text='<%# Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="2"?"待审中":Eval("STATE").ToString()=="4"||Eval("STATE").ToString()=="6"?"审核中...":Eval("STATE").ToString()=="8"?"通过":Eval("STATE").ToString()=="9"?"已处理":"驳回" %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlTask1" CssClass="link" NavigateUrl='<%#"TM_MP_Require_Audit.aspx?mp_audit_id="+Eval("ID")%>'
                                runat="server">
                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />
                                <asp:Label ID="state1" runat="server" Text='<%# Eval("STATE").ToString()=="2"||Eval("STATE").ToString()=="4"||Eval("STATE").ToString()=="6"?"审核":"查看" %>'></asp:Label>
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlTask2" CssClass="link" NavigateUrl='<%#"TM_MS_Detail_Audit.aspx?ms_audit_id="+Eval("ID")%>'
                                runat="server">
                                <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />
                                <asp:Label ID="state2" runat="server" Text='<%# Eval("STATE").ToString()=="2"||Eval("STATE").ToString()=="4"||Eval("STATE").ToString()=="6"?"审核":"查看" %>'></asp:Label>
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlTask3" CssClass="link" NavigateUrl='<%#"TM_Out_Source_Audit.aspx?ost_audit_id="+Eval("ID")%>'
                                runat="server">
                                <asp:Image ID="Image3" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />
                                <asp:Label ID="state3" runat="server" Text='<%# Eval("STATE").ToString()=="2"||Eval("STATE").ToString()=="4"||Eval("STATE").ToString()=="6"?"审核":"查看" %>'></asp:Label>
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlTask4" CssClass="link" NavigateUrl='<%#"TM_Packing_List_Audit.aspx?pk_audit_id="+Eval("ID")%>'
                                runat="server">
                                <asp:Image ID="Image4" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />
                                <asp:Label ID="state4" runat="server" Text='<%# Eval("STATE").ToString()=="2"||Eval("STATE").ToString()=="4"||Eval("STATE").ToString()=="6"?"审核":"查看" %>'></asp:Label>
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlTask5" CssClass="link" NavigateUrl='<%#"TM_Paint_Scheme_Audit.aspx?ps_audit_id="+Eval("ID")%>'
                                runat="server">
                                <asp:Image ID="Image5" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />
                                <asp:Label ID="state5" runat="server" Text='<%# Eval("STATE").ToString()=="2"||Eval("STATE").ToString()=="4"||Eval("STATE").ToString()=="6"?"审核":"查看" %>'></asp:Label>
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbtnCancel" runat="server" OnClientClick='return confirm("确认将该批作废吗？");'
                                OnClick="lkbtnCancel_OnClick" CommandArgument='<%#Eval("ID") %>'>
                                <asp:Image ID="Image6" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />作废</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:Panel ID="NoDataPanel" runat="server">
                没有任务!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
