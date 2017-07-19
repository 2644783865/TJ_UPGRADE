<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="TM_Paint_Task.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Paint_Task"
    Title="无标题页" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    我的任务信息列表<JSR:JSRegister ID="JSRegister1" runat="server" />

    <script src="../JS/BOM.js" language="javascript" type="text/javascript" charset="GB2312"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/DatePicker.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function ShowAllData(xm_gc) {
            var date = new Date();
            var time = date.getTime();
            window.open("TM_Design_Bom.aspx?NoUse=" + time + "&xm_gc=" + xm_gc);
        }
        function ShowAllOrg(taskid) {
            var date = new Date();
            var time = date.getTime();
            window.open("TM_Task_View.aspx?NoUse=" + time + "&action=" + taskid);
        }

        function ShowMyNode(obj) {
            var date = new Date();
            var time = date.getTime();
            var returnValue = window.showModalDialog("TM_MyTaskNote.aspx?nouse=" + time + "&engid=" + obj.title, '', "dialogHeight:400px;dialogWidth:700px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 5%; height: 24px">
                            <b>我的任务</b>
                        </td>
                        <td align="right" style="width: 5%; height: 24px;">
                            分类查询:
                        </td>
                        <td style="width: 32%">
                            <asp:RadioButtonList ID="rblstatus" RepeatColumns="6" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                                <asp:ListItem Text="全部" Value="'1','2','3','4','6'"></asp:ListItem>
                                <asp:ListItem Text="进行中" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="完工" Value="2"></asp:ListItem>
                                <asp:ListItem Text="停工" Value="3"></asp:ListItem>
                                <asp:ListItem Text="虚拟任务" Value="4"></asp:ListItem>
                                <asp:ListItem Text="工装任务" Value="6"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right">
                            <strong>合同:</strong>
                        </td>
                        <td style="width: 17%; height: 42px" align="left" valign="top">
                            <cc1:ComboBox ID="ddlProName" runat="server" AutoPostBack="true" Height="15px" AutoCompleteMode="SuggestAppend"
                                DropDownStyle="DropDownList" OnSelectedIndexChanged="ddlProName_SelectedIndexChanged">
                            </cc1:ComboBox>
                        </td>
                        <td align="right">
                            <strong>设备:</strong>
                        </td>
                        <td style="width: 26%; height: 42px" align="left" valign="top">
                            <cc1:ComboBox ID="ddlEngName" runat="server" AutoPostBack="true" Height="15px" AutoCompleteMode="SuggestAppend"
                                DropDownStyle="DropDownList" OnSelectedIndexChanged="ddlEngName_SelectedIndexChanged">
                                <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                            </cc1:ComboBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-wrapper">
                <div class="box-outer">
                    <div style="width: 100%;">
                        <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                            AutoGenerateColumns="False" CellPadding="3" ForeColor="#333333" Style="white-space: normal">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="24px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                    HeaderText="任务号">
                                    <ItemTemplate>
                                        <label>
                                            <%# Eval("TSA_ID")%></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                    HeaderText="合同号">
                                    <ItemTemplate>
                                        <label>
                                            <%# Eval("TSA_PJID")%></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                    HeaderText="项目名称">
                                    <ItemTemplate>
                                        <label>
                                            <%# Eval("CM_PROJ")%></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="220px" HeaderText="设备名称">
                                    <ItemTemplate>
                                        <label>
                                            <%# Eval("TSA_ENGNAME")%></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                    HeaderText="设备类型">
                                    <ItemTemplate>
                                        <label>
                                            <%# Eval("TSA_CONTYPE")%></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                    HeaderText="技术员">
                                    <ItemTemplate>
                                        <label>
                                            <%# Eval("TSA_YQHZR")%></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                    HeaderText="开始时间">
                                    <ItemTemplate>
                                        <label>
                                            <%# Eval("TSA_STARTDATE")%></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                    HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label ID="tsa_status" runat="server" Text='<%# Eval("TSA_STATE").ToString()=="2"?"完工":Eval("TSA_STATE").ToString()=="1"?"进行中...":Eval("TSA_STATE").ToString()=="3"?"停工":Eval("TSA_STATE").ToString()=="4"?"虚拟任务":Eval("TSA_STATE").ToString()=="6"?"工装任务":"待分工" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="分工信息" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlTask" CssClass="link" runat="server" NavigateUrl='<%#"TM_Tech_Assign_Detail.aspx?tmdetail_id="+Eval("TSA_ID")%>'>
                                            <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />
                                            查看
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="经营计划单" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlTask1" CssClass="link" Target="_blank" NavigateUrl='<%#"~/CM_Data/CM_TaskPinS.aspx?action=look&id="+Eval("ID") %>'
                                            runat="server">
                                            <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />查看
                                            <asp:Label ID="lblJYJHD" runat="server" ForeColor="Red" Text="" ToolTip="(正常驳回)"></asp:Label>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="油漆方案" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlTask6" CssClass="link" NavigateUrl='<%#"TM_Paint_Scheme_View.aspx?paint="+Eval("TSA_ID")%>'
                                            runat="server">
                                            <asp:Image ID="Image6" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />查看
                                            <asp:Label ID="lblPAINT" runat="server" ForeColor="Red" Text="" ToolTip="(正常驳回)"></asp:Label>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="我的备注" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlTask9" CssClass="link" ToolTip='<%#Eval("TSA_ID") %>' onclick="ShowMyNode(this);"
                                            runat="server">
                                            <asp:Image ID="Image9" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />修改
                                        </asp:HyperLink>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="rblstatus" EventName="SelectedIndexChanged" />--%>
            <asp:AsyncPostBackTrigger ControlID="ddlEngName" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
